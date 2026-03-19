import { Component, OnInit, Input, OnChanges } from '@angular/core';
import { ProductsService } from '../../Service/products.service';
import { Observable } from 'rxjs';
import { IProduct } from '../../IModels/Iproduct';
import { PagedResult } from '../../IModels/pagedResult';
import { ProductPreset } from '../../IModels/Enum/ProductPreset';
import { IProductFilter } from '../../IModels/IProductFilter';



@Component({
  selector: 'app-new-arrivals',
  templateUrl: './new-arrivals.component.html',
  styleUrl: './new-arrivals.component.css'
})
export class NewArrivalsComponent implements OnInit, OnChanges {


  title = "NEW ARRIVALS";
  
  @Input() showButtonInHome : boolean = false;
  @Input() showPagination = true;
  @Input()pageSize = 8;

  @Input() inputFilter?: IProductFilter;

  Products$ = this._ProductsService.NewArrivals$;
  constructor(private _ProductsService: ProductsService) { }

  ngOnInit(): void {
    this.loadProductNewArrivals();
  }

  ngOnChanges() {
    this.loadProductNewArrivals();
  }

  loadProductNewArrivals() {

    const filter: IProductFilter = {
      pageNumber: 1,
      pageSize: 8,
      preset: ProductPreset.NewArrivals,
      ...this.inputFilter
    };

    this._ProductsService.GetProductsWithFilter(filter).subscribe();
  }

  onPageChange(page: number) {

    const filter: IProductFilter = {
      pageNumber: page,
      pageSize: 8,
      preset: ProductPreset.NewArrivals,
      ...this.inputFilter
    };

    this._ProductsService.GetProductsWithFilter(filter).subscribe();
  }
  
}


