import { CanActivateFn, Router } from '@angular/router';
import { RegisterService } from '../Service/register.service';
import { inject } from '@angular/core';
import { JwtHelperService } from '@auth0/angular-jwt';

export const authGuard: CanActivateFn = (route, state) => {

  const router = inject(Router);
  const registerService = inject(RegisterService);
  const jwtHelper = inject(JwtHelperService);

  const token = localStorage.getItem('token');

  // ❌ مفيش توكن
  if (!token) {
    router.navigate(['/Login']);
    return false;
  }

  // ❌ التوكن expired
  if (jwtHelper.isTokenExpired(token)) {
    localStorage.removeItem('token');
    router.navigate(['/Login']);
    return false;
  }

  // ✅ لو فيه roles
  const roles: string[] = route.data?.['roles'];

  const user = registerService.currentUser;

  if (roles && roles.length > 0) {

    if (user && Array.isArray(user.role)) {
      const isAuthorized = user.role.some((r: string) => roles.includes(r));

      if (isAuthorized) return true;
    }

    // ❌ مش مصرح
    router.navigate(['/NotFound']);
    return false;
  }

  // ✅ user عادي
  return true;
};
