import { Component, EventEmitter, Input, OnInit, Output } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { Review, CreateReview } from '../../../models/api/request/review.model';

@Component({
  selector: 'app-review-form',
  templateUrl: './review-form.component.html',
  styleUrls: ['./review-form.component.css']
})
export class ReviewFormComponent implements OnInit {
  @Input() bookingId!: string;
  @Input() review?: Review;
  @Output() submitReview = new EventEmitter<CreateReview>();

  reviewForm!: FormGroup;
  isEditing = false;

  constructor(private fb: FormBuilder) { }

  ngOnInit(): void {
    this.isEditing = !!this.review;

    this.reviewForm = this.fb.group({
      comment: [this.review?.comment || '', [Validators.required, Validators.maxLength(500)]],
      cleanliness: [this.review?.cleanliness || 5, [Validators.required, Validators.min(1), Validators.max(5)]],
      accuracy: [this.review?.accuracy || 5, [Validators.required, Validators.min(1), Validators.max(5)]],
      checkIn: [this.review?.checkIn || 5, [Validators.required, Validators.min(1), Validators.max(5)]],
      communication: [this.review?.communication || 5, [Validators.required, Validators.min(1), Validators.max(5)]],
      location: [this.review?.location || 5, [Validators.required, Validators.min(1), Validators.max(5)]],
      value: [this.review?.value || 5, [Validators.required, Validators.min(1), Validators.max(5)]]
    });
  }

  onSubmit(): void {
    if (this.reviewForm.valid) {
      const reviewData: CreateReview = {
        bookingId: this.bookingId,
        ...this.reviewForm.value
      };
      this.submitReview.emit(reviewData);
    }
  }
}
