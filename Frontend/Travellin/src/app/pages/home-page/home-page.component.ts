import { Component, OnInit } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PropertyService } from '../../services/property.service';
import { IPropertyTypeRes } from '../../models/api/response/i-property-type-res';
import { HttpResponse, HttpErrorResponse } from '@angular/common/http';
import { IpropertyTypeApiResponse } from '../../models/api/response/iproperty-type-api-res';
import { HostListener } from '@angular/core';
import { IPropertyWithDistance } from '../../models/domain/iproperty-with-distance';
import { Heart, LucideAngularModule } from 'lucide-angular';
import { ToastContainerComponent } from '../../components/toast-container/toast-container.component';
import { ToastService } from '../../services/toast.service'; // Adjust the path as needed
import {LoadSpinnerComponent} from '../../components/load-spinner/load-spinner';
import { AdvancedSearchComponent } from '../../components/advanced-search/advanced-search';
import {
  faHouse,
  faBed,
  faHotel,
  faSun,
  faSearch,
  faGlobe,
  faSliders,
  faStar,
  faStarHalfAlt,
  faTree,
  faUmbrellaBeach,
  faMountain,
  faTractor,
  faCity,
  faHeart,
} from '@fortawesome/free-solid-svg-icons';
import { IProperty } from '../../models/domain/iproperty';
import { IpropertyRes } from '../../models/api/response/iproperty-res';
import { Router, RouterModule, ActivatedRoute } from '@angular/router';
import { FavoritePropertiesService } from '../../services/favorite-properties.service';
import { ISmartSearchReq } from '../../models/api/request/ismartSearch-req';
import { ISmartSearchRes } from '../../models/api/response/ismartSearch-res';
import { IFavoriteProperty } from '../../models/domain/ifaviorate-property';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
@Component({
  selector: 'app-home-page',
  standalone: true,
  imports: [
    CommonModule,
    FormsModule,
    FontAwesomeModule,
    LucideAngularModule,
    RouterModule,
    LoadSpinnerComponent,
    AdvancedSearchComponent
  ],
  templateUrl: './home-page.component.html',
  styleUrls: ['./home-page.component.scss'],
})
export class HomePageComponent implements OnInit {
  icon = {
    heart: Heart,
  };
  searchMode: 'ai' | 'simple' = 'ai';
  guestMenuVisible: boolean = false;
  showFilters: boolean = false;
  selectedPropertyType: number | null = null;
  priceRange: number = 500;
  selectedRating: number = 4;
  propertyTypes: IPropertyTypeRes[] = [];
  property: IPropertyWithDistance[] = [];
  allProperties: IPropertyWithDistance[] = [];
  errorMessage: string = '';
  userLat: number = 30.033333;
  userLon: number = 31.233334;
  scrollY = 0;
  isFocused = false;
  LocationName: string = '';
  checkInDate: string = '';
  checkOutDate: string = '';
  guestsCount: any = null;
  minPrice: number = -1;
  maxPrice: number = -1;
  propertyRating: number = 0;
  propertyTypeId: number = -1;
  stringAiSearch: string | null = null;
  // pagination variables-----------------------------------
  currentPage: number = 1;
  itemsPerPage: number = 8;
  totalItems: number = 0;
  Math = Math;

  guests = {
    adults: 2,
    children: 0,
    infants: 0,
    pets: 0,
  };

  amenities = [
    { id: 1, name: 'Pool', selected: false },
    { id: 2, name: 'WiFi', selected: true },
    { id: 3, name: 'Kitchen', selected: false },
    { id: 4, name: 'Parking', selected: false },
    { id: 5, name: 'Air conditioning', selected: false },
  ];

