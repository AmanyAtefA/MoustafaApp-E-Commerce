import { HttpErrorResponse, HttpEventType, HttpHandlerFn, HttpRequest } from "@angular/common/http";
import { throwError } from "rxjs";
import { catchError, tap } from "rxjs/operators";
import { inject } from '@angular/core';
import { Router } from '@angular/router';



export function GeneralInterceptor(request: HttpRequest<any>, next: HttpHandlerFn) {
  const token = localStorage.getItem('token');


  const router = inject(Router);
  let headers = request.headers;

  console.log("Interceptor Token:", token);

  if (token) {
    headers = headers.set('Authorization', 'Bearer ' + token);
  }

  if (['POST', 'PUT', 'PATCH'].includes(request.method.toUpperCase())) {
    headers = headers.set('Content-Type', 'application/json');
  }

  const clonedRequest = request.clone({ headers });

  return next(clonedRequest).pipe(
    catchError((error: HttpErrorResponse) => {

      console.error("SERVER ERROR FULL:", error);
      console.log("STATUS:", error.status);
      console.log("ERROR BODY:", error.error);
      console.log("FULL ERROR:", error);
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

        // 👇 هنا نعرض رسالة السيرفر
        errorMsg = error.error || 'Internal Server Error';

        console.error("SERVER MESSAGE:", error.error);
      }

      else {
        errorMsg = error.error?.message || 'Unexpected Error';
      }

      return throwError(() => new Error(errorMsg));
    })
  );
}
