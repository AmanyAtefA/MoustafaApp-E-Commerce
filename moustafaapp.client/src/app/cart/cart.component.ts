import { Component } from '@angular/core';

@Component({
  selector: 'app-cart',
  templateUrl: './cart.component.html',
  styleUrl: './cart.component.css'
})
export class CartComponent {

  quantity: number = 1;
  constructor() { }


  increase() {
    this.quantity++;
  }


  decrease() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }

}
