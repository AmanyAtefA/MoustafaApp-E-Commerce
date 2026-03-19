import { HttpErrorResponse, HttpEventType, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { throwError } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { inject } from '@angular/core';
import { Router } from '@angular/router';



export function GeneralInterceptor(request: HttpRequest<any>, next: HttpHandlerFn) {

  const token = localStorage.getItem('token');
  const router = inject(Router);

  let headers = request.headers;

  if (token) {
    headers = headers.set('Authorization', 'Bearer ' + token);
  }

  if (['POST', 'PUT', 'PATCH'].includes(request.method.toUpperCase())) {
    headers = headers.set('Content-Type', 'application/json');
  }

  const clonedRequest = request.clone({ headers });

  return next(clonedRequest).pipe(
    catchError((error: HttpErrorResponse) => {

      // ✅ أهم تعديل هنا
      if (error.status === 401) {

        localStorage.removeItem('token');

        // ❗ متعملش redirect لو هو بالفعل في login
        if (!router.url.includes('/Login')) {
          router.navigate(['/Login']);
        }
      }

      return throwError(() => error);
    })
  );
}
