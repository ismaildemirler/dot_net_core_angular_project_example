import { Injectable } from '@angular/core';
import { AlertifyService } from '../alertify/alertify.service';

@Injectable({
  providedIn: 'root'
})
export class LoggingService {

  constructor(private alertify: AlertifyService) { }

  logError(message: string, stack: string) {
    //this.alertify.error(message);
  }
}
