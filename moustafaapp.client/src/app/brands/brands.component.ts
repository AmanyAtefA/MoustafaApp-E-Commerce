import { Component } from '@angular/core';
import { BrandService } from '../../Service/brand.service';

@Component({
  selector: 'app-brands',
  templateUrl: './brands.component.html',
  styleUrl: './brands.component.css'
})
export class BrandsComponent {


  Brands$ = this._BrandService.Brands$;

  constructor(private _BrandService: BrandService) { }

  ngOnInit() {
    this._BrandService.loadBrands();
  }

}
