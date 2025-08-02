export interface Review {
  id: string;
  bookingId: string;
  comment: string;
  cleanliness: number;
  accuracy: number;
  checkIn: number;
  communication: number;
  location: number;
  value: number;
  createdAt: Date;
  updatedAt: Date;
}

export interface CreateReview {
  bookingId: string;
  comment: string;
  cleanliness: number;
  accuracy: number;
  checkIn: number;
  communication: number;
  location: number;
  value: number;
}
