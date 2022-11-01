import { APP_INITIALIZER, DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import localeDk from '@angular/common/locales/en-DK';
registerLocaleData(localeDk);

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { ProductComponent } from './components/product/product.component';
import { LoginComponent } from './components/login/login.component';
import { appInitializer } from './helpers/app.initializer';
import { AuthenticationService } from './_services/authentication.service';
import { AuthenticationInterceptor } from './_interceptor/authentication.interceptor';
import { HashLocationStrategy, LocationStrategy, registerLocaleData } from '@angular/common';
import { FormsModule } from '@angular/forms';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    ProductComponent,
    LoginComponent,
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule
  ],
  providers: [
    { provide: APP_INITIALIZER, useFactory: appInitializer, multi: true, deps: [AuthenticationService]},
    { provide: HTTP_INTERCEPTORS, useClass: AuthenticationInterceptor, multi: true},
    { provide: LocationStrategy, useClass: HashLocationStrategy},
    { provide: DEFAULT_CURRENCY_CODE, useValue: 'DKK'},
    { provide: LOCALE_ID, useValue: 'en-DK'},
    
  ],
  bootstrap: [AppComponent],
})
export class AppModule { }
