import { Component  ,OnInit, Input } from '@angular/core';
import { ProductsService } from '../../Service/products.service';
import { Observable } from 'rxjs';
import { IProduct } from '../../IModels/Iproduct';
import { PagedResult } from '../../IModels/paged-result';


@Component({
  selector: 'app-top-selling',
  templateUrl: './top-selling.component.html',
  styleUrl: './top-selling.component.css'
})
export class TopSellingComponent implements OnInit {



  @Input() showPagination = true;
  @Input() pageSize = 8;
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
