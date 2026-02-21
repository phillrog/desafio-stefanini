import { Injectable } from '@angular/core';
import { HttpInterceptor, HttpRequest, HttpHandler, HttpEvent, HttpErrorResponse } from '@angular/common/http';
import { Observable, throwError } from 'rxjs';
import { catchError } from 'rxjs/operators';
import { MessageService } from 'primeng/api';

@Injectable()
export class ErrorHandlerInterceptor implements HttpInterceptor {
  constructor(private messageService: MessageService) {}

  intercept(request: HttpRequest<any>, next: HttpHandler): Observable<HttpEvent<any>> {
    return next.handle(request).pipe(
      catchError((error: HttpErrorResponse) => {
        let severity = 'error';
        let summary = 'Erro';
        let detail = 'Ocorreu um erro inesperado.';

        if (error.status === 400) {
          summary = 'Dados Inválidos';
          detail = error.error?.message || 'Verifique as informações enviadas.';
        } else if (error.status === 404) {
          severity = 'warn';
          summary = 'Não Encontrado';
          detail = 'O recurso solicitado não existe.';
        } else if (error.status === 500) {
          summary = 'Erro no Servidor';
          detail = 'Tente novamente mais tarde.';
        }

        this.messageService.add({ severity, summary, detail, life: 5000 });
        return throwError(() => error);
      })
    );
  }
}
