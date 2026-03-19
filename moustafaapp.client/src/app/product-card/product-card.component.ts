import { Component, Input } from '@angular/core';
import { IProduct } from '../../IModels/Iproduct';

@Component({
  selector: 'app-product-card',
  templateUrl: './product-card.component.html',
  styleUrl: './product-card.component.css'
})
export class ProductCardComponent {

  @Input() product!: IProduct;
}
