import { Component, Input, OnInit } from '@angular/core';
import { IPropertyAmenity } from '../../../models/domain/iproperty-amenity';
import { PropertyService } from '../../../services/property.service';
import { CommonModule, JsonPipe } from '@angular/common';
import { IAminityCategory } from '../../../models/domain/IAminity-category';
import { forkJoin } from 'rxjs';
import { ActivatedRoute } from '@angular/router';
import { IPropertyAmenityItem } from '../../../models/domain/iproperty-amenity-item';

import * as lucideAngular from 'lucide-angular';
import {
  AirVent,
  Archive,
  Baby,
  Bath,
  BedDouble,
  Blend,
  Blocks,
  BookOpen,
  Bug,
  Cable,
  Calendar,
  Car,
  ChefHat,
  Coffee,
  CupSoda,
  Dice3,
  Dice5,
  DoorClosed,
  DoorOpen,
  Droplet,
  Fan,
  Film,
  FireExtinguisher,
  Flame,
  FlaskRound,
  Gamepad2,
  HeartPulse,
  Home,
  Lamp,
  Laptop,
  Microwave,
  Mountain,
  PawPrint,
  Pill,
  Puzzle,
  Sandwich,
  Sheet,
  Shirt,
  ShowerHead,
  Snowflake,
  Sofa,
  Sparkles,
  Thermometer,
  ThermometerSun,
  Toilet,
  Trophy,
  Truck,
  Tv,
  User,
  Utensils,
  UtensilsCrossed,
  Volume2,
  Wand2,
  Waves,
  Wifi,
  Wind,
} from 'lucide-angular';

@Component({
  selector: 'app-property-aminities',
  imports: [CommonModule, JsonPipe, lucideAngular.LucideAngularModule],
  templateUrl: './property-aminities.component.html',
  styleUrl: './property-aminities.component.css',
})
export class PropertyAminitiesComponent implements OnInit {
  icon = {
    sparkles: Sparkles,
  };

  icons: { [key: string]: any } = {
    wifi: Wifi,
    waves: Waves,
    airVent: AirVent,
    bath: Bath,
    wind: Wind,
    broom: Wand2,
    droplets: Droplet,
    droplet: Droplet,
    soap: Sparkles,
    toilet: Toilet,
    showerHead: ShowerHead,
    thermometerSun: ThermometerSun,
    flaskRound: FlaskRound,
    sparkles: Sparkles,
    shirt: Shirt,
    bedDouble: BedDouble,
    pill: Pill,
    lamp: Lamp,
    flame: Flame,
    hanger: Shirt,
    bug: Bug,
    archive: Archive,
    cable: Cable,
    tv: Tv,
    volume2: Volume2,
    gamepad2: Gamepad2,
    tennis: Trophy,
    dice5: Dice5,
    bookOpen: BookOpen,
    film: Film,
    baby: Baby,
    blocks: Blocks,
    chair: Home,
    utensilsCrossed: UtensilsCrossed,
    dice3: Dice3,
    doorClosed: DoorClosed,
    user: User,
    puzzle: Puzzle,
    fan: Fan,
    thermometer: Thermometer,
    firstAidKit: HeartPulse,
    laptop: Laptop,
    chefHat: ChefHat,
    fridge: CupSoda,
    plate: Truck,
    snowflake: Snowflake,
    microwave: Microwave,
    kettle: Coffee,
    coffee: Coffee,
    sandwich: Sandwich,
    sheet: Sheet,
    blender: Blend,
    knife: Utensils,
    doorOpen: DoorOpen,
    mountain: Mountain,
    fireExtinguisher: FireExtinguisher,
    sofa: Sofa,
    pawPrint: PawPrint,
    car: Car,
    calendar: Calendar,
  };

  @Input() propertyId!: string;

  // State variables
  isModalOpen = false;
  amenities: IPropertyAmenity[] = [];
  categories: IAminityCategory[] = [];
  isLoading = true;
  showAll = false;
  error = false;

  // Display constants
  readonly PREVIEW_AMENITIES_COUNT = 6;

  constructor(
    private propertyService: PropertyService,
    private route: ActivatedRoute
  ) {}

