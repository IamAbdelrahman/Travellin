import { Component, Input, OnInit } from '@angular/core';
import { IPropertyInfo } from '../../../models/domain/iproperty-info';
import { CommonModule } from '@angular/common';
import {
  BadgeCheck,
  CalendarDays,
  CheckCircle2,
  LucideAArrowDown,
  LucideAngularModule,
  Mail,
  Star,
} from 'lucide-angular';
import { ActivatedRoute } from '@angular/router';
import { PropertyService } from '../../../services/property.service';

@Component({
  selector: 'app-property-host',
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './property-host.component.html',
  styleUrl: './property-host.component.css',
})
export class PropertyHostComponent implements OnInit {
  @Input() property!: IPropertyInfo;

  constructor(private propertyService: PropertyService) {}

  icons = {
    calendar: CalendarDays,
    star: Star,
    check: CheckCircle2,
    verified: BadgeCheck,
    mail: Mail,
  };

  ngOnInit() {
    this.propertyService
      .getPropertyById(this.property?.id)
      .subscribe(property => {
        property.body.owner = {
          ...property.body.owner,
          firstName: property.body.owner.firstName || 'Host',
          photo: {
            id: 'default',
            photoUrl: property.body.owner.photo?.photoUrl || 'logo.png',
          },
          email: '',
          lastName: property.body.owner.lastName,
          phoneNumber: '',
          bio: '',
          birthDate: '',
          userId: property.body.owner.userId,
        };

        this.property = property.body;
      });
  }

  get hostName(): string {
    return `${this.property.owner?.firstName || ''} ${this.property.owner?.lastName || ''}`.trim();
  }

  get isSuperhost(): boolean {
    return (
      this.property.reviewCount >= 10 && this.property.averageRating >= 4.3
    );
  }

  get responseRate(): number {
    return 75;
  }
}
