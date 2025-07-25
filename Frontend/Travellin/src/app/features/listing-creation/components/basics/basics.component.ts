import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-basics',
  standalone: true,
  imports: [CommonModule],
  templateUrl: './basics.component.html',
  styleUrls: ['./basics.component.css']
})
export class BasicsComponent {
  basicItems = [
    { key: 'guests', label: 'Guests', min: 1, max: 16, value: 2 },
    { key: 'bedrooms', label: 'Bedrooms', min: 0, max: 10, value: 1 },
    { key: 'beds', label: 'Beds', min: 1, max: 16, value: 1 },
    { key: 'bathrooms', label: 'Bathrooms', min: 1, max: 10, value: 1 }
  ];

  bedTypes = ['Single', 'Double', 'Queen', 'King', 'Bunk', 'Sofa bed'];
  selectedBeds = new Set<string>();
  showBedTypes = false;

  getCount(key: string): number {
    const item = this.basicItems.find(i => i.key === key);
    return item ? item.value : 0;
  }

  increment(key: string) {
    const item = this.basicItems.find(i => i.key === key);
    if (item && item.value < item.max) item.value++;
  }

  decrement(key: string) {
    const item = this.basicItems.find(i => i.key === key);
    if (item && item.value > item.min) item.value--;
  }

  toggleBedType(bed: string) {
    if (this.selectedBeds.has(bed)) {
      this.selectedBeds.delete(bed);
    } else {
      this.selectedBeds.add(bed);
    }
  }

  toggleBedTypes() {
    this.showBedTypes = !this.showBedTypes;
  }
}
