import { IReview } from "./IReview";
import { ReviewStats } from "./ReviewStats";
import { PagedResult } from "./pagedResult";


export interface ProductReviewsResponse {
  stats: ReviewStats;
  reviews: PagedResult<IReview>;
}