  // Extended Font Awesome icons
  icons: { [key: string]: any } = {
    heart: faHeart,
    house: faHouse,
    'bed-single': faBed,
    hotel: faHotel,
    'sun-moon': faSun,
    globe: faGlobe,
    search: faSearch,
    sliders: faSliders,
    star: faStar,
    'star-half-alt': faStarHalfAlt,
    tree: faTree,
    'umbrella-beach': faUmbrellaBeach,
    mountain: faMountain,
    tractor: faTractor,
    city: faCity,
  };

  private favoritePropertyIds: string[] = [];
  private isFetchingFavorites = false;
  isLoadingProperties: boolean = true;

  constructor(
    private propertyService: PropertyService,
    private route: Router,
    private favouriteService: FavoritePropertiesService,
    private toastService: ToastService,
    private activeroute: ActivatedRoute
  ) {}

  ngOnInit(): void {
    this.getPropertyTypes();
    this.getAllProperty();
    this.loadFavorites();
    this.checkPaymentStatus();
  }

  setSearchMode(mode: 'ai' | 'simple'): void {
    this.searchMode = mode;
  }

  toggleGuestMenu(): void {
    this.guestMenuVisible = !this.guestMenuVisible;
  }

  @HostListener('window:scroll')
    onWindowScroll() {
    this.scrollY = window.scrollY;
  }

  toggleFilters(): void {
    this.showFilters = !this.showFilters;
  }

  selectPropertyType(id: number): void {
    this.selectedPropertyType = this.selectedPropertyType === id ? null : id;
    const queryParams: any = {
      page: this.currentPage,
      pageSize: this.itemsPerPage,
    };
    if (this.selectedPropertyType) {
      queryParams.PropertyTypeId = this.selectedPropertyType;
    }
    this.propertyService.searchProperty(queryParams).subscribe({
      next: (response: HttpResponse<IpropertyRes>) => {
        this.isLoadingProperties = false;
        if (response.status === 200 && response.body) {
          this.property = response.body.items.map(prop => ({
            ...prop,
            distanceFromMe: this.getDistanceFromLatLonInKm(
              this.userLat,
              this.userLon,
              prop.latitude,
              prop.longitude
            ).toFixed(1),
          }));
          this.totalItems = response.body.metaData.total;
          this.currentPage = response.body.metaData.page;
        } else {
          this.handlePropertyerror('Invalid search result');
        }
      },
      error: error => {
        this.isLoadingProperties = false;
        console.error('Search error:', error);
        this.handlePropertyerror('Failed to search properties');
      },
    });
  }

  getGuestSummary(): string {
    const parts = [];
    if (this.guests.adults > 0)
      parts.push(
        `${this.guests.adults} adult${this.guests.adults > 1 ? 's' : ''}`
      );
    if (this.guests.children > 0)
      parts.push(
        `${this.guests.children} child${this.guests.children > 1 ? 'ren' : ''}`
      );
    if (this.guests.infants > 0)
      parts.push(
        `${this.guests.infants} infant${this.guests.infants > 1 ? 's' : ''}`
      );
    if (this.guests.pets > 0)
      parts.push(`${this.guests.pets} pet${this.guests.pets > 1 ? 's' : ''}`);

    return parts.length > 0 ? parts.join(', ') : 'Add guests';
  }

  getPropertyTypes(): void {
    this.propertyService.showPropertyType().subscribe({
      next: (response: HttpResponse<IpropertyTypeApiResponse>) => {
        if (response.status === 200 && response.body) {
          this.propertyTypes = response.body.items;
        } else {
          this.handlePropertyerror('Invalid Loading Property Types');
        }
      },
      error: error => {
        console.error('Error fetching property types:', error);
        this.handlePropertyerror('Failed to load property types');
      },
    });
  }

