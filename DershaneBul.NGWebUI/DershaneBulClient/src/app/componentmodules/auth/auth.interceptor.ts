import { Injectable } from '@angular/core';
import {
  HttpClient, HttpInterceptor, HttpRequest, HttpHandler, HttpSentEvent, HttpHeaderResponse,
  HttpProgressEvent, HttpResponse, HttpUserEvent, HttpErrorResponse
} from "@angular/common/http";

import 'rxjs/add/operator/do';
import 'rxjs/add/operator/map';
import 'rxjs/add/operator/catch';
import 'rxjs/add/observable/throw';
import 'rxjs/add/operator/switchMap';
import 'rxjs/add/operator/finally';
import 'rxjs/add/operator/filter';
import 'rxjs/add/operator/take';

import { Router } from '@angular/router';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { AuthService } from 'src/app/componentmodules/auth/services/auth.service';
import { BehaviorSubject } from 'rxjs';
import { Observable } from 'rxjs/internal/Observable';

@Injectable()
export class AuthInterceptor implements HttpInterceptor {


  isRefreshingToken: boolean = false;
  tokenSubject: BehaviorSubject<string> = new BehaviorSubject<string>(null);

  constructor(
    private alertifyService: AlertifyService,
    private authService: AuthService,
    private router: Router
  ) { }

  addToken(req: HttpRequest<any>, token: string): HttpRequest<any> {
    return req.clone({ setHeaders: { Authorization: 'Bearer ' + token } })
  }
  // Bu sınıf tek metodu intercept olan HttpInterceptor sınıfından türeyip 
  // her çağrıda tokenı headera ekler ve bir hata oluştuğunda onu gösterir.
  intercept(req: HttpRequest<any>, next: HttpHandler): Observable<HttpSentEvent | HttpHeaderResponse | HttpProgressEvent | HttpResponse<any> | HttpUserEvent<any>> {

    // intercept metotta next handle dönüp header eklenmiş kopyalanmış isteği gönderiyoruz.
    if (!this.authService.getJwtToken()) {
      return next.handle(req);
    }

    console.log("isRefreshingToken: " + this.isRefreshingToken);
    return next.handle(this.addToken(req, this.authService.getJwtToken()))
      .catch(error => {
        if (error instanceof HttpErrorResponse) {
          switch ((<HttpErrorResponse>error).status) {
            case 401:
              console.log('error 401');
              return this.handle401Error(req, next);
            case 403:
              console.log('error 403');
              return this.handle403Error(error);
          }
        } else {
          return Observable.throw("int: " + error);
        }
      });
  }

  handle401Error(req: HttpRequest<any>, next: HttpHandler) {
    if (!this.isRefreshingToken) {
      // Hemen isRefreshingToken true yapıyoruz ki 
      //refreshtoken fonksiyonu tekrar çağrılamasın        
      this.isRefreshingToken = true;
      console.log("isRefreshingToken: " + this.isRefreshingToken);
      // Burada resetleme yapıyoruz ki diğer istekler refresh token fonksiyonundan
      // token gelene kadar bekleyecekler.
      this.tokenSubject.next(null);
      debugger;
      // authService.refreshToken çağırılıyor (Buradan Observable string döner)
      return this.authService.refreshToken()
        .switchMap((newToken: string) => {
          if (newToken) {
            // Başarılı olunduğunda yeni tokenla tokenSubject.next çağırılır.
            // bu diğer api çağıralarına refresh token çağırısından gelen
            // tokenın hazır olduğunu ve kullanılabileceğini bildirir.
            this.tokenSubject.next(newToken);
            // yeni token kullanılarak isteğe devam edilir.
            return next.handle(this.addToken(req, newToken));
          }
          // Eğer yeni bir token alınamazsa sıkıntı demektir. Kullanıcı dışarı atılır.
          this.isRefreshingToken = false;
          return this.authService.logOut();
        })
        .catch(error => {
          // Refresh token çağrılırken exception alınırsa kullanıcı dışarı atılır.
          return this.authService.logOut();
        })
        .finally(() => {
          // refreshtoken fonksiyonunun işi bittiğinde burası çalışır
          // isRefreshingToken tekrar false yapılır ki tekrar refresh token 
          // fonksiyonunun çağırılmasına hazır hale gelinebilsin.
          this.isRefreshingToken = false;
        });
      // Hangi path alınırsa alınsın, yeni next handle isteğini çözümleyen bir çağrıyla biten
      // bir observable dönmeliyiz. Böylece orjinal çağrı değiştirilmiş çağrı ile
      // eşleşebilir.           
    }
    // Eğer refreshingToken true ise tokenSubject null olana kadar bekleyeceğiz.
    else {
      return this.tokenSubject
        .filter(token => token != null)
        // Sadece take 1 alınır. 2 dönerse istekten çıkılır.
        .take(1)
        .switchMap(token => {
          // token hazır olduğunda yeni requestin next.handle çağırılır.
          return next.handle(this.addToken(req, token));
        });
    }
  }

  handle403Error(error) {
    console.log('outside conditional');
    // Bu noktada eğer refresh token süresi biterse ki bunun sebebi de refresh token 
    // ömrü boyunca hiç çağrı yapılmamasıdır. 
    // Böyle bir şeyle karşılaşmak çok zordur fakat olursa ‘invalid_grant’ mesajıyla birlikte
    // 400 error  alınmalı. Böylece kullanıcı dışarı atılıp login sayfasına yönlendirilmelidir.  

    if (error && error.status === 403 || error.error && error.error.error === 'invalid_grant') {
      // If we get a 400 and the error message is 'invalid_grant', the token is no longer valid so logout.
      console.log('inside the conditional');
      //return this.authService.logOut();
    }

    return Observable.throw("403 int: " + JSON.stringify(error));
  }
}

