import { Component  ,OnInit, Input, OnChanges } from '@angular/core';
import { ProductsService } from '../../Service/products.service';
import { Observable } from 'rxjs';
import { IProduct } from '../../IModels/Iproduct';
import { PagedResult } from '../../IModels/pagedResult';
import { ProductPreset } from '../../IModels/Enum/ProductPreset';
import { IProductFilter } from '../../IModels/IProductFilter';


@Component({
  selector: 'app-top-selling',
  templateUrl: './top-selling.component.html',
  styleUrl: './top-selling.component.css'
})
export class TopSellingComponent implements OnInit, OnChanges {


  title :string = "TOP SELLNG";
  @Input() showPagination = true;
  @Input() pageSize = 8;

  @Input() inputFilter?: IProductFilter;
  @Input() showButtonInHome: boolean = false;

  Products$ = this._ProductsService.TopSelling$;
  constructor(private _ProductsService: ProductsService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  ngOnChanges() {
    this.loadProducts();
  }

  loadProducts() {
    const filter: IProductFilter = {
      pageNumber: 1,
      pageSize: 8,
      preset: ProductPreset.BestSeller,
      ...this.inputFilter
    };

    this._ProductsService.GetProductsWithFilter(filter).subscribe();
  }

  onPageChange(page: number) {

    const filter: IProductFilter = {
      pageNumber: page,
      pageSize: 8,
      preset: ProductPreset.BestSeller,
      ...this.inputFilter
    };

    this._ProductsService.GetProductsWithFilter(filter).subscribe();
  }



}
