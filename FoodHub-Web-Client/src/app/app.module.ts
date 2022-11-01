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
import { HashLocationStrategy, LocationStrategy, registerLocaleData } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { SignupformComponent } from './components/signupform/signupform.component';

@NgModule({
  declarations: [
    AppComponent,
    HomeComponent,
    HeaderComponent,
    ProductComponent,
    LoginComponent,
    SignupformComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    FormsModule
  ],
  providers: [

    { provide: DEFAULT_CURRENCY_CODE, useValue: 'DKK'},
    { provide: LOCALE_ID, useValue: 'en-DK'}
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
