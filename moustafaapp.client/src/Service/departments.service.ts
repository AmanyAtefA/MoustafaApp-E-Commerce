import { HttpClient, HttpErrorResponse } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { BehaviorSubject, Observable, tap, catchError, of, throwError } from 'rxjs';
import { environment } from '../environments/environment.development';
import { IDepartment } from '../IModels/IDepartment';

@Injectable({
  providedIn: 'root'
})
export class DepartmentsService {

 

  private DepartmentsSubject = new BehaviorSubject<IDepartment[]>([]);
  Departments$ = this.DepartmentsSubject.asObservable();
  constructor(private http: HttpClient) { }


  loadDepartments(): void {
    if (this.DepartmentsSubject.value.length === 0) {
      this.refreshDepartments().subscribe();
    }
  }

  refreshDepartments(): Observable<IDepartment[]> {
    return this.http.get<IDepartment[]>(environment.baseUrl + "Department/GetAllDepartmentsWithProducts").pipe(
      tap(Departments => {
        console.log('Loaded Departments:', Departments);
        this.DepartmentsSubject.next(Departments)
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Departments:', error);
        alert('Error loading Departments');
        this.DepartmentsSubject.next([]);
        return of([]);
      })
    );
  }


  GetAllDepartments(): Observable<IDepartment[]> {
    return this.http.get<IDepartment[]>(environment.baseUrl + 'Department/getAllDepartments').pipe(
      tap(Departments => {
        console.log('Loaded All Departments:', Departments);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Departments:', error);
        alert('Error loading Departments');
        return of([] as IDepartment[]);
      })
    );
  }

  getAllDepartmentsWithProducts(): Observable<IDepartment[]> {
    return this.http.get<IDepartment[]>(environment.baseUrl + 'Department/getAllDepartmentsWithProducts').pipe(
      tap(Departments => {
        console.log('Loaded Departments With Products:', Departments);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Departments:', error);
        alert('Error loading Departments');
        return of([]);
      }));
  }


  getDepartmentById(id: number): Observable<IDepartment> {
    return this.http.get<IDepartment>(environment.baseUrl + 'Department/getDepartmentById/' + id).pipe(
      tap(Department => {
        console.log('Loaded Department By Id:', Department);
      }),

      catchError((error: HttpErrorResponse) => {
        console.error('Error loading Department:', error);
        alert('Error loading Department');
        return of(null as any);
      }
      ));
  }


  DeleteDepartment(id: number): Observable<IDepartment> {
    return this.http.delete<IDepartment>(environment.baseUrl + 'Department/DeleteDepartment/' + id).pipe(
      tap(() => {
        this.refreshDepartments().subscribe()
        console.log('Department is Deleted'),
          alert("Department is Deleted"),

          catchError((error: HttpErrorResponse) => {
            console.error('Error deleting Department:', error);
            alert("Error in Deleting Department")
            return throwError(() => error);
          })
      }
      ));
  }



}
