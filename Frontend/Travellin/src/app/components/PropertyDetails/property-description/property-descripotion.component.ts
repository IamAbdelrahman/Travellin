import { Component, Input } from '@angular/core';
import { IPropertyInfo } from '../../../models/domain/iproperty-info';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-property-description',
  imports: [CommonModule],
  templateUrl: './property-descripotion.component.html',
  styleUrl: './property-descripotion.component.css',
})
export class PropertyDescripotionComponent {
  @Input() property!: IPropertyInfo;
  isCollapsed = true;
  showModal = false;

  getGuestAccessText(): string {
    // Use safetyInfo or houseRules (whichever exists)
    const text = this.property.safteyInfo || this.property.houseRules || '';
    return this.truncateText(text, 120); // Show first 120 chars
  }

  getFullGuestAccessText(): string {
    return this.property.safteyInfo || this.property.houseRules || '';
  }

  toggleCollapse() {
    if (this.isCollapsed) {
      this.showModal = true; // Show popup when expanding
    }
    this.isCollapsed = !this.isCollapsed;
  }

  closeModal() {
    this.showModal = false;
  }

  private truncateText(text: string, maxLength: number): string {
    return text.length > maxLength
      ? text.substring(0, maxLength) + '...'
      : text;
  }
}
