import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { HomeComponent } from './components/home/home.component';
import { LoginComponent } from './components/login/login.component';
import { SignupformComponent } from './components/signupform/signupform.component';
import { ProductListComponent } from './components/product-list/product-list.component';



const routes: Routes = [
  { path: '', component: HomeComponent },
  { path: 'product-list', component: ProductListComponent }, //product-list:id
  { path: 'login', component: LoginComponent },
  { path:'signup', component: SignupformComponent },
  { path: '**', redirectTo: ''} // MUST BE LAST IN ARRAY!
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