  getAllProperty(): void {
    this.isLoadingProperties = true;
    this.propertyService
      .getAllProperty({
        page: this.currentPage,
        pageSize: this.itemsPerPage,
      })
      .subscribe({
        next: (response: HttpResponse<IpropertyRes>) => {
          this.isLoadingProperties = false;
          if (response.status === 200 && response.body) {
            this.property = response.body.items.map(prop => ({
              ...prop,
              distanceFromMe: this.getDistanceFromLatLonInKm(
                this.userLat,
                this.userLon,
                prop.latitude,
                prop.longitude
              ).toFixed(1),
            }));
            this.totalItems = response.body.metaData.total;
            this.currentPage = response.body.metaData.page;
          } else {
            this.handlePropertyerror('Invalid Loading Property');
          }
        },
        error: error => {
          this.isLoadingProperties = false;
          console.error('Error fetching property:', error);
          this.handlePropertyerror('Failed to load property');
        },
      });
  }

  handlePropertyerror(message: string): void {
    this.errorMessage = message;
  }

  // Helper method to get icon for property type
  getPropertyIcon(iconName: string): any {
    return this.icons[iconName] || this.icons['house']; // Default to house icon if not found
  }

  getDistanceFromLatLonInKm(
    lat1: number,
    lon1: number,
    lat2: number,
    lon2: number
  ): number {
    const R = 6371; // Radius of the earth in km
    const dLat = this.deg2rad(lat2 - lat1);
    const dLon = this.deg2rad(lon2 - lon1);
    const a =
      Math.sin(dLat / 2) * Math.sin(dLat / 2) +
      Math.cos(this.deg2rad(lat1)) *
        Math.cos(this.deg2rad(lat2)) *
        Math.sin(dLon / 2) *
        Math.sin(dLon / 2);
    const c = 2 * Math.atan2(Math.sqrt(a), Math.sqrt(1 - a));
    const d = R * c; // Distance in km
    return d;
  }

  deg2rad(deg: number): number {
    return deg * (Math.PI / 180);
  }

  getMarginTop() {
    if (this.searchMode === 'simple' && this.showFilters) {
      return 250; // 250px if Advanced and showFilters is visible
    } else if (this.searchMode === 'simple') {
      return 200; // 200px if Advanced and showFilters is not visible
    } else {
      return 100; // 100px if Simple search mode
    }
  }

  onFocus() {
    this.isFocused = true;
  }

  onBlur() {
    this.isFocused = false;
  }

  updatePageData(): void {
    const startIndex = (this.currentPage - 1) * this.itemsPerPage;
    const endIndex = startIndex + this.itemsPerPage;
    this.allProperties = this.property.slice(startIndex, endIndex);
  }

  get totalPages(): number {
    return Math.ceil(this.totalItems / this.itemsPerPage);
  }

  getPages(): number[] {
    const pages = [];
    const maxVisiblePages = 5; // Show max 5 page numbers at a time
    const halfVisible = Math.floor(maxVisiblePages / 2);

    let startPage = Math.max(1, this.currentPage - halfVisible);
    let endPage = startPage + maxVisiblePages - 1;

    if (endPage > this.totalPages) {
      endPage = this.totalPages;
      startPage = Math.max(1, endPage - maxVisiblePages + 1);
    }

    for (let i = startPage; i <= endPage; i++) {
      pages.push(i);
    }

    return pages;
  }

  onPageChange(pageNumber: number): void {
    if (pageNumber < 1 || pageNumber > this.totalPages) return;

    this.currentPage = pageNumber;

    // Fetch new data for the page (server-side pagination)
    this.getAllProperty();

    // Scroll to top of results
    this.scrollToResults();
  }
  private scrollToResults(): void {
    const cardsElement = document.getElementById('cards');
    if (cardsElement) {
      cardsElement.scrollIntoView({ behavior: 'smooth' });
    }
  }

  navigateToProperty(propertyId: string): void {
    this.route.navigate(['/property', propertyId]);
  }

