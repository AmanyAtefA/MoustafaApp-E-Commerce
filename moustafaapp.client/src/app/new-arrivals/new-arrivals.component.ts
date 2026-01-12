import { Component, OnInit,Input } from '@angular/core';
import { ProductsService } from '../../Service/products.service';
import { Observable } from 'rxjs';
import { IProduct } from '../../IModels/Iproduct';
import { PagedResult } from '../../IModels/paged-result';



@Component({
  selector: 'app-new-arrivals',
  templateUrl: './new-arrivals.component.html',
  styleUrl: './new-arrivals.component.css'
})
export class NewArrivalsComponent implements OnInit{


  @Input() showPagination = true;
  @Input()pageSize = 8;
  page = 1;
  

  newArrivals$!: Observable<PagedResult<IProduct>>;

  constructor(private _ProductsService: ProductsService) { }

  ngOnInit(): void {
    this.load();
  }

  load() {
    this.newArrivals$ =
      this._ProductsService.loadProductNewArrivals(this.page, this.pageSize);
  }
  onPageChange(page: number) {
    this.page = page;
    this.load();
  }
  
}


