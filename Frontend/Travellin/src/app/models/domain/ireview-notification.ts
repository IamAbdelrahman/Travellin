export interface ReviewNotification {
  reviewId: string;
  bookingId: string;
  propertyTitle: string;
  reviewerName: string;
  rating: number;
  reviewText?: string;
  reviewDate: Date;
}