  searchProperties(): void {
    this.isLoadingProperties = true;
    const queryParams: any = {
      page: this.currentPage,
      pageSize: this.itemsPerPage,
    };

    // Add location if it's available
    if (this.LocationName) {
      queryParams.LocationName = this.LocationName;
    }

    // Add checkInDate if it's available
    if (this.checkInDate) {
      queryParams.CheckIn = this.checkInDate;
    }

    // Add checkOutDate if it's available
    if (this.checkOutDate) {
      queryParams.CheckOut = this.checkOutDate;
    }

    // Add guestsCount if it's greater than 0
    if (this.guestsCount != null) {
      queryParams.GuestCount = this.guestsCount;
    }

    // Add price range
    if (this.minPrice >= 0) {
      queryParams.PriceMin = this.minPrice;
    }

    // Add maxPrice if it's greater than 0
    if (this.maxPrice > 0 && this.maxPrice > this.minPrice) {
      queryParams.PriceMax = this.maxPrice;
    }

    // Add propertyRating if it's greater than 0
    if (this.selectedRating > 0) {
      queryParams.MinAvgRating = this.selectedRating;
    }

    // Add propertyTypeId if it's greater than 0
    if (this.selectedPropertyType) {
      queryParams.PropertyTypeId = this.selectedPropertyType;
    }

    this.propertyService.searchProperty(queryParams).subscribe({
      next: (response: HttpResponse<IpropertyRes>) => {
        this.isLoadingProperties = false;
        if (response.status === 200 && response.body) {
          this.property = response.body.items.map(prop => ({
            ...prop,
            distanceFromMe: this.getDistanceFromLatLonInKm(
              this.userLat,
              this.userLon,
              prop.latitude,
              prop.longitude
            ).toFixed(1),
          }));
          this.totalItems = response.body.metaData.total;
          this.currentPage = response.body.metaData.page;
        } else {
          this.handlePropertyerror('Invalid search result');
        }
      },
      error: error => {
        this.isLoadingProperties = false;
        console.error('Search error:', error);
        this.handlePropertyerror('Failed to search properties');
      },
    });
  }

  SmartSearch(): void {
    if (this.stringAiSearch) {
      this.isLoadingProperties = true;

      this.propertyService.smartSearch(this.stringAiSearch).subscribe({
        next: (response: HttpResponse<ISmartSearchRes>) => {
          this.isLoadingProperties = false;
          if (response.status === 200 && response.body) {
            this.property = response.body.items.map(prop => ({
              ...prop,
              distanceFromMe: this.getDistanceFromLatLonInKm(
                this.userLat,
                this.userLon,
                prop.latitude,
                prop.longitude
              ).toFixed(1),
            }));
            this.totalItems = response.body.metaData.total;
            this.currentPage = response.body.metaData.page;
          } else {
            this.handlePropertyerror('Invalid Smart Search result');
          }
        },
        error: error => {
          this.isLoadingProperties = false;
          console.error('Error during Smart Search:', error);
          this.handlePropertyerror('Failed to search properties');
        },
      });
    }
  }

  private loadFavorites(
    page = 1,
    accumulatedFavorites: IFavoriteProperty[] = []
  ): void {
    if (this.isFetchingFavorites) return;

    this.isFetchingFavorites = true;
    const pageSize = 100; // Adjust based on your API's max page size

    this.favouriteService.getAll({ page, pageSize }).subscribe({
      next: response => {
        this.isFetchingFavorites = false;

        if (response?.items) {
          const allFavorites = [...accumulatedFavorites, ...response.items];

          // If we got a full page, there might be more favorites
          if (response.items.length === pageSize) {
            this.loadFavorites(page + 1, allFavorites);
          } else {
            // We've fetched all favorites
            this.favoritePropertyIds = allFavorites.map(fav => fav.propertyId);
          }
        }
      },
      error: error => {
        this.isFetchingFavorites = false;
        console.error('Error loading favorites:', error);
      },
    });
  }

  isFavorite(propertyId: string): boolean {
    return this.favoritePropertyIds.includes(propertyId);
  }

  toggleFavorite(event: Event, propertyId: string): void {
    event.stopPropagation();

    if (this.isFavorite(propertyId)) {
      this.removeFromFavorites(propertyId);
    } else {
      this.addToFavorites(propertyId);
    }
  }

