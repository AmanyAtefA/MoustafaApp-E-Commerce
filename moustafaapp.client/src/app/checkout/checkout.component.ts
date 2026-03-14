import { Component } from '@angular/core';
import { FormBuilder, Validators } from '@angular/forms';
import { Router } from '@angular/router';
import { CheckoutService } from '../../Service/checkout.service';
import { CartsService } from '../../Service/carts.service';
import { ICartItem } from '../../IModels/ICartItem';
import { IAddress } from '../../IModels/IAddress';
@Component({
  selector: 'app-checkout',
  templateUrl: './checkout.component.html',
  styleUrl: './checkout.component.css'
})
export class CheckoutComponent {

  cart$ = this._CartService.userCart$;

  constructor(
    private fb: FormBuilder,
    private _CheckoutService: CheckoutService,
    private _CartService: CartsService,
    private router: Router
  ) { }


  ngOnInit() {
    this._CartService.getCartByUserIdFromToken().subscribe();
  }


  form = this.fb.group({
    fullName: ['', Validators.required],
    phoneNumber: ['', Validators.required],
    city: ['', Validators.required],
    street: ['', Validators.required],
    notes: ['']
  })

  checkout() {
    if (this.form.invalid) return;

    const data: IAddress = this.form.value as IAddress;

    this._CheckoutService.checkout(data)
      .subscribe({
        next: (res: any) => {
          console.log(data);
          alert("Order Successfully")
          this.router.navigate(['/Order', res.orderId])
        }
      })
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
}
