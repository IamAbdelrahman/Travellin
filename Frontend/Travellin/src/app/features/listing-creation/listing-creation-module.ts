import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListingCreationContainerComponent } from './pages/listing-creation-container/listing-creation-container.component';
import { PlaceType } from './components/place-type/place-type.component';
import { BasicsComponent } from './components/basics/basics.component';
import { AmenitiesComponent } from './components/amenities/amenities.component';
import {PhotosComponent } from './components/photos/photos.component';
import { Title } from './components/title/title.component';
import { Pricing } from './components/pricing/pricing.component';
import { DiscountsComponent } from './components/discounts/discounts.component';
import { Review } from './components/review/review.component';
import { PropertyTypeComponent } from './components/property-type/property-type.component';
import { BehaviorSubject } from 'rxjs';
import { ListingCreationService } from './services/listing-creation.service';
import { Observable } from 'rxjs';
import {LocationComponent} from './components/location/location.component';
const routes: Routes = [
  {
    path: '',
    component: ListingCreationContainerComponent,
    children: [
      { path: 'property-type', component: PropertyTypeComponent },
      { path: 'place-type', component: PlaceType },
      { path: 'location', component: LocationComponent },
      { path: 'basics', component: BasicsComponent },
      { path: 'amenities', component: AmenitiesComponent },
      { path: 'photos', component: PhotosComponent },
      { path: 'title', component: Title },
      { path: 'pricing', component: Pricing },
      { path: 'discounts', component: DiscountsComponent },
      { path: 'review', component: Review },  
      { path: '', redirectTo: 'property-type', pathMatch: 'full' },
      { path: '**', redirectTo: 'property-type' }
    ]
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class ListingCreationRoutingModule { }
