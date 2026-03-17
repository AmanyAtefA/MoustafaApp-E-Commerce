import { Component, Input, OnInit } from '@angular/core';
import { ProductsService } from '../../Service/products.service';
import { IProductFilter } from '../../IModels/IProductFilter';
import { IProduct } from '../../IModels/Iproduct';
import { ActivatedRoute } from '@angular/router';

@Component({
  selector: 'app-products',
  templateUrl: './products.component.html',
  styleUrl: './products.component.css'
})
export class ProductsComponent implements OnInit{


  @Input() Products: IProduct[] = [];
  @Input() title: string = '';

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
        departmentId: params['departmentId'],
        brandId: params['brandId'],
        search: params['search'],
        onSale: params['onSale'] === 'true',
        preset: params['preset']
      };

      this._ProductsService.loadProducts(this.ProductFilter);

    });

  }

  calculatePriceAfterDiscount(price?: number, discount?: number): number {

    if (!price)
      return 0;

    return price - (price * (discount ?? 0) / 100);

  }

} 
