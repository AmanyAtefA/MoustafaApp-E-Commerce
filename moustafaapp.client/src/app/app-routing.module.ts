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
import { CartComponent } from './cart/cart.component';
import { CheckoutComponent } from './checkout/checkout.component';
import { SuccessComponent } from './success/success.component';
import { OrderComponent } from './order/order.component';

const routes: Routes = [
  {
    path: '',
    component: LayoutComponent,
    children: [
      { path: '', redirectTo: 'Home', pathMatch: 'full' },
      { path: 'Home', component: Home },
      { path: 'Products', component: ProductsComponent },
      { path: 'ProductDetail/:id', component: ProductDetailComponent },
      { path: 'NewArrivals', component: NewArrivalsComponent },
      { path: 'TopSelling', component: TopSellingComponent },
      { path: 'Cart', component: CartComponent },
      { path: 'Checkout', component: CheckoutComponent },
      { path: 'Success', component: SuccessComponent },
      { path: 'Order/:id', component: OrderComponent },

    ]
  },

  { path: 'Login', component: LoginComponent },
  { path: 'Register', component: RegisterComponent },

  { path: '**', redirectTo: '/Home' },
];

@NgModule({
  imports: [
    RouterModule.forRoot(routes)
  ],
  exports: [RouterModule] 
})
export class AppRoutingModule {

  titleProducts = true;

}
