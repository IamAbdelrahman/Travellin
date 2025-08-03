// import { Component, OnInit } from '@angular/core';
// import { ActivatedRoute } from '@angular/router';
// import { ReviewService } from '../../../services/review.service';
// import { Review } from '../../../models/api/request/review.model';

// @Component({
//   selector: 'app-review-detail',
//   templateUrl: './review-detail.component.html',
//   styleUrls: ['./review-detail.component.css']
// })
// export class ReviewDetailComponent implements OnInit {
//   review?: Review;
//   loading = false;
//   error = '';

//   constructor(
//     private route: ActivatedRoute,
//     private reviewService: ReviewService
//   ) { }

//   ngOnInit(): void {
//     const id = this.route.snapshot.paramMap.get('id');
//     if (id) {
//       this.loadReview(id);
//     }
//   }

//   loadReview(id: string): void {
//     this.loading = true;
//     this.reviewService.getReviewById(id).subscribe({
//       next: (review) => {
//         this.review = review;
//         this.loading = false;
//       },
//       error: (err) => {
//         this.error = 'Failed to load review details';
//         this.loading = false;
//         console.error(err);
//       }
//     });
//   }
// }
