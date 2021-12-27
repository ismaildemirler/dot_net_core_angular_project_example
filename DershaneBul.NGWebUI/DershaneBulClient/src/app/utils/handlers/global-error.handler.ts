import { ErrorHandler, Injector, Injectable } from '@angular/core';
import { HttpErrorResponse } from '@angular/common/http';
import { ErrorService } from 'src/app/services/error/error.service';
import { AlertifyService } from 'src/app/services/alertify/alertify.service';
import { throwError } from 'rxjs';
import { LoggingService } from 'src/app/services/logging/logging.service';

@Injectable()
export class GlobalErrorHandler implements ErrorHandler {

  // Error handling is important and needs to be loaded first.
  // Because of this we should manually inject the services with Injector.
  constructor(private injector: Injector) { }

  handleError(error: Error | HttpErrorResponse) {

    const errorService = this.injector.get(ErrorService);
    const logger = this.injector.get(LoggingService);
    const notifier = this.injector.get(AlertifyService);

    let message;
    let details;
    let stackTrace;

    if (error instanceof HttpErrorResponse) {
      // Server Error
      message = errorService.getServerMessage(error);
      details = errorService.getServerDetail(error);
      stackTrace = errorService.getServerStack(error);
      notifier.minimalDialog(details, message);
    } else {
      // Client Error
      message = errorService.getClientMessage(error);
      stackTrace = errorService.getClientStack(error);
      notifier.error(message);
    }

    // Always log errors
    logger.logError(message, stackTrace);
    return throwError("Global error: " + error);
  }
}
