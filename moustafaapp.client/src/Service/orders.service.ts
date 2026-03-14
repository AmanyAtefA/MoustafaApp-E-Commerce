import { Injectable } from '@angular/core'; import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { environment } from '../environments/environment.development';
import { Observable } from 'rxjs';
import { IOrder } from '../IModels/IOrder';
import { catchError, of, tap, throwError } from 'rxjs';
@Injectable({
  providedIn: 'root'
})
export class OrdersService {

  constructor(private http: HttpClient) { }




  getOrderById(orderId: number): Observable<IOrder> {
    return this.http.get<IOrder>(environment.baseUrl + "Order/GetOrderById/" + orderId).pipe(
      tap(Order => {
        console.log(' Order By Id', Order);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Order By Id', error);
        alert('Error loading Order By Id');
        return of(null as any);
      }
      ));
  }

  getOrderByUserID(UserID: string): Observable<IOrder> {
    return this.http.get<IOrder>(environment.baseUrl + "Order/GetOrderByUserID/" + UserID).pipe(
      tap(Order => {
        console.log(' Order By UserID', Order);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Order By UserID', error);
        alert('Error loading Order By UserID');
        return of(null as any);
      }
      ));
  }


}
