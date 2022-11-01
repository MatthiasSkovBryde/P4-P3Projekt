import { Injectable } from '@angular/core';
import { Router, CanActivate, ActivatedRouteSnapshot, RouterStateSnapshot, UrlTree } from '@angular/router';
import { Observable } from 'rxjs';
import { JwtDecodePlus } from '../helpers/JWTDecodePlus';
import { AuthenticationService } from './authentication.service';

@Injectable({ providedIn: 'root'})
export class AdminGuard implements CanActivate {
    constructor(private router: Router, private authenticationService: AuthenticationService) { 
        this.authenticationService.OnTokenChanged.subscribe((token) => {
            if(token != '') {
                this.isLoggedIn = true;
            }
            else {
                this.isLoggedIn = false;
            }
        });
    }

    private isLoggedIn: boolean = false;

    public canActivate(route: ActivatedRouteSnapshot, state: RouterStateSnapshot): boolean | UrlTree | Observable<boolean | UrlTree> | Promise<boolean | UrlTree> {
        if (!this.isLoggedIn) {
            return this.router.navigate(['login'], {queryParams: { returnUrl: state.url}});
        }

        return true;
    }
}