import { Component, Input } from '@angular/core';
import { IPropertyInfo } from '../../../models/domain/iproperty-info';
import { CommonModule } from '@angular/common';
import { BedDouble, DoorOpen, LucideAngularModule, Sofa } from 'lucide-angular';

@Component({
  selector: 'app-sleeping-arrangement',
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './sleeping-arrangement.component.html',
  styleUrl: './sleeping-arrangement.component.css',
})
export class SleepingArrangementComponent {
  @Input() property!: IPropertyInfo;

  icon = {
    BedDouble: BedDouble,
    Sofa: Sofa,
    door: DoorOpen,
  };

  hasSpaceItems(spaceTypeId: number): boolean {
    return this.property.spaceItemSummaries?.some(
      (item: any) => item.itemType.propertySpaceTypeId === spaceTypeId
    );
  }

  getSpaceItems(spaceTypeId: number): any[] {
    return (
      this.property.spaceItemSummaries?.filter(
        (item: any) => item.itemType.propertySpaceTypeId === spaceTypeId
      ) || []
    );
  }

  getSpaceItemIcon(itemName: string): string {
    const iconMap: { [key: string]: string } = {
      'Full Bathroom': 'bath',
      'Game Room': 'gamepad',
      Bed: 'bed',
      'King Bed': 'bed',
      'Queen Bed': 'bed',
      'Bunk Bed': 'bed',
      'Sofa Bed': 'sofa',
      Crib: 'baby',
      TV: 'tv',
      Wardrobe: 'hanger',
      Desk: 'monitor',
    };

    return iconMap[itemName] || 'square';
  }
}
