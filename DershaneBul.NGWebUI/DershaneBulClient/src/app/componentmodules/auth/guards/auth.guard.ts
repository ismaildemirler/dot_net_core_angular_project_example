import { CanActivate, Router } from '@angular/router';
import { Injectable } from '@angular/core';
import { AuthService } from 'src/app/componentmodules/auth/services/auth.service';

@Injectable({
  providedIn: 'root'
})
export class AuthGuard implements CanActivate {

  constructor(private authService: AuthService, private router: Router) { }
  
  canActivate() {
    if (this.authService.loggedIn()) {
      this.router.navigate(['/home']);
    }
    return !this.authService.loggedIn();
  }
}
