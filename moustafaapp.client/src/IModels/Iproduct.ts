import { ICategory } from "./Icategory";
import { IProductImage } from "./Iproduct-image";

export interface IProduct {

  id: number;
  name: string;
  description: string;
  price: number;
  qty: number;
  photo: string;
  categoryId: number;
  category?: ICategory | null;
  productImages: IProductImage[];
  amount?: number ;
  discount?: number;
  CreatedAt: Date;

}
