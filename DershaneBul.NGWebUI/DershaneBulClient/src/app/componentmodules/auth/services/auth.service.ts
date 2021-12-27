import { Injectable } from '@angular/core';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { Router } from '@angular/router';
import { JwtHelper, tokenNotExpired } from 'angular2-jwt';
import { AppConstants } from 'src/app/utils/app-constants';
import { HttpHeaders, HttpErrorResponse, HttpClient } from '../../../../../node_modules/@angular/common/http';
import { RequestRegisterUser } from 'src/app/componentmodules/auth/models/request/reuest-register-user';
import { RequestLoginUser } from 'src/app/componentmodules/auth/models/request/request-login-user';
import { Observable } from 'rxjs/Rx'
import { RequestRefreshToken } from 'src/app/componentmodules/auth/models/request/request-refresh-token';
import { ResponseAuthentication } from 'src/app/componentmodules/auth/models/response/response-authentication';
import { RequestLogoutUser } from 'src/app/componentmodules/auth/models/request/request-logout-user';

@Injectable({
  providedIn: 'root'
})
export class AuthService {

  constructor(
    private httpClient: HttpClient,
    private constants: AppConstants,
    private router: Router,
    private alertifyService: AlertifyService
  ) { }

  path = this.constants.BASE_URL_DEV;
  JwtHelper: JwtHelper = new JwtHelper();

  register(registerUser: RequestRegisterUser) {
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json");
    return this.httpClient
      .post(this.path + "identity/register", registerUser, { headers: headers })
      .subscribe(data => {
        let jsonResponse = JSON.parse(JSON.stringify(data));
        if (jsonResponse.success) {
          this.alertifyService.success(jsonResponse.message);
        }
        else {
          this.alertifyService.minimalDialog("Bilinmeyen bir hata ile karşılaşıldı!", "Hata");
        }
      });
  }

  home() {
    return this.httpClient.get<any>(this.path + "Home/", {
    }).subscribe(data => {
    });
  }

  login(loginUser: RequestLoginUser) {
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json");
    this.httpClient
      .post(this.path + "identity/login", loginUser, { headers: headers })
      .subscribe(data => {
        let jsonResponse = JSON.parse(JSON.stringify(data));
        if (jsonResponse.success) {
          if (jsonResponse.token != null) {
            this.saveTokens(
              jsonResponse.token.toString(),
              jsonResponse.refreshToken.toString()
            );
          }
          this.alertifyService.success(jsonResponse.message);
          this.router.navigateByUrl('/home');
        }
        else {
          this.alertifyService.minimalDialog("Bilinmeyen bir hata ile karşılaşıldı!", "Hata");
        }
      });
  }


  refreshToken(): Observable<string> {
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json");
    let tokens = new RequestRefreshToken();
    tokens.RefreshToken = this.getRefreshToken();
    tokens.Token = this.getJwtToken();
    return this.httpClient
      .post(this.path + "identity/refresh", tokens, { headers: headers })
      .map((tokens: ResponseAuthentication) => {
        this.saveTokens(tokens["token"], tokens["refreshToken"]);
        return tokens["token"];
      });
  }

  logOut() {
    debugger;
    let headers = new HttpHeaders();
    headers = headers.append("Content-Type", "application/json");
    let logOutModel = new RequestLogoutUser();
    logOutModel.Email = this.getCurrentUser()["email"];
    this.httpClient
      .post(this.path + "identity/logout", logOutModel, { headers: headers })
      .subscribe(() => {
        this.removeTokens();
        this.router.navigate(['/membership']);
      });

    return Observable.throw("");
  }

  saveTokens(token: string, refreshToken: string) {
    localStorage.setItem(this.constants.TOKEN, token);
    localStorage.setItem(this.constants.REFRESH_TOKEN, refreshToken);
  }

  removeTokens() {
    localStorage.removeItem(this.constants.TOKEN);
    localStorage.removeItem(this.constants.REFRESH_TOKEN);
  }

  loggedIn() {
    return tokenNotExpired(this.constants.TOKEN);
  }

  get token() {
    return localStorage.getItem(this.constants.TOKEN);
  }

  getRefreshToken() {
    return localStorage.getItem(this.constants.REFRESH_TOKEN);
  }

  getJwtToken() {
    return this.token;
  }

  getCurrentUser() {
    return this.JwtHelper.decodeToken(this.token);
  }

  errorHandler(error: HttpErrorResponse) {
  }
}
