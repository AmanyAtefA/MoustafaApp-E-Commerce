import { IProduct } from "./Iproduct";

export interface IProductImage {
  id: number;
  productId?: number;
  imageUrl?: string;
  product?: IProduct ;
}
