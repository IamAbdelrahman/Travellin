import { Injectable } from '@angular/core';
import { BehaviorSubject } from 'rxjs';
import { Listing } from '../models/listing'; // Assuming you have a Listing model defined
@Injectable({ providedIn: 'root' })
export class ListingCreationService {
  private listingSubject = new BehaviorSubject<Partial<Listing>>({});
  currentListing$ = this.listingSubject.asObservable();

  updateListing(update: Partial<Listing>) {
    const current = this.listingSubject.value;
    this.listingSubject.next({ ...current, ...update });
  }

  getCurrentListing() {
    return this.listingSubject.value;
  }
}
