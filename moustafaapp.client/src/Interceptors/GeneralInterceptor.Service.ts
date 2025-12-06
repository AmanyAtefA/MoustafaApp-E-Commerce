import { HttpErrorResponse, HttpEventType, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { throwError } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { inject } from '@angular/core';
import { Router } from '@angular/router';


const router = inject(Router);

export function GeneralInterceptor(request: HttpRequest<any>, next: HttpHandlerFn) {
  const token = localStorage.getItem('token');

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
      let errorMsg = '';

      if (error.status === 0) {
        errorMsg = '❌ فشل الاتصال بالسيرفر.';
      }
      else if (error.status === 401) {
        errorMsg = '🚫 غير مصرح. من فضلك سجل الدخول.';
        localStorage.removeItem('token');
        router.navigate(['/login']);
      }
      else if (error.status === 500) {
        errorMsg = '⚠️ خطأ داخلي في الخادم. حاول لاحقًا.';
      }
      else {
        errorMsg = error.error?.message || 'حدث خطأ غير متوقع.';
      }

      console.error(errorMsg);
      return throwError(() => new Error(errorMsg));
    })
  );

}
