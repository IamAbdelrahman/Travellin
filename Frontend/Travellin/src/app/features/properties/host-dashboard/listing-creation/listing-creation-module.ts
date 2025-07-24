import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ListingCreationContainer } from './pages/listing-creation-container/listing-creation-container.component';
import { PropertyType } from './components/property-type/property-type.component';
import { PlaceType } from './components/place-type/place-type.component';
import { Location } from './components/location/location.component';
import { Basics } from './components/basics/basics.component';
import { Amenities } from './components/amenities/amenities.component';
import { Photos } from './components/photos/photos.component';
import { Title } from './components/title/title.component';
import { Pricing } from './components/pricing/pricing.component';
import { Discounts } from './components/discounts/discounts.component';
import { Review } from './components/review/review.component';

const routes: Routes = [
  {
    path: '',
    component: ListingCreationContainer,
    children: [
      { path: 'property-type', component: PropertyType },
      { path: 'place-type', component: PlaceType },
      { path: 'location', component: Location },
      { path: 'basics', component: Basics },
      { path: 'amenities', component: Amenities },
      { path: 'photos', component: Photos },
      { path: 'title', component: Title },
      { path: 'pricing', component: Pricing },
      { path: 'discounts', component: Discounts },
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
