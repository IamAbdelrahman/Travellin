import { Component } from '@angular/core';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { NgModule } from '@angular/core';
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

@Component({
  selector: 'app-advanced-search',
  templateUrl: './advanced-search.html',
  styleUrls: ['./advanced-search.css'],
  imports: [FormsModule, CommonModule],
})
export class AdvancedSearchComponent {
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

  showFilters = false;
  
  propertyTypes = [
    { value: '', label: 'Any type' },
    { value: 'house', label: 'House' },
    { value: 'apartment', label: 'Apartment' },
    { value: 'hotel', label: 'Hotel' },
    { value: 'villa', label: 'Villa' }
  ];

  amenitiesList = ['WiFi', 'Kitchen', 'Parking', 'Pool'];

  onSearch(): void {
    console.log('Search data:', this.searchData);
    console.log('Filter data:', this.filterData);
    // Add your search logic here
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
    console.log('Applying filters:', this.filterData);
    this.showFilters = false;
    // Add your filter logic here
  }
}