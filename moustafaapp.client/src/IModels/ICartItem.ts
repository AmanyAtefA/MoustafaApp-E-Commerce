import { IProduct } from "./Iproduct"

export interface ICartItem {

  cartItemId: number;
  product: IProduct;
  productId: number,
  productName: string,
  quantity: number,
  priceOfUnit: number,
  photo: string,

}
