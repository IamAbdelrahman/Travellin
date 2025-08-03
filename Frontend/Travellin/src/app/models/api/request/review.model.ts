export enum ReviewType {
  Guest = 'Guest',
  Host = 'Host'
}

export enum ReviewStatus {
  Pending = 'Pending',
  Submitted = 'Submitted',
  Published = 'Published',
  Hidden = 'Hidden',
  Expired = 'Expired'
}

export interface Reviewer {
  id: string;
  firstName: string;
  lastName: string;
  photoUrl: string;
}

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
  
  // Enhanced review system fields
  type: ReviewType;
  status: ReviewStatus;
  reviewPeriodStart?: Date;
  reviewPeriodEnd?: Date;
  isPublic: boolean;
  isAnonymous: boolean;
  
  reviewer?: Reviewer;
  avg?: number;
}

export interface CreateReview {
  bookingId: string;
  userId?: string; // Optional - will be set by backend from JWT token
  comment: string;
  cleanliness: number;
  accuracy: number;
  checkIn: number;
  communication: number;
  location: number;
  value: number;
  
  // Enhanced review system fields
  type: ReviewType;
  isAnonymous?: boolean;
}
