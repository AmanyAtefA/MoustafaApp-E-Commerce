import { Component, OnInit } from '@angular/core';
import { ActivatedRoute } from '@angular/router';
import { IOrder } from '../../IModels/IOrder';
import { OrdersService } from '../../Service/orders.service';

@Component({
  selector: 'app-order',
  templateUrl: './order.component.html',
  styleUrl: './order.component.css'
})
export class OrderComponent implements OnInit {

  order: IOrder | null = null;

  constructor(
    private _OrderService: OrdersService,
    private route: ActivatedRoute
  )
  { }

  ngOnInit() {

    const id = this.route.snapshot.paramMap.get('id')

    this._OrderService.getOrderById(Number(id))
      .subscribe(res => {
        this.order = res
      })

  }

}

