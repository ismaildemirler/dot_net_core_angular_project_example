import { NgModule } from '@angular/core';
import { RouterModule } from '@angular/router';
import { HTTP_INTERCEPTORS, HttpClientModule } from '@angular/common/http';
import { ReactiveFormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';
import { ProtectedPageGuard } from 'src/app/componentmodules/auth/guards/protected-page.guard';
import { AuthService } from 'src/app/componentmodules/auth/services/auth.service';
import { MembershipComponent } from 'src/app/componentmodules/auth/containers/membership/membership.component';
import { AuthGuard } from 'src/app/componentmodules/auth/guards/auth.guard';
import { AuthInterceptor } from 'src/app/componentmodules/auth/auth.interceptor';
import { BrowserModule } from '@angular/platform-browser';

@NgModule({
  declarations: [
    MembershipComponent
  ],
  providers: [
    AuthGuard,
    AuthService,
    ProtectedPageGuard,
    {
      provide: HTTP_INTERCEPTORS,
      useClass: AuthInterceptor,
      multi: true
    }
  ],
  imports: [
    CommonModule,
    RouterModule,
    HttpClientModule,
    ReactiveFormsModule,
  ],
  exports: [MembershipComponent]
})
export class AuthModule {
}
