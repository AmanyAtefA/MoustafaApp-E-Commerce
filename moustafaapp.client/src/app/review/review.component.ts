import { Component, Input } from '@angular/core';
import { IReview } from '../../IModels/IReview';
import { ReviewStats } from '../../IModels/ReviewStats';
import { ReviewService } from '../../Service/review.service';
import { ProductsService } from '../../Service/products.service';
import { Observable } from 'rxjs';
import { IProduct } from '../../IModels/Iproduct';
import { PagedResult } from '../../IModels/paged-result';

@Component({
  selector: 'app-review',
  templateUrl: './review.component.html',
  styleUrl: './review.component.css'
})
export class ReviewComponent {


  open = false;

  reviews: IReview[] = [];
  stats!: ReviewStats;

  pageNumber = 1;
  totalPages = 0;

  @Input() showPagination = true;
  @Input() pageSize = 8;
  page = 1;


  @Input() productId!: number;

  newArrivals$!: Observable<PagedResult<IProduct>>;
  constructor(private reviewService: ReviewService ) { }


 ngOnInit(): void {
    
  }

  ngOnChanges() {
    if (this.productId) {
      this.loadReviews();
    }
  }


  loadReviews() {
    this.reviewService
      .getReviewsByProductId(this.productId)
      .subscribe(res => {
        console.log(res);
        this.reviews = res
      });
  }


  onPageChange(page: number) {
    this.page = page;
    this.loadReviews();
  }

 
}
