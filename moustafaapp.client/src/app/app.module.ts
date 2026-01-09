import { HttpClientModule } from '@angular/common/http';
import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';
import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { Home } from './home/home';
import { NavebarComponent } from './navebar/navebar.component';
import { HeaderOfferComponent } from './header-offer/header-offer.component';
import { BrandsComponent } from './brands/brands.component';
import { NewArrivalsComponent } from './new-arrivals/new-arrivals.component';
import { TopSellingComponent } from './top-selling/top-selling.component';
import { DressStyleComponent } from './dress-style/dress-style.component';
import { FooterComponent } from './footer/footer.component';
import { ProductDetailComponent } from './product-detail/product-detail.component';
import { ReviewsComponent } from './reviews/reviews.component';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { RegisterComponent } from './register/register.component';
import { LoginComponent } from './login/login.component';
import { RouterModule } from '@angular/router';
import { LayoutComponent } from './layout/layout.component';
import { JWT_OPTIONS, JwtHelperService } from '@auth0/angular-jwt';
@NgModule({
  declarations: [
    AppComponent,
    Home,
    NavebarComponent,
    LoginComponent,
    RegisterComponent,
    HeaderOfferComponent,
    BrandsComponent,
    NewArrivalsComponent,
    TopSellingComponent,
    DressStyleComponent,
    FooterComponent,
    ProductDetailComponent,
    ReviewsComponent,
    LayoutComponent
  ],
  imports: [
    BrowserModule, HttpClientModule,
    AppRoutingModule, RouterModule, FormsModule, ReactiveFormsModule
  ],
  providers: [
    { provide: JWT_OPTIONS, useValue: JWT_OPTIONS },
    JwtHelperService
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
