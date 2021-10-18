import { Injectable } from '@angular/core';
import {
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpInterceptor
} from '@angular/common/http';
import { Observable } from 'rxjs';
import { BusyService } from '../services/busy.service';
import { finalize, delay } from 'rxjs/operators';

@Injectable()
export class LoadingInterceptor implements HttpInterceptor {

  constructor(private busyServive: BusyService) { }

  intercept(request: HttpRequest<unknown>, next: HttpHandler): Observable<HttpEvent<unknown>> {
    if (request.method === 'POST' && request.url.includes('orders')) {
      return next.handle(request);
    }
    if (request.method === 'DELETE') {
      return next.handle(request);
    }
    if (request.url.includes('emailexists')) {
      return next.handle(request);
    }
    this.busyServive.busy();
    return next.handle(request).pipe(
      delay(0),
      finalize(() => {
        this.busyServive.idle();
      })
    );
  }
}
