import { Component, OnInit } from '@angular/core';
import { Observable, filter, map, switchMap } from 'rxjs';
import { IProduct } from '../../IModels/Iproduct';
import { ProductsService } from '../../Service/products.service';
import { SizeService } from '../../Service/size.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ISizes } from '../../IModels/ISizes';



@Component({
  selector: 'app-product-detail',
  templateUrl: './product-detail.component.html',
  styleUrl: './product-detail.component.css'
})
export class ProductDetailComponent implements OnInit {

  Product$!: Observable<IProduct>
  Sizes$!: Observable<ISizes[]>

  productId!: number;


  selectedColorId: number |null = null;
  selectedSizeId: number | null =null;
  quantity: number = 1;
  constructor(private _ProductsService: ProductsService,
    private route: ActivatedRoute,
    private _SizeService: SizeService) { }


  ngOnInit(): void {
    //this.route.paramMap.subscribe(params => {
    //  const id = params.get('id');
    //  if (id != null) {
    //    this.productId = +id;
    //    this.Product$ = this._ProductsService.getProductyByIdWithDetails(this.productId);
    //    console.log('Product ID from URL:', this.productId);
    //  } else {
    //    console.error('Product ID is null');
    //  }
    //});
    this.route.params.subscribe(p => {
      console.log('Route Params:', p);
    });

    this.Product$ = this.route.paramMap.pipe(
      map(params => params.get('id')),
      filter((id): id is string => id !== null),
      map(id => +id),
      switchMap(id => {
        this.productId = id;
        return this._ProductsService.getProductyByIdWithDetails(id)
      }
    ));


    this._SizeService.getAllSizes().subscribe();
    this.Sizes$ = this._SizeService.Sizes$;
    
  }



  selectColor(colorId: number) {
    this.selectedColorId =
      this.selectedColorId === colorId ? null : colorId;
  }


  selectSize(sizeId: number) {
    this.selectedSizeId =
      this.selectedSizeId === sizeId ? null : sizeId;
  }



  increase() {
    this.quantity++;
  }


  decrease() {
    if (this.quantity > 1) {
      this.quantity--;
    }
  }



}
