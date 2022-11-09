import { query } from '@angular/animations';
import { Injectable } from '@angular/core';
import { ActivatedRouteSnapshot, CanActivate, Router, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { AuthenticationService } from './authentication.service';

@Injectable({
  providedIn: 'root'
})
export class AuthenticationGuard implements CanActivate {

  private isLoggedIn: Boolean = false;

  constructor(private router: Router, private authenticationService: AuthenticationService) { 
    this.authenticationService.OnTokenChanged.subscribe((token) => {
      if (token !== '') {
        this.isLoggedIn = true;
      }
      else {
        this.isLoggedIn = false
      }
    })
  }

  public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot) : Observable<boolean | UrlTree> | Promise<boolean | UrlTree> | boolean | UrlTree {
    if (!this.isLoggedIn) {
      return this.router.navigate([ '/login' ], { queryParams: { returnUrl: state.url } }); 
    }
    return true;
  }
}
