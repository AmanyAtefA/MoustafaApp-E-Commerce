import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { Observable } from 'rxjs/internal/Observable';
import { BehaviorSubject, catchError, of, tap, throwError } from 'rxjs';
import { ICartItem } from '../IModels/ICartItem';
import { ICart } from '../IModels/icart';

@Injectable({
  providedIn: 'root'
})
export class CartsService {

  private CartsSubject = new BehaviorSubject<ICart[]>([]);
  Carts$ = this.CartsSubject.asObservable();

  private Key = "Cart";
  private CartProduct: ICartItem[] =[];

  constructor(private http: HttpClient) { }


  loadProducts(): void {
    if (this.CartsSubject.value.length === 0) {
      this.refreshCarts().subscribe();
    }
  }

  refreshCarts(): Observable<ICart[]> {
    return this.http.get<ICart[]>(environment.baseUrl + "Article/getAllCarts").pipe(
      tap(Carts => {
        console.log('Loaded Carts:', Carts);
        this.CartsSubject.next(Carts)
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Carts:', error);
        alert('Error loading Carts');
        this.CartsSubject.next([]);
        return of([]);
      })
    );
  }


  getAllCarts(): Observable<ICart[]> {
    return this.http.get<ICart[]>(environment.baseUrl + 'Cart/getAllCarts');
  }

  GetCartsByUserId(UserId:string): Observable<ICart[]> {
    return this.http.get<ICart[]>(environment.baseUrl + 'Cart/GetCartsByUserId/' + UserId).pipe(
      tap(Cart => {
        console.log(' Cart By UserId', Cart);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Cart By UserId', error);
        alert('Error loading Cart By UserId');
        return of(null as any);
      }
      ));
  }

  
  getCartById(id:number): Observable<ICart> {
    return this.http.get<ICart>(environment.baseUrl + 'Cart/GetCartById/' + id).pipe(
      tap(Cart => {
        console.log(' Cart By Id', Cart);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Cart By UserId', error);
        alert('Error loading Cart By Id');
        return of(null as any);
      }
    ));
  }


  getCartFromLocalStorage():ICartItem[] {
    return JSON.parse(localStorage.getItem(this.Key) || "").pipe(
      tap(Cart => {
        console.log(' Cart From Local Storage', Cart);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading From Local Storage', error);
        alert('Error loading Cart From Local Storage');
        return of(null as any);
      }
      ));
  }
  

  setCartInLocalStorage(CartProduct: ICartItem[]){
    localStorage.setItem(this.Key, JSON.stringify(CartProduct))
  }

  

  DeleteCart(id: number): Observable<ICart>{
    return this.http.delete<ICart>(environment.baseUrl + "Cart/DeleteCart/" + id).pipe(
      tap(() => {
        this.refreshCarts().subscribe()
        console.log('Cart is Deleted'),
          alert("Cart is Deleted"),

          catchError((error: HttpErrorResponse) => {
            console.error('Error deleting Cart:', error);
            alert("Error in Deleting Cart")
            return throwError(() => error);
          })
      }
      ));
  }
  
}
