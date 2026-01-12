import { Component, OnInit } from '@angular/core';
import { ProductsService } from '../../Service/products.service';


@Component({
  selector: 'app-home',
  standalone: false,
  templateUrl: './home.html',
  styleUrl: './home.css',
})
export class Home implements OnInit{

  

  constructor(private _ProductsService: ProductsService) { }
  ngOnInit(): void {
   
  }
}
