import { Component, OnInit } from '@angular/core';
import { Observable, filter, map, switchMap } from 'rxjs';
import { IProduct } from '../../IModels/Iproduct';
import { ProductsService } from '../../Service/products.service';
import { SizeService } from '../../Service/size.service';
import { CartsService } from '../../Service/carts.service';
import { ActivatedRoute, Router } from '@angular/router';
import { ISizes } from '../../IModels/ISizes';
import { IAddItem } from '../../IModels/IAddItem';



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
    private _SizeService: SizeService,
    private _CartService: CartsService,
    private router: Router) { }


  ngOnInit(): void {
  
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
    if (this.quantity < 1) {
      this.quantity--;
    }
  }



  addToCart() {


    const item: IAddItem = {
      productId: this.productId,
      quantity: this.quantity,
      sizeId: this.selectedSizeId!,
      colorId: this.selectedColorId!
    }


    if (!this.selectedSizeId) {
      alert("Please select size")
      return
    }

    if (!this.selectedColorId) {
      alert("Please select color")
      return
    }

    console.log(item)

    this._CartService.addItemToCart(item).subscribe({
      next: (res) => {
        console.log("Added to cart", res)
        alert("Product added to cart")
        
      },
      error: (err) => {
        console.log(err)
      }
    })

  }

}
