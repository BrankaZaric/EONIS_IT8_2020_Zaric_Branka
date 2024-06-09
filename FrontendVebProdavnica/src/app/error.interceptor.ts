import { Injectable } from '@angular/core';
import {
  HttpInterceptor,
  HttpRequest,
  HttpHandler,
  HttpEvent,
  HttpErrorResponse
} from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MatSnackBar } from '@angular/material/snack-bar';

@Injectable()
export class ErrorInterceptor implements HttpInterceptor {
  constructor(private snackBar: MatSnackBar) {}

  intercept(
    request: HttpRequest<any>,
    next: HttpHandler
  ): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let errorMessage = 'An unknown error occurred';
        if (error.error instanceof ErrorEvent) {
          // Client-side errors
          errorMessage = `Error: ${error.error.message}`;
        } else {
          // Server-side errors
          errorMessage = `Error Code: ${error.status}\nMessage: ${error.message}`;
          // Show snackbar notification based on status code
          switch (error.status) {
            case 400:
              this.snackBar.open('Bad request. Please check your input data.', 'Close', { duration: 5000 });
              break;
            case 401:
              this.snackBar.open('Unauthorized access. Please check your credentials.', 'Close', { duration: 5000 });
              break;
            case 404:
              this.snackBar.open('Resource not found', 'Close', { duration: 5000 });
              break;
            case 500:
              this.snackBar.open('Internal server error', 'Close', { duration: 5000 });
              break;
            default:
              this.snackBar.open(errorMessage, 'Close', { duration: 5000 });
          }
        }
        // Returning throwError will propagate the error to the subscriber
        return throwError(error);
      })
    );
  }
}
