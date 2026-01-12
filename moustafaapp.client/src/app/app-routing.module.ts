import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { Home } from './home/home';
import { NotFoundComponent } from './not-found/not-found.component';
import { LoginComponent } from './login/login.component';
import { RegisterComponent } from './register/register.component';
import { LayoutComponent } from './layout/layout.component';
import { ProductsComponent } from './products/products.component';
import { NewArrivalsComponent } from './new-arrivals/new-arrivals.component';
import { TopSellingComponent } from './top-selling/top-selling.component';

const routes: Routes = [
  {
    path: '', component: LayoutComponent,
    children: [
      { path: '', pathMatch: 'full', redirectTo: 'Home' },
      { path: 'Home', component: Home },
      { path: 'Products', component: ProductsComponent },
      { path: 'ProductDetail', component: ProductDetailComponent },
      { path: 'NewArrivals', component: NewArrivalsComponent },
      { path: 'TopSelling', component: TopSellingComponent },
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
export class AppRoutingModule {

  titleProducts = true;

}
