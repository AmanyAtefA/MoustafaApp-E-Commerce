import { Component, OnInit,Input } from '@angular/core';
import { ProductsService } from '../../Service/products.service';
import { Observable } from 'rxjs';
import { IProduct } from '../../IModels/Iproduct';
import { PagedResult } from '../../IModels/pagedResult';
import { ProductPreset } from '../../IModels/Enum/ProductPreset';



@Component({
  selector: 'app-new-arrivals',
  templateUrl: './new-arrivals.component.html',
  styleUrl: './new-arrivals.component.css'
})
export class NewArrivalsComponent implements OnInit{


  @Input() showPagination = true;
  @Input()pageSize = 8;

  
  Products$ = this._ProductsService.NewArrivals$;
  constructor(private _ProductsService: ProductsService) { }

  ngOnInit(): void {
    this.loadProductNewArrivals();
  }

  loadProductNewArrivals() {
    this._ProductsService.GetProductsWithFilter({
      pageNumber: 1,
      pageSize: this.pageSize,
      preset: ProductPreset.NewArrivals
    }).subscribe();
  }

  onPageChange(page: number) {
    this._ProductsService.GetProductsWithFilter({
      pageNumber: page,
      pageSize: this.pageSize,
      preset: ProductPreset.NewArrivals
    }).subscribe();
  }
  
}


