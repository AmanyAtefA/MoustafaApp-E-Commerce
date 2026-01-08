import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { ICategory } from '../IModels/Icategory';
import { environment } from '../environments/environment.development';
import { BehaviorSubject, Observable, catchError, of, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CategoriesService {

  private CategoriesSubject = new BehaviorSubject<ICategory[]>([]);
  Categories$ = this.CategoriesSubject.asObservable();
  constructor(private http: HttpClient) { }


  loadCategories(): void {
    if (this.CategoriesSubject.value.length === 0) {
      this.refreshCategories().subscribe();
    }
  }

  refreshCategories(): Observable<ICategory[]> {
    return this.http.get<ICategory[]>(environment.baseUrl + "Article/getAllCategoriesWithProducts").pipe(
      tap(Categories => {
        console.log('Loaded Categories:', Categories);
        this.CategoriesSubject.next(Categories)
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Categories:', error);
        alert('Error loading Categories');
        this.CategoriesSubject.next([]);
        return of([]);
      })
    );
  }


  GetAllCategories(): Observable<ICategory[]> {
    return this.http.get<ICategory[]>(environment.baseUrl + 'Category/getAllCategories').pipe(
      tap(Categoris => {
        console.log('Loaded All Categories:', Categoris);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Categories:', error);
        alert('Error loading Categories');
        return of([] as ICategory[]);
      })
    );
  }

  getAllCategoriesWithProducts(): Observable<ICategory[]> {
    return this.http.get<ICategory[]>(environment.baseUrl + 'Category/getAllCategoriesWithProducts').pipe(
      tap(Categoris => {
        console.log('Loaded Categories With Products ThenInclude:', Categoris);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Categories:', error);
        alert('Error loading Categories');
        return of([]);
      }));
  }


   getCategoryById (id: number): Observable<ICategory> {
     return this.http.get<ICategory>(environment.baseUrl + 'Category/getCategoryById/' + id).pipe(
       tap(Category => {
         console.log('Loaded Category By Id:', Category);
       }),

       catchError((error: HttpErrorResponse) => {
         console.error('Error loading Category:', error);
         alert('Error loading Category');
         return of(null as any);
       }
     ));
  }

  CreateCategory(Category: object): Observable<ICategory> {
    return this.http.post<ICategory>(environment.baseUrl + 'Category/CreateCategory/', Category).pipe(
      tap(() => {
        this.refreshCategories().subscribe(),
          console.log('Category added'),
          alert("Category added")

        catchError((error: HttpErrorResponse) => {
          console.error('Error adding Category:', error);
          alert("Error in adding Category")
          return throwError(() => error);
        })
      }
    ));
  }
  

  DeleteCategory(id: number): Observable<ICategory> {
    return this.http.delete<ICategory>(environment.baseUrl + 'Category/DeleteCategory/' + id).pipe(
      tap(() => {
        this.refreshCategories().subscribe()
        console.log('Category is Deleted'),
          alert("Category is Deleted"),

      catchError((error: HttpErrorResponse) => {
        console.error('Error deleting Category:', error);
        alert("Error in Deleting Category")
        return throwError(() => error);
      })
    }
    ));
  }


  UpdateCategory(id: number, category: ICategory): Observable<ICategory> {
    return this.http.put<ICategory>(environment.baseUrl + 'Category/UpdateCategory/' + id, category).pipe(
      tap(() => {
        this.refreshCategories().subscribe(),
          console.log('Category Update:'),
          alert("Category Updated"),

      catchError((error: HttpErrorResponse) => {
        console.error('Error updating Category:', error);
        alert("Error in Updating Category")
        return throwError(() => error);
        })
      }
    ));
  }
  

}
