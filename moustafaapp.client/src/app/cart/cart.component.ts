import { Component } from '@angular/core';
import { CartsService } from '../../Service/carts.service';
import { ICartItem } from '../../IModels/ICartItem';


@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {


  couponCode = '';
  cart$ = this._CartService.userCart$;

  constructor(private _CartService: CartsService) { }

  ngOnInit() {
    this._CartService.getCartByUserIdFromToken().subscribe();
  }


  removeItem(cartItemId: number) {
    this._CartService.removeItem(cartItemId).subscribe();
  }


  updateQuantity(item: ICartItem) {
    this._CartService.updateQuantity({
      cartItemId: item.cartItemId,
      quantity: item.quantity
    }).subscribe();
  }


  increase(item: ICartItem) {
    item.quantity++;
    this.updateQuantity(item);
  }


  decrease(item: ICartItem) {
    if (item.quantity <= 1) return;

    item.quantity--;
    this.updateQuantity(item);
  }


  applyCoupon() {

    if (!this.couponCode) return;

    this._CartService.applyCoupon({ code: this.couponCode })
      .subscribe({
        next: () => {
          this.couponCode = "";
        },
        error: () => alert("Invalid Coupon")
      });

  }


  removeCoupon() {
    this._CartService.removeCoupon().subscribe();
  }



}
