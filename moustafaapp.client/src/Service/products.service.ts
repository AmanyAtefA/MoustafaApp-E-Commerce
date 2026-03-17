import { HttpClient, HttpErrorResponse, HttpParams } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, BehaviorSubject } from 'rxjs';
import { environment } from '../environments/environment';
import { IProduct } from '../IModels/Iproduct';
import { tap, catchError, of, throwError, map } from 'rxjs';
import { IProductFilter } from '../IModels/IProductFilter';
import { emptyPagedResult } from '../Helper/PaginationEmptyHelper';
import { PagedResult } from '../IModels/pagedResult';
import { ProductPreset } from '../IModels/Enum/ProductPreset';

@Injectable({
  providedIn: 'root'
})
export class ProductsService {

  constructor(private http: HttpClient) { }


  private ProductsSubject = new BehaviorSubject<PagedResult<IProduct>>(emptyPagedResult<IProduct>());
  Products$ = this.ProductsSubject.asObservable();

  private NewArrivalsSubject = new BehaviorSubject<PagedResult<IProduct>>(emptyPagedResult<IProduct>());
  NewArrivals$ = this.NewArrivalsSubject.asObservable();

  private TopSellingSubject = new BehaviorSubject<PagedResult<IProduct>>(emptyPagedResult<IProduct>());
  TopSelling$ = this.TopSellingSubject.asObservable();

  private Page = 1;
  private PageSize = 4;


  loadProducts(filter: IProductFilter): void {

    if (this.ProductsSubject.value.totalCount === 0) {

      this.GetProductsWithFilter(filter).subscribe();

    }
  }


  refreshProducts(): Observable<PagedResult<IProduct>> {
    return this.http.get<PagedResult<IProduct>>(environment.baseUrl + "Product/GetAllProductsWithDetails").pipe(
      tap(Products => {
        console.log('Loaded Products:', Products);
        this.ProductsSubject.next(Products)
        this.loadProductNewArrivals(1, this.PageSize);

      }),
      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Products:', error);
        alert('Error loading Products');
        return of(emptyPagedResult<IProduct>());
      })
    );
  }


  GetProductsWithFilter(filter: IProductFilter) {

    let params = new HttpParams();

    Object.entries(filter).forEach(([key, value]) => {
      if (value !== null && value !== undefined && value !== '') {
        params = params.append(key, value.toString());
      }
    });

    return this.http.get<PagedResult<IProduct>>(environment.baseUrl + 'Product/GetProductsWithFilter', { params }).pipe(
      tap(products => {

        if (filter.preset === ProductPreset.NewArrivals) {
          this.NewArrivalsSubject.next(products);
        }

        else if (filter.preset === ProductPreset.BestSeller) {
          this.TopSellingSubject.next(products);
        }

        if (filter.onSale) {
          params = params.set('onSale', filter.onSale);
        }

        else {
          this.ProductsSubject.next(products);
        }
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Products:', error);
        alert('Error loading Products');
        return of(emptyPagedResult<IProduct>());
      }
      ));
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
    return this.http.get<IProduct>(environment.baseUrl + "Product/GetProductyByIdWithDetails/" + id).pipe(
      tap(Product => {
        console.log(' Product By Id', Product);
      }),
      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Product:', error);
        alert('Error loading Product');
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
      environment.baseUrl + 'Product/GetAllProductsNewArrivalsAsync/',
        { params } )
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



}

