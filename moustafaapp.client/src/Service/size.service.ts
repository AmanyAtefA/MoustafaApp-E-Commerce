import { Injectable } from '@angular/core';
import { ISizes } from '../IModels/ISizes';
import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { BehaviorSubject, Observable, tap, catchError, of } from 'rxjs';
import { environment } from '../environments/environment.development';

@Injectable({
  providedIn: 'root'
})
export class SizeService {

  constructor(private http: HttpClient) { }


  private SizesSubject = new BehaviorSubject<ISizes[]>([]);
  Sizes$ = this.SizesSubject.asObservable();


  getAllSizes(): Observable<ISizes[]> {
    return this.http.get<ISizes[]>(environment.baseUrl + 'Size/GetAllSizes').pipe(
      tap(sizes => this.SizesSubject.next(sizes)),
      catchError(() => {
        this.SizesSubject.next([]);
        return of([]);
      })
    );
  }


}
