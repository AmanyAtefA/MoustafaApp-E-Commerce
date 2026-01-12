import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable } from 'rxjs/internal/Observable';
import { environment } from '../environments/environment.development';
import { IProduct } from '../IModels/Iproduct';
import { BehaviorSubject } from 'rxjs/internal/BehaviorSubject';
import { tap, catchError, of, throwError, map } from 'rxjs';
import { PagedResult } from '../IModels/paged-result';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }


  private ProductsSubject = new BehaviorSubject<IProduct[]>([]);
  Products$ = this.ProductsSubject.asObservable();


  private Page = 1;
  private PageSize = 4;


  loadProducts(): void {
    if (this.ProductsSubject.value.length === 0) {
      this.refreshProducts().subscribe();
    }
  }

  refreshProducts(): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(environment.baseUrl + "Product/GetAllProductsWithDetails").pipe(
      tap(Products => {
        console.log('Loaded Products:', Products);
        this.ProductsSubject.next(Products)
        this.loadProductNewArrivals(1, this.PageSize);

      }),
      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Products:', error);
        alert('Error loading Products');
        this.ProductsSubject.next([]);
        return of([]);
      })
    );
  }


  getAllProductsWithDetails(): Observable<IProduct[]> {
    return this.http.get<IProduct[]>(environment.baseUrl + 'Product/getAllProductsWithDetails').pipe(
      tap(Products => {
        console.log('Loaded All Products:', Products);
      }),
      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Products:', error);
        alert('Error loading Products');
        return of([] as IProduct[]);
      }
      ));
  }

  getProductyByIdWithDetails(id: number): Observable<IProduct> {
    return this.http.get<IProduct>(environment.baseUrl + "Product/getProductyByIdWithDetails/" + id).pipe(
      tap(Product => {
        console.log(' Product By Id', Product);
      }),
      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Category:', error);
        alert('Error loading Category');
        return of(null as any);
      }
      ));
  }



  CreateProduct(product: IProduct): Observable<IProduct> {
    return this.http.post<IProduct>(environment.baseUrl + 'Product/CreateProduct', product).pipe(
      tap(() => {
        this.refreshProducts().subscribe(),
          console.log('Product added'),
          alert("Product added")       
      }),
      catchError((error: HttpErrorResponse) => {
          console.error('Error adding Category:', error);
          alert("Error in adding Category")
          return throwError(() => error);
        })
    );
  }


  UpdateProduct(id: number, formData: FormData): Observable<any> {

    return this.http.put(environment.baseUrl + 'Product/UpdateProduct/' + id, formData).pipe(
      tap(() => {
        this.refreshProducts().subscribe(),
          console.log('Product Update:'),
          alert("Product Updated")

      }),

          catchError((error: HttpErrorResponse) => {
            console.error('Error updating Product:', error);
            alert("Error in Updating Product")
            return throwError(() => error);
          })
    );
  }


  DeleteProduct(id: number): Observable<IProduct> {

    return this.http.delete<IProduct>(environment.baseUrl + 'Product/DeleteProduct/' + id).pipe(
      tap(() => {
        this.refreshProducts().subscribe()
        console.log('Product is Deleted'),
          alert("Product is Deleted")

       }),
      catchError((error: HttpErrorResponse) => {
            console.error('Error deleting Product:', error);
            alert("Error in Deleting Product")
            return throwError(() => error);
          }));
  }



  loadProductNewArrivals(page?: number, pageSize?: number):Observable<PagedResult<IProduct>> {

    if (page)
      this.Page = page;
    if (pageSize)
      this.PageSize = pageSize;

    const params = { page: this.Page, pageSize: this.PageSize };

    return this.http.get<PagedResult<IProduct>>(
      environment.baseUrl + 'Product/GetAllProductsPagedinationaNewArrivals/',
        { params }
      )
      .pipe(
        tap(res => {
          console.log('Loaded NewArrivals:', res);
        }),
        catchError(err => {
          console.error('New Arrivals Error', err);
          alert('Error in Loading NewArrivals');
          return throwError(() => err);
        }),
    )
  }

  
  




  //// لفلترة البرودكت  ب createdAt Desو Take  8

  //getProductByFilterCreatedAt(limitTake = 8) {
  //  return this.Products$.pipe(
  //    map(products =>
  //      [...products]
  //        .filter(p => p.createdAt)
  //        .sort(
  //          (a, b) =>
  //            new Date(b.createdAt).getTime() -
  //            new Date(a.createdAt).getTime()
  //        )
  //        .slice(0, limitTake)
  //    )
  //  );
  //}


  //// لفلترة البرودكت  ب اوبجيكت {filter?,sort?,limit?}

  //getProductsByGeniricFilterAndSortAndTake(options?: {
  //  filter?: (p: IProduct) => boolean;
  //  sort?: (a: IProduct, b: IProduct) => number;
  //  limit?: number;
  //}) {
  //  return this.Products$.pipe(
  //    map(products => {
  //      let result = [...products];

  //      if (options?.filter) {
  //        result = result.filter(options.filter);
  //      }

  //      if (options?.sort) {
  //        result = result.sort(options.sort);
  //      }

  //      if (options?.limit) {
  //        result = result.slice(0, options.limit);
  //      }

  //      return result;
  //    })
  //  );
  //}


}

