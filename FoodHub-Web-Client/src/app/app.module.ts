import { APP_INITIALIZER, DEFAULT_CURRENCY_CODE, LOCALE_ID, NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import localeDk from '@angular/common/locales/en-DK';
registerLocaleData(localeDk);
import { RouterModule } from '@angular/router';

import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { HomeComponent } from './components/home/home.component';
import { HeaderComponent } from './components/header/header.component';
import { LoginComponent } from './components/login/login.component';
import { appInitializer } from './helpers/app.initializer';
import { AuthenticationService } from './_services/authentication.service';
import { AuthenticationInterceptor } from './_interceptor/authentication.interceptor';
import { HashLocationStrategy, LocationStrategy, registerLocaleData } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SignupformComponent } from './components/signupform/signupform.component';
import { FooterComponent } from './components/footer/footer.component';
import { ProductListComponent } from './components/product-list/product-list.component';
import { UserinfoComponent } from './components/userinfo/userinfo.component';


@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    LoginComponent,
    SignupformComponent,
    FooterComponent,
    ProductListComponent,
    UserinfoComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule,
    HttpClientModule,
    RouterModule,
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
