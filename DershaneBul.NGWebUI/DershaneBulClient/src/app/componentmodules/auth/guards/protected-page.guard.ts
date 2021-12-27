import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/componentmodules/auth/services/auth.service';

@Injectable({
    providedIn: 'root'
})
export class ProtectedPageGuard implements CanActivate {

    constructor(private authService: AuthService, private router: Router) { }

    canActivate() {
        return this.canLoad();
    }

    canLoad() {
        if (!this.authService.loggedIn()) {
            this.router.navigate(['/membership']);
        }
        return this.authService.loggedIn();
    }
}
