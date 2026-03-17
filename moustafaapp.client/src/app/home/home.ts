import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../../Service/products.service';
import { BrandService } from '../../Service/brand.service';


@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit{

  
  Brands$ = this._BrandService.Brands$;
  constructor(private _ProductsService: ProductsService,
    private _BrandService: BrandService,) { }


  ngOnInit(): void {
    this._BrandService.loadBrands();
  }
}
