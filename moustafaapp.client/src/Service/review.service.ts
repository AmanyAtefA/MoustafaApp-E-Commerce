import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap, catchError, of, throwError } from 'rxjs';
import { environment } from '../environments/environment';
import { IReview } from '../IModels/IReview';
import { ProductReviewsResponse } from '../IModels/ProductReviewsResponse';


@Injectable({
  providedIn: 'root'
})
export class ReviewService {
 constructor(private http: HttpClient) { }


  getAllReviews(): Observable<IReview[]> {
    return this.http.get<IReview[]>(environment.baseUrl + 'Review/GetAllReviews').pipe(
      tap(Reviews => {
        console.log('Loaded All Reviews:', Reviews);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Reviews:', error);
        alert('Error loading Reviews');
        return of([] as IReview[]);
      })
    );
  }


  getReviewsByProductId(id: number): Observable<IReview[]> {

    return this.http.get<IReview[]>(environment.baseUrl + 'Review/GetReviewsByProductId/' + id)
      .pipe(
        tap(res =>
          console.log('Loaded Reviews By Product Id:', res)),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Reviews:', error);
        alert('Error loading Reviews');
        return of([]);
      })
    );
  }


  createReview(data: any): Observable<IReview> {

    return this.http.post<IReview>(environment.baseUrl + 'Review/CreateReview/', data)
      .pipe(
      tap(() => {
        alert('Review added');
      }),

      catchError(err => {
        alert('Error adding Review');
        return throwError(() => err);
      })
    );
  }


  updateReview(id: number, data: { rating: number; reviewText?: string }): Observable<void> {

    return this.http.put<void>(environment.baseUrl + 'Review/UpdateReview/' + id, data)
      .pipe(
      tap(() => {
        alert('Review Updated');
      }),

      catchError(err => {
        alert('Error updating Review');
        return throwError(() => err);
      })
    );
  }


  deleteReview(id: number): Observable<void> {

    return this.http.delete<void>(environment.baseUrl + 'Review/DeleteReview/' + id).pipe(
      tap(() => {
        alert('Review Deleted');
      }),

      catchError(err => {
        alert('Error deleting Review');
        return throwError(() => err);
      })
    );
  }


  private Page = 1;
  private PageSize = 5;

  loadProductReviews
    (productId: number, page?: number, pageSize?: number): Observable<ProductReviewsResponse> {

    if (page)
      this.Page = page;

    if (pageSize)
      this.PageSize = pageSize;

    const params = {
      pageNumber: this.Page,
      pageSize: this.PageSize
    };

    return this.http.get<ProductReviewsResponse>(
      environment.baseUrl + `Review/GetReviewsByProductId/${productId}`,
      { params }).pipe(
      tap(res => {
        console.log('Loaded Product Reviews:', res);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading reviews:', error);
        alert('Error loading reviews');
        return throwError(() => error);
      })
    );
  }

}
