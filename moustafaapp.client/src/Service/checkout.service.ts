import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { environment } from '../environments/environment';
import { IAddress } from '../IModels/IAddress';
import { Observable } from 'rxjs';
import { IOrder } from '../IModels/IOrder';
import { catchError, of, tap, throwError } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class CheckoutService {

  constructor(private http: HttpClient) { }


  checkout(data: IAddress) {
    return this.http.post(environment.baseUrl + "Cart/checkout", data)
  }


  getOrderBrId(orderId: number): Observable<IOrder> {
    return this.http.get<IOrder>(environment.baseUrl + "Cart/GetOrderBrId" + orderId).pipe(
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
}
