import { Component, inject } from '@angular/core';
import { Heart, LucideAngularModule } from 'lucide-angular';
import { IFavoriteProperty } from '../../models/domain/ifaviorate-property';
import { FavoritePropertiesService } from '../../services/favorite-properties.service';
import { IGetFavoritePropertiesRes } from '../../models/api/response/iget-favorite-properties-res';
import { IGetFavoritePropertiesReq } from '../../models/api/request/iget-favorite-properties-req';
import { Router, RouterModule } from '@angular/router';
import { ToastService } from '../../services/toast.service';

@Component({
  selector: 'app-favorites-page',
  imports: [LucideAngularModule, RouterModule],
  templateUrl: './favorites-page.component.html',
  styleUrl: './favorites-page.component.scss',
})
export class FavoritesPageComponent {
  private toastService = inject(ToastService);

  icons = {
    heart: Heart,
  };

  properties: IFavoriteProperty[] = [];
  currentPage = 1;
  itemsPerPage = 8;
  totalItems = 0;
  totalPages = 0;
  isLoading = true;

  constructor(
    private favoritesService: FavoritePropertiesService,
    private router: Router
  ) {}

  ngOnInit(): void {
    this.loadFavorites();
  }

  loadFavorites(): void {
    this.isLoading = true;
    const queryParams: IGetFavoritePropertiesReq = {
      page: this.currentPage,
      pageSize: this.itemsPerPage,
    };

    this.favoritesService.getAll(queryParams).subscribe({
      next: (response: IGetFavoritePropertiesRes) => {
        this.properties = response.items;
        this.totalItems = response.metaData.total;
        this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);
        this.isLoading = false;
      },
      error: error => {
        console.error('Error loading favorites:', error);
        this.isLoading = false;
      },
    });
  }

  changePage(page: number): void {
    if (page >= 1 && page <= this.totalPages && page !== this.currentPage) {
      this.currentPage = page;
      this.loadFavorites();
    }
  }

  getPages(): number[] {
    const pages = [];
    const maxVisiblePages = 5;

    let startPage = Math.max(
      1,
      this.currentPage - Math.floor(maxVisiblePages / 2)
    );
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

  toggleFavorite(event: MouseEvent, property: IFavoriteProperty): void {
    event.preventDefault();
    event.stopPropagation();

    this.favoritesService.removeFromFavorites(property.propertyId).subscribe({
      next: () => {
        // Remove the property from the local array
        this.properties = this.properties.filter(
          p => p.propertyId !== property.propertyId
        );

        // Update pagination info
        this.totalItems--;
        this.totalPages = Math.ceil(this.totalItems / this.itemsPerPage);

        // Optional: Show success message
        this.toastService.showSuccess('Removed from favorites');

        // If the current page becomes empty, go to previous page
        if (this.properties.length === 0 && this.currentPage > 1) {
          this.changePage(this.currentPage - 1);
        }
      },
      error: error => {
        console.error('Error removing favorite:', error);
        this.toastService.showError('Failed to remove from favorites');
      },
    });
  }
}
