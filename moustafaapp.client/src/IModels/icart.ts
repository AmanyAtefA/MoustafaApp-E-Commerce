import { ICartItem } from "./ICartItem";

export interface ICart {

  id: number;
  date: Date;
  orderId?: number;
  couponId?: number;
  userId?: string;
  userName?: string;
  total: number;
  cash: number;
  discountRate: number;
  subtotal: number,
  discount: number,
  deliveryFee: number,
  cartItems: ICartItem[];

}
