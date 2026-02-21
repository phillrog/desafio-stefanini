import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';

import { AppRoutingModule } from './app-routing-module';
import { AppComponent } from './app';
import { SharedModule } from './shared/shared.module';
import { LoadingInterceptor } from './core/interceptors/loading.interceptor';
import { ErrorHandlerInterceptor } from './core/interceptors/error-handler.interceptor';
import { MessageService, ConfirmationService } from 'primeng/api';

// PrimeNG Config
import { providePrimeNG } from 'primeng/config';
import Aura from '@primeuix/themes/aura';

import {LOCALE_ID } from '@angular/core'; 
import { registerLocaleData } from '@angular/common';
import localePt from '@angular/common/locales/pt';

registerLocaleData(localePt);

@NgModule({
  declarations: [
    AppComponent
  ],
  imports: [
    BrowserModule,
    BrowserAnimationsModule,
    HttpClientModule,
    AppRoutingModule,
    SharedModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: LoadingInterceptor, multi: true },
    { provide: HTTP_INTERCEPTORS, useClass: ErrorHandlerInterceptor, multi: true },
    providePrimeNG({
        theme: {
            preset: Aura,
           
        }
    }),
    MessageService,
    ConfirmationService,
    { provide: LOCALE_ID, useValue: 'pt-BR' }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
