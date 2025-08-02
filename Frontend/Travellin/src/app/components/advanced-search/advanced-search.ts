import { Component, OnInit, HostListener, ElementRef, ViewChild, Output, EventEmitter } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable, BehaviorSubject, debounceTime, distinctUntilChanged, switchMap } from 'rxjs';
import { ApiConstant } from '../../utils/api-constant.util';
import { environment } from '../../../environments/environment';

interface SearchData {
  destination: string;
  checkIn: string;
  checkOut: string;
  guests: string;
}

interface FilterData {
  minPrice: number | null;
  maxPrice: number | null;
  propertyType: string;
  amenities: string[];
}

interface Country {
  id: number;
  name: string;
  regionId: number;
}

interface GuestCounts {
  adults: number;
  children: number;
  infants: number;
  pets: number;
}

// Backend API response structure
interface PaginatedResult<T> {
  items: T[];
  metaData: any;
}

@Component({
  selector: 'app-advanced-search',
  templateUrl: './advanced-search.html',
  styleUrls: ['./advanced-search.css'],
  imports: [FormsModule, CommonModule],
})
export class AdvancedSearchComponent implements OnInit {
  @ViewChild('searchBar') searchBar!: ElementRef;
  @Output() searchEvent = new EventEmitter<any>();
  
  searchData: SearchData = {
    destination: '',
    checkIn: '',
    checkOut: '',
    guests: ''
  };

  filterData: FilterData = {
    minPrice: null,
    maxPrice: null,
    propertyType: '',
    amenities: []
  };

  // Airbnb-like state management
  isExpanded = false;
  isCompact = false;
  activeField: 'destination' | 'dates' | 'guests' | null = null;
  
  // Countries and autocomplete
  countries: Country[] = [];
  filteredCountries: Country[] = [];
  showDestinationDropdown = false;
  
  // Guest selection
  guestCounts: GuestCounts = {
    adults: 0,
    children: 0,
    infants: 0,
    pets: 0
  };
  showGuestsDropdown = false;
  
  // Date selection
  showDatePicker = false;
  
  showFilters = false;
  
  propertyTypes = [
    { value: '', label: 'Any type' },
    { value: 'house', label: 'House' },
    { value: 'apartment', label: 'Apartment' },
    { value: 'hotel', label: 'Hotel' },
    { value: 'villa', label: 'Villa' }
  ];

  amenitiesList = ['WiFi', 'Kitchen', 'Parking', 'Pool'];

  constructor(private http: HttpClient) {}

  ngOnInit() {
    this.loadCountries();
    this.updateGuestsString();
  }

  @HostListener('window:scroll', [])
  onWindowScroll() {
    const scrollTop = window.pageYOffset;
    this.isCompact = scrollTop > 100;
  }

  @HostListener('document:click', ['$event'])
  onDocumentClick(event: Event) {
    const target = event.target as HTMLElement;
    
    if (!this.searchBar?.nativeElement?.contains(target)) {
      this.closeAllDropdowns();
      this.isExpanded = false;
    }
  }

  // Load countries from API
  loadCountries(): void {
    this.http.get<Country[]>(`${environment.apiUrl}/api/v1/Countries`, {
      withCredentials: true
    }).subscribe({
      next: (response) => {
        if (response && response.length > 0) {
          this.countries = response;
          this.filteredCountries = response;
        } else {
          this.setFallbackCountries();
        }
      },
      error: (error) => {
        // Try without credentials to see if it's a CORS issue
        this.http.get<Country[]>(`${environment.apiUrl}/api/v1/Countries`).subscribe({
          next: (response) => {
            if (response && response.length > 0) {
              this.countries = response;
              this.filteredCountries = response;
            } else {
              this.setFallbackCountries();
            }
          },
          error: (credError) => {
            this.setFallbackCountries();
          }
        });
      }
    });
  }

  // Fallback countries if API fails
  setFallbackCountries(): void {
    this.countries = [
      { id: 1, name: 'United States', regionId: 1 },
      { id: 2, name: 'Canada', regionId: 1 },
      { id: 3, name: 'United Kingdom', regionId: 2 },
      { id: 4, name: 'France', regionId: 2 },
      { id: 5, name: 'Germany', regionId: 2 },
      { id: 6, name: 'Spain', regionId: 2 },
      { id: 7, name: 'Italy', regionId: 2 },
      { id: 8, name: 'Japan', regionId: 3 },
      { id: 9, name: 'Australia', regionId: 4 },
      { id: 10, name: 'New Zealand', regionId: 4 }
    ];
    this.filteredCountries = this.countries;
  }

  // Filter countries based on search input
  filterCountries(searchTerm: string): void {
    if (!searchTerm.trim()) {
      this.filteredCountries = this.countries;
      return;
    }
    
    this.filteredCountries = this.countries.filter(country =>
      country.name.toLowerCase().includes(searchTerm.toLowerCase())
    );
  }

  // Select a country
  selectCountry(country: Country): void {
    this.searchData.destination = country.name;
    this.showDestinationDropdown = false;
    this.activeField = null;
  }

  // Expand search bar
  expandSearch(): void {
    this.isExpanded = true;
    this.activeField = 'destination';
    this.showDestinationDropdown = true;
  }

  // Close all dropdowns
  closeAllDropdowns(): void {
    this.showDestinationDropdown = false;
    this.showGuestsDropdown = false;
    this.showDatePicker = false;
    this.activeField = null;
  }

