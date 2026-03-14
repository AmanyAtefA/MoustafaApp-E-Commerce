
import { IOrderItem } from "./IOrderItem";
import { IAddress } from "./IAddress";

export interface IOrder {
  orderId: number
  notes: string;
  shippingStatus: string;
  paymentStatus: string;
  totalAmount: number;
  subtotal: number;
  discount: number;
  deliveryFee: number;
  createdAt: Date;
  address: IAddress;
  orderItems: IOrderItem[];
 
}
