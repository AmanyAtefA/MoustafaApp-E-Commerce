import { IProduct } from "./Iproduct";
export interface IBrand {
  brandId: number;
  brandName: string;
  photoBrand: string;
  product: IProduct[];
}