  // Toggle destination dropdown
  toggleDestinationDropdown(): void {
    this.showDestinationDropdown = !this.showDestinationDropdown;
    this.activeField = this.showDestinationDropdown ? 'destination' : null;
    
    if (this.showDestinationDropdown) {
      this.filterCountries(this.searchData.destination);
    }
  }

  // Toggle guests dropdown
  toggleGuestsDropdown(): void {
    this.showGuestsDropdown = !this.showGuestsDropdown;
    this.activeField = this.showGuestsDropdown ? 'guests' : null;
  }

  // Toggle date picker
  toggleDatePicker(): void {
    this.showDatePicker = !this.showDatePicker;
    this.activeField = this.showDatePicker ? 'dates' : null;
  }

  // Update guest counts
  updateGuestCount(type: keyof GuestCounts, increment: boolean): void {
    if (increment) {
      this.guestCounts[type]++;
    } else if (this.guestCounts[type] > 0) {
      this.guestCounts[type]--;
    }
    
    this.updateGuestsString();
  }

  // Update the guests display string
  updateGuestsString(): void {
    const parts = [];
    
    if (this.guestCounts.adults > 0) {
      parts.push(`${this.guestCounts.adults} adult${this.guestCounts.adults > 1 ? 's' : ''}`);
    }
    if (this.guestCounts.children > 0) {
      parts.push(`${this.guestCounts.children} child${this.guestCounts.children > 1 ? 'ren' : ''}`);
    }
    if (this.guestCounts.infants > 0) {
      parts.push(`${this.guestCounts.infants} infant${this.guestCounts.infants > 1 ? 's' : ''}`);
    }
    if (this.guestCounts.pets > 0) {
      parts.push(`${this.guestCounts.pets} pet${this.guestCounts.pets > 1 ? 's' : ''}`);
    }
    
    this.searchData.guests = parts.length > 0 ? parts.join(', ') : 'Add guests';
  }

  // Get total guest count
  getTotalGuests(): number {
    return this.guestCounts.adults + this.guestCounts.children + this.guestCounts.infants;
  }

  // Handle destination input
  onDestinationInput(event: any): void {
    this.filterCountries(event.target.value);
    this.showDestinationDropdown = true;
    this.activeField = 'destination';
  }

  // Handle destination focus
  onDestinationFocus(): void {
    this.expandSearch();
  }

  // Handle destination click
  onDestinationClick(): void {
    this.expandSearch();
    this.showDestinationDropdown = true;
    this.filterCountries(this.searchData.destination);
  }

  // Handle date input focus
  onDateFocus(): void {
    this.expandSearch();
    this.activeField = 'dates';
  }

  // Handle guests focus
  onGuestsFocus(): void {
    this.expandSearch();
    this.activeField = 'guests';
  }

  // Handle guests click
  onGuestsClick(): void {
    this.expandSearch();
    this.showGuestsDropdown = true;
  }

  // Search functionality
  onSearch(): void {
    this.closeAllDropdowns();
    this.isExpanded = false;
    
    // Prepare search parameters
    const searchParams: any = {
      page: 1,
      pageSize: 8,
    };

    // Add destination if available
    if (this.searchData.destination && this.searchData.destination.trim()) {
      searchParams.LocationName = this.searchData.destination.trim();
    }

    // Add check-in date if available
    if (this.searchData.checkIn) {
      searchParams.CheckIn = this.searchData.checkIn;
    }

    // Add check-out date if available
    if (this.searchData.checkOut) {
      searchParams.CheckOut = this.searchData.checkOut;
    }

    // Add guest count if available
    const totalGuests = this.getTotalGuests();
    if (totalGuests > 0) {
      searchParams.GuestCount = totalGuests;
    }

    // Add filter parameters
    if (this.filterData.minPrice !== null && this.filterData.minPrice > 0) {
      searchParams.PriceMin = this.filterData.minPrice;
    }

    if (this.filterData.maxPrice !== null && this.filterData.maxPrice > 0) {
      searchParams.PriceMax = this.filterData.maxPrice;
    }

    if (this.filterData.propertyType && this.filterData.propertyType.trim()) {
      searchParams.PropertyTypeId = this.filterData.propertyType;
    }

    // Emit the search event with parameters
    this.searchEvent.emit(searchParams);
  }

  toggleFilters(): void {
    this.showFilters = !this.showFilters;
  }

  onAmenityChange(amenity: string, event: any): void {
    if (event.target.checked) {
      this.filterData.amenities.push(amenity);
    } else {
      const index = this.filterData.amenities.indexOf(amenity);
      if (index > -1) {
        this.filterData.amenities.splice(index, 1);
      }
    }
  }

  isAmenitySelected(amenity: string): boolean {
    return this.filterData.amenities.includes(amenity);
  }

  clearFilters(): void {
    this.filterData = {
      minPrice: null,
      maxPrice: null,
      propertyType: '',
      amenities: []
    };
  }

  applyFilters(): void {
    this.showFilters = false;
    // Add your filter logic here
  }

  // Get compact display text
  getCompactDisplayText(): string {
    const parts = [];
    if (this.searchData.destination) parts.push(this.searchData.destination);
    if (this.searchData.checkIn && this.searchData.checkOut) parts.push('Dates');
    if (this.getTotalGuests() > 0) parts.push(`${this.getTotalGuests()} guests`);
    
    return parts.length > 0 ? parts.join(' • ') : 'Start your search';
  }

  // TrackBy function for performance
  trackByCountry(index: number, country: Country): number {
    return country.id;
  }
}