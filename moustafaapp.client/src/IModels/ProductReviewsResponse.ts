import { IReview } from "./IReview";
import { ReviewStats } from "./ReviewStats";
import { PagedResult } from "./paged-result";


export interface ProductReviewsResponse {
  stats: ReviewStats;
  reviews: PagedResult<IReview>;
}
