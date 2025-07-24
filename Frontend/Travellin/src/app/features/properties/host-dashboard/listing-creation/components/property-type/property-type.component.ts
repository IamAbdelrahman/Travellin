import { Component } from '@angular/core';
import { ListingCreationService } from '../../services/listing-creation.service';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-property-type',
  templateUrl: './property-type.component.html',
  styleUrls: ['./property-type.component.css'],
  imports: [CommonModule]
})
export class PropertyTypeComponent {
  propertyTypes:string[] = [
    'House', 'Apartment', 'Barn',
    'Bed & breakfast', 'Boat', 'Cabin',
    'Camper/RV', 'Casa particular', 'Castle',
    'Cave', 'Container', 'Cycladic home'
  ];

  selectedType: string | null = null;
  selectedTypeImage: string | null = null;
  constructor(private listingService: ListingCreationService) {}

  selectType(type: string) {
    this.selectedType = type;
    this.listingService.updateListing({ propertyType: type });
  }
}
