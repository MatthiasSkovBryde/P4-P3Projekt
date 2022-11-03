import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { ProductComponent } from './components/product/product.component';


import { SignupformComponent } from './components/signupform/signupform.component';

const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'product/:id', component: ProductComponent },
  {path: 'login', component: LoginComponent},
  { path:'signup', component: SignupformComponent},
  { path: '**', redirectTo: ''} // MUST BE LAST IN ARRAY!
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