  addToFavorites(propertyId: string): void {
    this.favouriteService.addToFavorites(propertyId).subscribe({
      next: () => {
        this.favoritePropertyIds.push(propertyId);
        this.toastService.showSuccess('Added to favorites');
      },
      error: error => {
        console.error('Error adding favorite:', error);
        this.toastService.showError('Failed to add to favorites');
      },
    });
  }

  removeFromFavorites(propertyId: string): void {
    this.favouriteService.removeFromFavorites(propertyId).subscribe({
      next: () => {
        this.favoritePropertyIds = this.favoritePropertyIds.filter(
          id => id !== propertyId
        );
        this.toastService.showSuccess('Removed from favorites');
      },
      error: error => {
        console.error('Error removing favorite:', error);
        this.toastService.showError('Failed to remove from favorites');
      },
    });
  }
  checkPaymentStatus(): void {
    const paymentStatus =
      this.activeroute.snapshot.queryParamMap.get('paymentStatus');

    if (!paymentStatus) {
      return;
    }

    if (paymentStatus.toLowerCase() === 'success') {
      this.toastService.showSuccess('Payment successful');
    } else if (paymentStatus.toLowerCase() === 'canceled') {
      this.toastService.showError('Payment canceled, please try again');
    }
  }
  handleAdvancedSearch(searchParams: any): void {
    this.isLoadingProperties = true;
    this.currentPage = 1; // Reset to first page for new search

    this.propertyService.searchProperty(searchParams).subscribe({
      next: (response: HttpResponse<IpropertyRes>) => {
        this.isLoadingProperties = false;
        if (response.status === 200 && response.body) {
          this.property = response.body.items.map(prop => ({
            ...prop,
            distanceFromMe: this.getDistanceFromLatLonInKm(
              this.userLat,
              this.userLon,
              prop.latitude,
              prop.longitude
            ).toFixed(1),
          }));
          this.totalItems = response.body.metaData.total;
          this.currentPage = response.body.metaData.page;
          
          // Scroll to results
          this.scrollToResults();
        } else {
          this.handlePropertyerror('Invalid search result');
        }
      },
      error: error => {
        this.isLoadingProperties = false;
        console.error('Advanced search error:', error);
        this.handlePropertyerror('Failed to search properties');
      },
    });
  }

  onFilterChange() {
    const queryParams: any = {
      page: this.currentPage,
      pageSize: this.itemsPerPage,
    };

    // Add minPrice if it's >= 0
    if (this.minPrice >= 0) {
      queryParams.PriceMin = this.minPrice;
    }
    if (this.selectedPropertyType) {
      queryParams.PropertyTypeId = this.selectedPropertyType;
    }

    // Add maxPrice if valid
    if (this.maxPrice > 0 && this.maxPrice > this.minPrice) {
      queryParams.PriceMax = this.maxPrice;
    }

    // Add property rating if selected
    if (this.selectedRating > 0) {
      queryParams.MinAvgRating = this.selectedRating;
    }

    this.isLoadingProperties = true;

    this.propertyService.searchProperty(queryParams).subscribe({
      next: (response: HttpResponse<IpropertyRes>) => {
        this.isLoadingProperties = false;
        if (response.status === 200 && response.body) {
          this.property = response.body.items.map(prop => ({
            ...prop,
            distanceFromMe: this.getDistanceFromLatLonInKm(
              this.userLat,
              this.userLon,
              prop.latitude,
              prop.longitude
            ).toFixed(1),
          }));
          this.totalItems = response.body.metaData.total;
          this.currentPage = response.body.metaData.page;
        } else {
          this.handlePropertyerror('Invalid search result');
        }
      },
      error: error => {
        this.isLoadingProperties = false;
        console.error('Search error:', error);
        this.handlePropertyerror('Failed to search properties');
      },
    });
  }
}