  ngOnInit() {
    this.propertyId = this.route.snapshot.paramMap.get('id') || '';
    console.log('Property ID:', this.propertyId);
    this.loadAmenitiesData();
  }
  loadAmenitiesData() {
    this.isLoading = true;
    this.error = false;

    forkJoin({
      propertyAmenities: this.propertyService.getAmenitiesByPropertyId(
        this.propertyId
      ),
      allAmenities: this.propertyService.getAllAmenities(),
      categories: this.propertyService.getAmenityCategories(),
    }).subscribe({
      next: ({ propertyAmenities, allAmenities, categories }) => {
        try {
          console.log('Raw API Responses:', {
            propertyAmenities,
            allAmenities,
            categories,
          });

          this.categories =
            this.extractItemsArray<IAminityCategory>(categories);
          console.log('Processed categories:', this.categories);

          const propertyAmenitiesArray =
            this.extractItemsArray<IPropertyAmenityItem>(propertyAmenities);
          console.log('Property amenities:', propertyAmenitiesArray);

          const allAmenitiesArray =
            this.extractItemsArray<IPropertyAmenity>(allAmenities);
          console.log('All amenities:', allAmenitiesArray);

          const allAmenitiesMap = new Map<string, IPropertyAmenity>();
          allAmenitiesArray.forEach(amenity => {
            if (amenity?.id) {
              allAmenitiesMap.set(amenity.id.toString(), amenity);
            }
          });

          const propertyAmenityIds = new Set(
            propertyAmenitiesArray
              .map(pa => pa?.amenity?.id?.toString())
              .filter(Boolean)
          );
          console.log('Property amenity IDs:', propertyAmenityIds);

          this.amenities = Array.from(propertyAmenityIds)
            .map(id => allAmenitiesMap.get(id))
            .filter((amenity): amenity is IPropertyAmenity => !!amenity);

          console.log('Final amenities list:', this.amenities);

          this.isLoading = false;
        } catch (error) {
          console.error('Error processing amenities data:', error);
          this.error = true;
          this.isLoading = false;
        }
      },
      error: err => {
        console.error('Error loading amenities:', err);
        this.error = true;
        this.isLoading = false;
      },
    });
  }

  private extractItemsArray<T>(data: any): T[] {
    if (Array.isArray(data)) {
      return data;
    }
    if (data && typeof data === 'object' && Array.isArray(data.items)) {
      return data.items;
    }
    return [];
  }

  getTopAmenities(): IPropertyAmenity[] {
    return this.amenities.slice(0, this.PREVIEW_AMENITIES_COUNT);
  }

  getGroupedAmenities(): {
    categoryName: string;
    amenities: IPropertyAmenity[];
  }[] {
    if (!this.amenities.length || !this.categories.length) return [];

    const categoryMap = new Map<string, string>();
    this.categories.forEach(category => {
      if (category?.id && category?.name) {
        categoryMap.set(category.id.toString(), category.name);
      }
    });

    const grouped = this.amenities.reduce(
      (acc, amenity) => {
        if (!amenity?.categoryId) return acc;

        const categoryId = amenity.categoryId.toString();
        const categoryName = categoryMap.get(categoryId) || 'Other Amenities'; // Better fallback

        if (!acc[categoryName]) {
          acc[categoryName] = [];
        }

        if (amenity.name && typeof amenity.name === 'string') {
          acc[categoryName].push(amenity);
        }

        return acc;
      },
      {} as Record<string, IPropertyAmenity[]>
    );

    return Object.entries(grouped)
      .map(([categoryName, amenities]) => ({
        categoryName,
        amenities: amenities.sort((a, b) =>
          (a.name || '').localeCompare(b.name || '')
        ),
      }))
      .sort((a, b) => a.categoryName.localeCompare(b.categoryName));
  }

  toggleShowAll() {
    this.showAll = !this.showAll;
  }

  openModal() {
    this.isModalOpen = true;
    document.body.style.overflow = 'hidden';
  }

  closeModal() {
    this.isModalOpen = false;
    document.body.style.overflow = '';
  }
}
