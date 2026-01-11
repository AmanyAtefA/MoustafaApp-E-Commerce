import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../../Service/products.service';
import { IProduct } from '../../IModels/Iproduct';
import { Observable } from 'rxjs';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit{


  titleProducts = true;
  Products$ = this._ProductsService.Products$;


    constructor(private _ProductsService: ProductsService) { }

  ngOnInit(): void {
    this.Products$;
    this._ProductsService.loadProducts();
    console.log(this.Products$);
  }


  calcolatePriceAfterDiscount(price?: number, discount?: number): number {
    if (!price)
      return 0;

    return price - (price * (discount ?? 0) / 100);
  }

}
