import { IProduct } from "./Iproduct";

export interface IProductImage {
  id: number;
  productId?: number;
  images?: string;
  product?: IProduct ;
}
