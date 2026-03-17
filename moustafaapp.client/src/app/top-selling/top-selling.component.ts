import { Component  ,OnInit, Input } from '@angular/core';
import { ProductsService } from '../../Service/products.service';
import { Observable } from 'rxjs';
import { IProduct } from '../../IModels/Iproduct';
import { PagedResult } from '../../IModels/pagedResult';
import { ProductPreset } from '../../IModels/Enum/ProductPreset';


@Component({
  selector: 'app-top-selling',
  templateUrl: './top-selling.component.html',
  styleUrl: './top-selling.component.css'
})
export class TopSellingComponent implements OnInit {



  @Input() showPagination = true;
  @Input() pageSize = 8;


  Products$ = this._ProductsService.TopSelling$;
  constructor(private _ProductsService: ProductsService) { }

  ngOnInit(): void {
    this.loadProducts();
  }

  loadProducts() {
    this._ProductsService.GetProductsWithFilter({
      pageNumber: 1,
      pageSize: this.pageSize,
      preset: ProductPreset.BestSeller
    }).subscribe();
  }

  onPageChange(page: number) {
    this._ProductsService.GetProductsWithFilter({
      pageNumber: page,
      pageSize: this.pageSize,
      preset: ProductPreset.BestSeller
    }).subscribe();
  }


}
