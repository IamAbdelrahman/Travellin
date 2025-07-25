import {
  Component,
  ElementRef,
  HostListener,
  Input,
  OnInit,
} from '@angular/core';
import { ActivatedRoute, RouterOutlet } from '@angular/router';
import { Icon } from 'leaflet';
import { Lightbox } from 'ngx-lightbox';
import { CommonModule, JsonPipe } from '@angular/common';
import { LucideAngularModule } from 'lucide-angular';
import { LightboxModule } from 'ngx-lightbox';
import { HttpClientModule } from '@angular/common/http';
import { FormsModule } from '@angular/forms';
import { IPropertyImage } from '../../models/domain/iproperty-image';
import { PropertyService } from '../../services/property.service';
import { IUserProfile } from '../../models/domain/iuser-profile';
import { IPropertyAmenity } from '../../models/domain/iproperty-amenity';
import { IAminityCategory } from '../../models/domain/IAminity-category'; // Ensure this path is correct
import { IReview } from '../../models/domain/ireview';
import { IPropertyInfo } from '../../models/domain/iproperty-info';
import { IImage } from '../../models/domain/iimage';
import { PropertyHeaderComponent } from '../../components/PropertyDetails/property-header/property-header.component';
import { RatingHeaderComponent } from '../../components/PropertyDetails/rating-header/rating-header.component';
import { BookingCardComponent } from '../../components/PropertyDetails/booking-card/booking-card.component';
import { PropertyHostComponent } from '../../components/PropertyDetails/property-host/property-host.component';
import { PropertyMapComponent } from '../../components/PropertyDetails/property-map/property-map.component';
import { PropertyHighlightsComponent } from '../../components/PropertyDetails/property-highlights/property-highlights.component';
import { PropertyDescripotionComponent } from '../../components/PropertyDetails/property-description/property-descripotion.component';
import { SleepingArrangementComponent } from '../../components/PropertyDetails/sleeping-arrangement/sleeping-arrangement.component';
import { PropertyAminitiesComponent } from '../../components/PropertyDetails/property-aminities/property-aminities.component';
import { ThingsToKnowComponent } from '../../components/PropertyDetails/things-to-know/things-to-know.component';
import { FavoriteButtonComponent } from '../../components/PropertyDetails/favorite-button/favorite-button.component';
import { PropertyAvailabilityComponent } from '../../components/PropertyDetails/property-availability/property-availability.component';
import { forkJoin } from 'rxjs';

@Component({
  selector: 'app-property-info',
  standalone: true,
  imports: [
    CommonModule,
    LucideAngularModule,
    LightboxModule,
    HttpClientModule,
    FormsModule,
    RouterOutlet,
    PropertyDescripotionComponent,
    SleepingArrangementComponent,
    JsonPipe,
    PropertyHeaderComponent,
    RatingHeaderComponent,
    BookingCardComponent,
    PropertyHostComponent,
    PropertyMapComponent,
    PropertyHighlightsComponent,
    PropertyAminitiesComponent,
    ThingsToKnowComponent,
    FavoriteButtonComponent,
    PropertyAvailabilityComponent,
  ],
  templateUrl: './property-info.component.html',
  styleUrls: ['./property-info.component.scss'],
})
export class PropertyInfoComponent implements OnInit {
  @Input() property!: IPropertyInfo;
  propertyImages: IPropertyImage[] = [];
  aminaties: IPropertyAmenity[] = [];
  reviews: IReview[] = [];
  host: IUserProfile | null = null;
  loading = true;
  error = false;
  lightboxImages: any[] = [];
  categories: IAminityCategory[] = [];

  activeImageIndex = 0;

  propertyId!: string;

  constructor(
    private route: ActivatedRoute,
    private propertyService: PropertyService,
    private lightbox: Lightbox,
    private elementRef: ElementRef
  ) {}

  ngOnInit(): void {
    this.fixLeafletAssets();
    this.route.paramMap.subscribe(params => {
      const propertyId = params.get('id');

      if (propertyId) {
        this.loadProperty(propertyId);
        this.loadAmenitiesDataForPropertyInfo(propertyId);
      } else {
        this.error = true;
        this.loading = false;
      }
    });
  }

  private fixLeafletAssets() {
    delete (Icon.Default.prototype as any)._getIconUrl;
    Icon.Default.mergeOptions({
      iconRetinaUrl: 'assets/marker-icon-2x.png',
      iconUrl: 'assets/marker-icon.png',
      shadowUrl: 'assets/marker-shadow.png',
    });
  }

