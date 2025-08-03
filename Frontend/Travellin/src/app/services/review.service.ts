import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Review, CreateReview, ReviewType, ReviewStatus } from '../models/api/request/review.model';
import { ApiConstant } from '../utils/api-constant.util';

export interface ReviewPeriod {
  bookingId: string;
  checkOutDate: string;
  reviewPeriodStart: string;
  reviewPeriodEnd: string;
  canReviewAsGuest: boolean;
  canReviewAsHost: boolean;
  hasGuestReview: boolean;
  hasHostReview: boolean;
  daysRemaining: number;
}

export interface PropertyRating {
  averageRating: number;
  reviewCount: number;
}

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  constructor(private http: HttpClient) { }

  // Basic CRUD operations
  getAllReviews(): Observable<Review[]> {
    return this.http.get<Review[]>(ApiConstant.reviews.getAll);
  }

  getReviewById(id: string): Observable<Review> {
    const url = ApiConstant.reviews.getById.replace('{id}', id);
    return this.http.get<Review>(url);
  }

  getReviewsByBookingId(bookingId: string): Observable<Review[]> {
    const url = ApiConstant.reviews.getByBooking.replace('{bookingId}', bookingId);
    return this.http.get<Review[]>(url);
  }

  createReview(review: CreateReview): Observable<Review> {
    return this.http.post<Review>(ApiConstant.reviews.create, review);
  }

  updateReview(id: string, review: Review): Observable<void> {
    const url = ApiConstant.reviews.update.replace('{id}', id);
    return this.http.put<void>(url, review);
  }

  deleteReview(id: string): Observable<void> {
    const url = ApiConstant.reviews.delete.replace('{id}', id);
    return this.http.delete<void>(url);
  }

  // Enhanced review system methods
  getPropertyReviews(propertyId: string, page: number = 1, pageSize: number = 10): Observable<Review[]> {
    const url = ApiConstant.reviews.getPropertyReviews.replace('{propertyId}', propertyId);
    return this.http.get<Review[]>(`${url}?page=${page}&pageSize=${pageSize}`);
  }

  getPropertyRating(propertyId: string): Observable<PropertyRating> {
    const url = ApiConstant.reviews.getPropertyRating.replace('{propertyId}', propertyId);
    return this.http.get<PropertyRating>(url);
  }

  getUserReviews(userId: string, type: ReviewType): Observable<Review[]> {
    const url = ApiConstant.reviews.getUserReviews.replace('{userId}', userId);
    return this.http.get<Review[]>(`${url}?type=${type}`);
  }

  getReviewPeriod(bookingId: string): Observable<ReviewPeriod> {
    const url = ApiConstant.reviews.getReviewPeriod.replace('{bookingId}', bookingId);
    return this.http.get<ReviewPeriod>(url);
  }

  canReview(bookingId: string, type: ReviewType): Observable<{ canReview: boolean }> {
    const url = ApiConstant.reviews.canReview.replace('{bookingId}', bookingId);
    return this.http.get<{ canReview: boolean }>(`${url}?type=${type}`);
  }

  publishReview(reviewId: string): Observable<void> {
    const url = ApiConstant.reviews.publish.replace('{id}', reviewId);
    return this.http.post<void>(url, {});
  }

  hideReview(reviewId: string): Observable<void> {
    const url = ApiConstant.reviews.hide.replace('{id}', reviewId);
    return this.http.post<void>(url, {});
  }
}
