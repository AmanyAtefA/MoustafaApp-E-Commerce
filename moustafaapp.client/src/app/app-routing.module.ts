import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { Home } from './home/home';
import { NotFoundComponent } from './not-found/not-found.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { LayoutComponent } from './layout/layout.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'Home' },
      { path: 'Home', component: Home },
      { path: 'ProductDetail', component: ProductDetailComponent },
    ]

  },
  { path: 'Login', component: LoginComponent },
  { path: 'Register', component: RegisterComponent },
  { path: 'NotFound', component: NotFoundComponent },
  {path: '**', redirectTo: '/NotFound', pathMatch: 'full' },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