  loadProperty(id: string): void {
    this.loading = true;
    this.error = false;

    this.propertyService.getPropertyById(id).subscribe({
      next: response => {
        if (!response?.body) {
          console.error('Invalid response structure - missing propertyData');
          this.error = true;
          this.loading = false;
          return;
        }

        this.property = response.body;
        this.host = response.body.owner.userName;

        this.propertyImages = response.body.photos.map(
          (photo: IImage): IImage => ({
            id: photo.id,
            photoUrl: photo.photoUrl,
          })
        );

        if (this.propertyImages.length > 0) {
          this.prepareLightboxImages();
        }

        this.loading = false;
      },
      error: err => {
        console.error('Error loading property:', err);
        this.error = true;
        this.loading = false;
      },
    });
  }

  //==================================================================================================
  images: IImage[] = [];

  private prepareLightboxImages(): void {
    this.lightboxImages = this.propertyImages.map(propertyImage => {
      // Get the full image details from your images array
      const fullImage = this.images.find(
        img => img.id === propertyImage.PhotoId
      );

      return {
        src: fullImage?.photoUrl || 'assets/default-image.jpg',
        thumb: fullImage?.photoUrl || 'assets/default-image.jpg',
        caption: this.property.title,
      };
    });
  }

  isImageFullscreen = false;
  fullscreenImageRef: ElementRef | null = null;

  toggleFullscreen(): void {
    this.isImageFullscreen = !this.isImageFullscreen;

    if (this.isImageFullscreen) {
      const imgElement =
        this.elementRef.nativeElement.querySelector('.main-image');
      if (imgElement) {
        this.fullscreenImageRef = imgElement;
        if (imgElement.requestFullscreen) {
          imgElement.requestFullscreen();
          document.addEventListener('keydown', this.handleKeydown);
        } else if ((imgElement as any).webkitRequestFullscreen) {
          (imgElement as any).webkitRequestFullscreen();
        } else if ((imgElement as any).msRequestFullscreen) {
          (imgElement as any).msRequestFullscreen();
        }
      }
    } else {
      if (document.exitFullscreen) {
        document.exitFullscreen();
      } else if ((document as any).webkitExitFullscreen) {
        (document as any).webkitExitFullscreen();
      } else if ((document as any).msExitFullscreen) {
        (document as any).msExitFullscreen();
      }
    }
  }

  private handleKeydown = (e: KeyboardEvent) => {
    if (e?.key === 'Escape') this.toggleFullscreen();
    if (e?.key === 'ArrowLeft') this.prevImage();
    if (e?.key === 'ArrowRight') this.nextImage();
  };
  // Image gallery navigation
  nextImage() {
    this.activeImageIndex =
      (this.activeImageIndex + 1) % this.property.photos.length;
  }

  prevImage() {
    this.activeImageIndex =
      (this.activeImageIndex - 1 + this.property.photos.length) %
      this.property.photos.length;
  }

  // Handle fullscreen change events
  @HostListener('document:fullscreenchange')
  @HostListener('document:webkitfullscreenchange')
  @HostListener('document:msfullscreenchange')
  handleFullscreenChange() {
    this.isImageFullscreen = !!document.fullscreenElement;
    if (!this.isImageFullscreen) {
      this.fullscreenImageRef = null;
    }
  }

  // Clean up
  ngOnDestroy() {
    if (this.isImageFullscreen && document.exitFullscreen) {
      document.exitFullscreen();
    }
  }

  //========================favorite button====================================
  isFavorite = false;

  onFavoriteToggle(isFavorite: boolean) {
    this.isFavorite = isFavorite;
  }

  private extractItemsArray<T>(response: any): T[] {
    if (response?.items && Array.isArray(response.items)) {
      return response.items;
    }
    return [];
  }

  loadAmenitiesDataForPropertyInfo(propertyId: string) {
    forkJoin({
      propertyAmenities:
        this.propertyService.getPropertyAmenitiesById(propertyId),
      allAmenities: this.propertyService.getAllAmenities(),
      categories: this.propertyService.getAmenitiesCategories(),
    }).subscribe({
      next: ({ propertyAmenities, allAmenities, categories }) => {
        try {
          this.aminaties = propertyAmenities.body.items.map(
            (am: IPropertyAmenity) => am.name
          );
        } catch (error) {
          this.error = true;
        }
      },
      error: err => {
        this.error = true;
      },
    });
  }
}
