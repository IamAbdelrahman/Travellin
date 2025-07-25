import { Component, Input, OnInit } from '@angular/core';
import { IPropertyInfo } from '../../../models/domain/iproperty-info';
import { ILocation } from '../../../models/domain/ilocation';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-property-header',
  imports: [CommonModule],
  templateUrl: './property-header.component.html',
  styleUrl: './property-header.component.css',
})
export class PropertyHeaderComponent implements OnInit {
  @Input() property: IPropertyInfo | null = null;

  ngOnInit(): void {
    console.log('Property Type:', this.property?.propertyType);
  }

  get ratingDisplay(): string {
    if (!this.property?.averageRating) return '0';
    return this.property.averageRating.toFixed(1);
  }

  get reviewCountDisplay(): string {
    if (!this.property?.reviewCount) return '0';
    return this.property.reviewCount.toString();
  }
  getLocation() {}
}
