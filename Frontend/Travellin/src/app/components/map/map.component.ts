import { Component } from '@angular/core';
import { NgLeafletUniversalModule } from 'ng-leaflet-universal';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-map',
  imports: [NgLeafletUniversalModule, CommonModule],
  templateUrl: './map.component.html',
  styleUrl: './map.component.css',
})
export class MapComponent {}
