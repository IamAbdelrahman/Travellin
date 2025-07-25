import { Component, Input } from '@angular/core';
import { IPropertyInfo } from '../../../models/domain/iproperty-info';
import { CommonModule } from '@angular/common';
import { KeyRound, LucideAngularModule, MapPin, Undo2 } from 'lucide-angular';

@Component({
  selector: 'app-property-highlights',
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './property-highlights.component.html',
  styleUrl: './property-highlights.component.css',
})
export class PropertyHighlightsComponent {
  @Input() property!: IPropertyInfo;
  icon = {
    key: KeyRound,
    mapPin: MapPin,
    undo: Undo2,
  };

  hasSelfCheckIn(): boolean {
    const text = [this.property.safteyInfo, this.property.houseRules]
      .join(' ')
      .toLowerCase();

    return /lockbox|self.?check|keypad/.test(text);
  }
}
