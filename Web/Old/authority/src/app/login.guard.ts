import { Injectable } from '@angular/core';
import { CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';

@Injectable({
  providedIn: 'root'
})
export class LoginGuard implements CanActivate {
  canActivate(
    next: ActivatedRouteSnapshot,
    state: RouterStateSnapshot): Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
      if (!(window as any).AuthorityHelper) { return true; }
      const token = (window as any).AuthorityHelper.getToken();
      if (token) { return true; }
      if (!((window as any).GotoLogin)) { return true; }
      (window as any).GotoLogin();
      return false;
  }
}
