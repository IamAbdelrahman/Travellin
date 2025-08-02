import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Review, CreateReview } from '../models/api/request/review.model';

@Injectable({
  providedIn: 'root'
})
export class ReviewService {
  private apiUrl = 'api/v1/reviews';

  constructor(private http: HttpClient) { }

  getAllReviews(): Observable<Review[]> {
    return this.http.get<Review[]>(this.apiUrl);
  }

  getReviewById(id: string): Observable<Review> {
    return this.http.get<Review>(`${this.apiUrl}/${id}`);
  }

  getReviewsByBookingId(bookingId: string): Observable<Review[]> {
    return this.http.get<Review[]>(`${this.apiUrl}/booking/${bookingId}`);
  }

  createReview(review: CreateReview): Observable<Review> {
    return this.http.post<Review>(this.apiUrl, review);
  }

  updateReview(id: string, review: Review): Observable<void> {
    return this.http.put<void>(`${this.apiUrl}/${id}`, review);
  }

  deleteReview(id: string): Observable<void> {
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}
