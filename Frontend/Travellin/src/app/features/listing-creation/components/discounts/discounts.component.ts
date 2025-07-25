import { Component } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-discounts',
  standalone: true,
  imports: [CommonModule, FormsModule],
  templateUrl: './discounts.component.html',
  styleUrls: ['./discounts.component.css']
})
export class DiscountsComponent {
  discounts = [
    {
      name: 'New listing promotion',
      percentage: 20,
      description: 'Offer 20% off your first 3 bookings',
      isActive: true
    },
    {
      name: 'Last-minute discount',
      percentage: 18,
      description: 'For stays booked 14 days or less before arrival',
      isActive: false
    },
    {
      name: 'Weekly discount',
      percentage: 10,
      description: 'For stays of 7 nights or more',
      isActive: false
    },
    {
      name: 'Monthly discount',
      percentage: 20,
      description: 'For stays of 28 nights or more',
      isActive: false
    }
  ];

  showCustomDiscount = false;
  customDiscountValue = 10;
  customDiscountNights = 7;

  toggleDiscount(discount: any) {
    discount.isActive = !discount.isActive;
  }

  toggleCustomDiscount() {
    this.showCustomDiscount = !this.showCustomDiscount;
  }

  addCustomDiscount() {
    if (this.customDiscountValue && this.customDiscountNights) {
      this.discounts.push({
        name: `Custom discount (${this.customDiscountNights}+ nights)`,
        percentage: this.customDiscountValue,
        description: `${this.customDiscountValue}% off for ${this.customDiscountNights}+ night stays`,
        isActive: true
      });
      this.showCustomDiscount = false;
    }
  }
}
