import { AfterViewInit, Component, Input, OnDestroy } from '@angular/core';
import { IPropertyInfo } from '../../../models/domain/iproperty-info';
import L from 'leaflet';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-property-map',
  imports: [CommonModule],
  templateUrl: './property-map.component.html',
  styleUrl: './property-map.component.css',
})
export class PropertyMapComponent implements AfterViewInit, OnDestroy {
  @Input() property!: IPropertyInfo;
  private map!: L.Map;
  private marker!: L.Marker;

  ngAfterViewInit(): void {
    if (this.property && this.property.latitude && this.property.longitude) {
      this.initMap();
    } else {
      console.warn('Property data missing coordinates');
    }
  }

  private initMap(): void {
    // Initialize map with property coordinates
    this.map = L.map('map').setView(
      [this.property.latitude, this.property.longitude],
      15
    );

    // Add tile layer
    L.tileLayer('https://{s}.tile.openstreetmap.org/{z}/{x}/{y}.png', {
      attribution: '© OpenStreetMap contributors',
      maxZoom: 19,
    }).addTo(this.map);

    // Add custom marker
    this.marker = L.marker([this.property.latitude, this.property.longitude], {
      icon: this.getCustomMarker(),
      title: this.property.title,
    }).addTo(this.map);

    // Add popup with property info
    this.marker.bindPopup(`
      <b>${this.property.title}</b><br>
      ${this.property.pricePerNight}/night
    `);
  }

  private getCustomMarker(): L.Icon {
    return L.icon({
      iconUrl: 'https://unpkg.com/leaflet@1.7.1/dist/images/marker-icon.png',
      iconRetinaUrl:
        'https://unpkg.com/leaflet@1.7.1/dist/images/marker-icon-2x.png',
      shadowUrl:
        'https://unpkg.com/leaflet@1.7.1/dist/images/marker-shadow.png',
      iconSize: [25, 41],
      iconAnchor: [12, 41],
      popupAnchor: [1, -34],
      shadowSize: [41, 41],
    });
  }

  ngOnDestroy(): void {
    if (this.map) {
      this.map.remove();
    }
  }
}
