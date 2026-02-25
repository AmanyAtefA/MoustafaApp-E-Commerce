
export interface IReview {
  reviewId: number;
  productId: number;
  productName: string;
  rating: number;
  reviewText?: string;
  userId?: string;
  userName?: string;
  fullName?: string;
  datePosted: string;
  updatedAt: string;
}
