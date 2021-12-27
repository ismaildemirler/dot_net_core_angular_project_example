import { Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { debug } from 'util';

@Injectable({
  providedIn: 'root'
})
export class ErrorService {

  constructor() { }

  getClientMessage(error: Error): string {
    if (!navigator.onLine) {
      return 'Lütfen internet bağlantınızı kontrol ediniz!';
    }
    return error.message ? error.message : error.toString();
  }

  getClientStack(error: Error): string {
    return error.stack;
  }

  getServerMessage(error: HttpErrorResponse): string {
    return error["error"]["message"];
  }

  getServerDetail(error: HttpErrorResponse): string {
    return error["error"]["details"];
  }

  getServerStack(error: HttpErrorResponse): string {
    // handle stack trace
    return 'stack';
  }
}
