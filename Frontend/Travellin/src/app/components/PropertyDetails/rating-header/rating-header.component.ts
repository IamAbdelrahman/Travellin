import { Component, Input } from '@angular/core';
import { IPropertyInfo } from '../../../models/domain/iproperty-info';
import { CommonModule } from '@angular/common';
import { Award, LucideAngularModule } from 'lucide-angular';

@Component({
  selector: 'app-rating-header',
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './rating-header.component.html',
  styleUrl: './rating-header.component.css',
})
export class RatingHeaderComponent {
  @Input({ required: true }) property!: IPropertyInfo;

  icons = {
    award: Award,
  };

  get starRating(): string {
    const rating = this.property.averageRating || 0;
    return rating.toFixed(2); // Formats to 4.94
  }

  get filledStars(): number {
    return Math.floor(this.property.averageRating || 0);
  }

  get hasHalfStar(): boolean {
    const rating = this.property.averageRating || 0;
    return rating % 1 >= 0.5;
  }

  get emptyStars(): number {
    return 5 - this.filledStars - (this.hasHalfStar ? 1 : 0);
  }

  get reviewCountText(): string {
    return `${this.property.reviewCount} reviews`;
  }

  get isHighlyRated(): boolean {
    return (this.property.averageRating || 0) >= 4.3;
  }
}
