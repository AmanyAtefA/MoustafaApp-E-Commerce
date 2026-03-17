import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap, catchError, of, throwError } from 'rxjs';
import { IBrand } from '../IModels/IBrand';
import { environment } from '../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class BrandService {


  private BrandsSubject = new BehaviorSubject<IBrand[]>([]);
  Brands$ = this.BrandsSubject.asObservable();
  constructor(private http: HttpClient) { }


  loadBrands(): void {
    if (this.BrandsSubject.value.length === 0) {
      this.refreshBrands().subscribe();
    }
  }

  refreshBrands(): Observable<IBrand[]> {
    return this.http.get<IBrand[]>(environment.baseUrl + "Brand/GetAllBrands").pipe(
      tap(Brands => {
        console.log('Loaded Brands:', Brands);
        this.BrandsSubject.next(Brands)
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Brands:', error);
        alert('Error loading Brands');
        this.BrandsSubject.next([]);
        return of([]);
      })
    );
  }



  getAllBrands(): Observable<IBrand[]> {
    return this.http.get<IBrand[]>(environment.baseUrl + 'Brand/getAllBrands').pipe(
      tap(Brands => {
        console.log('Loaded Brands With Products:', Brands);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Brands:', error);
        alert('Error loading Brands');
        return of([]);
      }));
  }


  getBrandById(id: number): Observable<IBrand> {
    return this.http.get<IBrand>(environment.baseUrl + 'Brandt/getBrandById/' + id).pipe(
      tap(Brand => {
        console.log('Loaded Brand By Id:', Brand);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Brand:', error);
        alert('Error loading Brand');
        return of(null as any);
      }
      ));
  }


  DeleteBrand(id: number): Observable<IBrand> {
    return this.http.delete<IBrand>(environment.baseUrl + 'Brand/DeleteBrand/' + id).pipe(
      tap(() => {
        this.refreshBrands().subscribe()
        console.log('Brand is Deleted'),
          alert("Brand is Deleted"),

          catchError((error: HttpErrorResponse) => {
            console.error('Error deleting Brand:', error);
            alert("Error in Deleting Brand")
            return throwError(() => error);
          })
      }
      ));
  }

}
