import { Component, Input, OnInit } from '@angular/core';
import { ProductsService } from '../../Service/products.service';
import { IProductFilter } from '../../IModels/IProductFilter';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit{


  @Input() title: string = 'PRODUCTS';

  Products$ = this._ProductsService.Products$;

  totalPages = 0;
  totalCount = 0;

  ProductFilter: IProductFilter = {
    pageNumber: 1,
    pageSize: 8
  };

  constructor(private _ProductsService: ProductsService,
    private route: ActivatedRoute,) { }

  ngOnInit(): void {

    this.route.queryParams.subscribe(params => {

      this.ProductFilter = {
        pageNumber: 1,
        pageSize: 8,
        departmentId: params['departmentId'] ? +params['departmentId'] : undefined,
        brandId: params['brandId'] ? +params['brandId'] : undefined,
        search: params['search'],
        onSale: params['onSale'] === 'true',
        preset: params['preset'] ? +params['preset'] : undefined
      };
      console.log(this.ProductFilter);
      this._ProductsService.loadProducts(this.ProductFilter);

    });

  }

  calculatePriceAfterDiscount(price?: number, discount?: number): number {

    if (!price)
      return 0;

    return price - (price * (discount ?? 0) / 100);

  }

} 
