import { Component, OnInit } from '@angular/core';
import { Router, RouterModule } from '@angular/router';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { PropertyService } from '../../services/property.service';
import { HttpResponse } from '@angular/common/http';
import { FontAwesomeModule } from '@fortawesome/angular-fontawesome';
import { IPropertyWithDistance } from '../../models/domain/iproperty-with-distance';
import { IpropertyRes } from '../../models/api/response/iproperty-res';
import { ToastService } from '../../services/toast.service';
import {
  faChevronLeft,
  faChevronRight,
  faTrash,
  faThumbtack,
  faCheck,
  faTimes,
  faCheckCircle,
} from '@fortawesome/free-solid-svg-icons';

@Component({
  selector: 'app-property-admin',
  imports: [CommonModule, FormsModule, RouterModule, FontAwesomeModule],
  templateUrl: './property-admin.component.html',
  styleUrl: './property-admin.component.scss',
})
export class PropertyAdminComponent implements OnInit {
  searchMode: 'simple' | 'advanced' = 'simple';
  showFilters: boolean = false;
  property: IPropertyWithDistance[] = [];
  allProperties: IPropertyWithDistance[] = [];
  errorMessage: string = '';
  userLat: number = 30.033333;
  userLon: number = 31.233334;
  currentPage: number = 1;
  itemsPerPage: number = 10;
  totalItems: number = this.property?.length || 0;
  Math = Math;
  isLoadingProperties: boolean = true;

  icons: { [key: string]: any } = {
    'chevron-left': faChevronLeft,
    'chevron-right': faChevronRight,
    trash: faTrash,
    pin: faThumbtack,
    check: faCheck,
    times: faTimes,
    checkCircle: faCheckCircle,
  };
  constructor(
    private propertyService: PropertyService,
    private route: Router,
    private toastService: ToastService
  ) {}
  ngOnInit(): void {
    this.getAllProperty();
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
    if (this.searchMode === 'advanced' && this.showFilters) {
      return 250; // 250px if Advanced and showFilters is visible
    } else if (this.searchMode === 'advanced') {
      return 200; // 200px if Advanced and showFilters is not visible
    } else {
      return 100; // 100px if Simple search mode
    }
  }
  // updatePageData(): void {
  //   // const startIndex = (this.currentPage - 1) * this.itemsPerPage;
  //   // const endIndex = startIndex + this.itemsPerPage;
  //   // this.allProperties = this.property.slice(startIndex, endIndex);
  //   if (this.itemsPerPage <= 0) {
  //     this.itemsPerPage = 10; // Set a default value
  //   }
  //   const startIndex = (this.currentPage - 1) * this.itemsPerPage;
  //   const endIndex = startIndex + this.itemsPerPage;
  //   this.allProperties = this.property.slice(startIndex, endIndex);
  // }
  // onPageChange(pageNumber: number): void {
  //   this.currentPage = pageNumber;
  //   this.updatePageData();
  // }
  //Pagination--------------------------------------------------------------
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
  //======================================================================
  onDelete(propertyId: string): void {
    const propToDelete = this.property.find(p => p.id === propertyId);
    if (!propToDelete) return;
    if (propToDelete.isDeleted) {
      this.toastService.showWarning('This property is already deleted.');
      return;
    }

    if (confirm('Are you sure you want to delete this property?')) {
      this.propertyService.deleteProperty(propertyId).subscribe({
        next: () => {
          console.log('Trying to delete property with ID:', propertyId); // to test
          this.property = this.property.filter(p => p.id !== propertyId);
          //show toast
          this.toastService.showSuccess('Property deleted successfully');
        },
        error: err => {
          console.error('Error deleting property', err);
          this.toastService.showError('Failed to delete property');
        },
      });
    }
  }

  toggleActive(property: IPropertyWithDistance): void {
    property.isActive = !property.isActive;
    console.log(
      'Toggle active for property:',
      property.id,
      'Now:',
      property.isActive
    );
  }
}
