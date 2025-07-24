import { Component } from '@angular/core';
import { Router, ActivatedRoute, RouterOutlet, UrlSegment } from '@angular/router';
import { CommonModule } from '@angular/common';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-listing-creation-container',
  standalone: true,
  imports: [CommonModule, RouterOutlet], // Add RouterOutlet here
  templateUrl: './listing-creation-container.component.html',
  styleUrls: ['./listing-creation-container.component.css']
})
export class ListingCreationContainerComponent {
  // Define all steps in order
  steps = [
    { id: 1, path: 'property-type', title: 'Property Type' },
    { id: 2, path: 'place-type', title: 'Place Type' },
    { id: 3, path: 'location', title: 'Location' },
    { id: 4, path: 'basics', title: 'Basics' },
    { id: 5, path: 'amenities', title: 'Amenities' },
    { id: 6, path: 'photos', title: 'Photos' },
    { id: 7, path: 'title', title: 'Title' },
    { id: 8, path: 'pricing', title: 'Pricing' },
    { id: 9, path: 'discounts', title: 'Discounts' },
    { id: 10, path: 'review', title: 'Review' }
  ];
  currentStep$: Observable<UrlSegment[]> | undefined;
  currentStepIndex = 0;
  progressPercentage = 0;

  constructor(private router: Router, private route: ActivatedRoute) {}

  ngOnInit() {
    this.currentStep$ = this.route.firstChild?.url;
    this.route.firstChild?.url.subscribe(segments => {
      const currentPath = segments[0]?.path;
      this.currentStepIndex = this.steps.findIndex(step => step.path === currentPath);
      this.calculateProgress();
    });
  }

  calculateProgress() {
    this.progressPercentage = ((this.currentStepIndex + 1) / this.steps.length) * 100;
  }

  navigateToStep(index: number) {
    if (index >= 0 && index < this.steps.length) {
      this.router.navigate([this.steps[index].path], { relativeTo: this.route });
    }
  }

  getProgressText(): string {
    return `Step ${this.currentStepIndex + 1} of ${this.steps.length}`;
  }
}
