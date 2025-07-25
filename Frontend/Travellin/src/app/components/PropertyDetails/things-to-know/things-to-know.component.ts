import { Component, Input } from '@angular/core';
import { IPropertyInfo } from '../../../models/domain/iproperty-info';
import { CommonModule } from '@angular/common';
import {
  ChevronDown,
  ChevronUp,
  Clock,
  Shield,
  Calendar,
  Dog,
  Camera,
  AlarmSmoke,
  AlarmClock,
  LucideAngularModule,
} from 'lucide-angular';

@Component({
  selector: 'app-things-to-know',
  imports: [CommonModule, LucideAngularModule],
  templateUrl: './things-to-know.component.html',
  styleUrl: './things-to-know.component.css',
})
export class ThingsToKnowComponent {
  @Input() property!: IPropertyInfo;

  icons = {
    chevronDown: ChevronDown,
    chevronUp: ChevronUp,
    clock: Clock,
    shield: Shield,
    calendar: Calendar,
    dog: Dog,
    camera: Camera,
    smokeAlarm: AlarmSmoke,
    carbonAlarm: AlarmClock,
  };

  showModal = false;
  modalContent: { title: string; items: string[] } = { title: '', items: [] };

  openModal(title: string, items: string[]) {
    this.modalContent = { title, items };
    this.showModal = true;
  }

  closeModal() {
    this.showModal = false;
  }

  parseRules(rules?: string | undefined): string[] {
    return rules?.split('\n').filter(r => r.trim()) || [];
  }

  expandedSections: { [key: string]: boolean } = {
    houseRules: false,
    safety: false,
    cancellation: false,
  };

  toggleSection(section: string): void {
    this.expandedSections[section] = !this.expandedSections[section];
  }

  parseHouseRules(rules: string): string[] {
    if (!rules) return [];
    return rules.split('\n').filter(rule => rule.trim() !== '');
  }

  isSectionExpanded(section: string): boolean {
    return this.expandedSections[section] || false;
  }

  filterTruthy(items: any[]): any[] {
    return items.filter(item => !!item);
  }
}
