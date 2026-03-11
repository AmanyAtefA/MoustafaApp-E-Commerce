import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment.development';
import { Observable } from 'rxjs/internal/Observable';
import { BehaviorSubject, catchError, of, tap, throwError } from 'rxjs';
import { IAddItem } from '../IModels/IAddItem';
import { ICart } from '../IModels/iCart';
import { IUpdateQuantityItem } from '../IModels/IUpdateQuantityItem';

@Injectable({
  providedIn: 'root'
})
export class CartsService {

  private CartsSubject = new BehaviorSubject<ICart[]>([]);
  Carts$ = this.CartsSubject.asObservable();


  private userCartSubject = new BehaviorSubject<ICart | null>(null);
  userCart$ = this.userCartSubject.asObservable();
  constructor(private http: HttpClient) { }


  loadِCarts(): void {
    if (this.CartsSubject.value.length === 0) {
      this.refreshCarts().subscribe();
    }
  }

  refreshCarts(): Observable<ICart[]> {
    return this.http.get<ICart[]>(environment.baseUrl + "Cart/getAllCarts").pipe(
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


  getCartByUserIdFromToken(): Observable<ICart> {
    return this.http.get<ICart>(environment.baseUrl + 'Cart/GetCartByUserId/').pipe(

      tap(cart => {
        console.log('Cart By UserId', cart);
        this.userCartSubject.next(cart);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Cart By UserId', error);
        return of(null as any);
      })
    );
  }

  
  getCartById(id:number): Observable<ICart> {
    return this.http.get<ICart>(environment.baseUrl + 'Cart/GetCartById/' + id).pipe(
      tap(Cart => {
        console.log(' Cart By Id', Cart);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Cart By Id', error);
        alert('Error loading Cart By Id');
        return of(null as any);
      }
    ));
  }



  DeleteCart(id: number): Observable<ICart> {
    return this.http.delete<ICart>(environment.baseUrl + "Cart/DeleteCart/" + id).pipe(

      tap(() => {
        this.refreshCarts().subscribe();
        console.log('Cart is Deleted');
        alert("Cart is Deleted");
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error deleting Cart:', error);
        alert("Error in Deleting Cart");
        return throwError(() => error);
      })
    );
  }


  addItemToCart(item: IAddItem): Observable<ICart> {
    return this.http.post<ICart>(environment.baseUrl + "Cart/AddItem", item).pipe(

      tap(cart => {
        this.userCartSubject.next(cart);
      })

    );
  }

  removeItem(cartItemId: number): Observable<ICart> {
    return this.http.delete<ICart>(environment.baseUrl + "Cart/RemoveItem/" + cartItemId).pipe(

      tap(cart => {
        this.userCartSubject.next(cart);
      })

    );
  }

  
  updateQuantity(UpdateQuantityItem: IUpdateQuantityItem): Observable<ICart> {
    return this.http.put<ICart>(environment.baseUrl + "Cart/UpdateQuantity", UpdateQuantityItem).pipe(

      tap(cart => {
        this.userCartSubject.next(cart);
      })

    );
  }


  checkout(data: any) {
    return this.http.post('/api/checkout', data);
  }

}
