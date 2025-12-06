import { CanActivateFn, Router } from '@angular/router';
import { RegisterService } from '../Service/register.service';
import { inject } from '@angular/core';
import { map } from 'rxjs/operators';


export const authGuard: CanActivateFn = (route, state,) => {

    const router = inject(Router);
    const registerService = inject(RegisterService);

    const Roles: string[] = route.data['roles'];

  const user = registerService.currentUser;
  if (user && Array.isArray(user.role)) {
    const isAuthorized = user.role.some((role: string) => Roles.includes(role));
    if (isAuthorized) {
      return true;
    }
  }

  router.navigate(['/ManegerPage']); 
    return false;
  
};
