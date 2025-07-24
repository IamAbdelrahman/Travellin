import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-amenities',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './amenities.component.html',
  styleUrls: ['./amenities.component.css']
})
export class AmenitiesComponent {
  standardAmenities = [
    'Wifi', 'TV', 'Kitchen', 'Washer',
    'Free parking', 'Paid parking',
    'Air conditioning', 'Dedicated workspace'
  ];

  selectedAmenities = new Set<string>();
  customAmenities: string[] = [];
  customAmenity = '';

  toggleAmenity(amenity: string) {
    if (this.selectedAmenities.has(amenity)) {
      this.selectedAmenities.delete(amenity);
    } else {
      this.selectedAmenities.add(amenity);
    }
  }

  addCustomAmenity() {
    if (this.customAmenity.trim() && !this.customAmenities.includes(this.customAmenity)) {
      this.customAmenities.push(this.customAmenity.trim());
      this.customAmenity = '';
    }
  }

  removeCustomAmenity(amenity: string) {
    this.customAmenities = this.customAmenities.filter(a => a !== amenity);
  }
}
