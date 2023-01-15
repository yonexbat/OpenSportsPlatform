import { HttpEvent, HttpHandler, HttpInterceptor, HttpRequest, } from '@angular/common/http';
import { Injectable } from '@angular/core';
import { Observable, } from 'rxjs';


@Injectable()
export class JwtInterceptor implements HttpInterceptor {
    
    // eslint-disable-next-line @typescript-eslint/no-explicit-any
    intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {

        const token = localStorage.getItem('jwt');
        if (token) {
            request = request.clone({ headers: request.headers.set('Authorization', 'Bearer ' + token) });
        }

        return next.handle(request);
    }
}
