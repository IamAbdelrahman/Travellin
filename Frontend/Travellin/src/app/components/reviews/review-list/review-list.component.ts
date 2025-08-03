// import { Component, OnInit } from '@angular/core';
// import { ReviewService } from '../../../services/review.service';
// import { Review } from '../../../models/api/request/review.model';

// @Component({
//   selector: 'app-review-list',
//   templateUrl: './review-list.component.html',
//   styleUrls: ['./review-list.component.css']
// })
// export class ReviewListComponent implements OnInit {
//   reviews: Review[] = [];
//   loading = false;
//   error = '';

//   constructor(private reviewService: ReviewService) { }

//   ngOnInit(): void {
//     this.loadReviews();
//   }

//   loadReviews(): void {
//     this.loading = true;
//     this.reviewService.getAllReviews().subscribe({
//       next: (reviews) => {
//         this.reviews = reviews;
//         this.loading = false;
//       },
//       error: (err) => {
//         this.error = 'Failed to load reviews';
//         this.loading = false;
//         console.error(err);
//       }
//     });
//   }

//   deleteReview(id: string): void {
//     if (confirm('Are you sure you want to delete this review?')) {
//       this.reviewService.deleteReview(id).subscribe({
//         next: () => {
//           this.reviews = this.reviews.filter(r => r.id !== id);
//         },
//         error: (err) => {
//           console.error('Failed to delete review:', err);
//         }
//       });
//     }
//   }
// }
