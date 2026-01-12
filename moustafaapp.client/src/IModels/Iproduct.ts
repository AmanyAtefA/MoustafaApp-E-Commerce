import { ICategory } from "./Icategory";
import { IProductImage } from "./Iproduct-image";

export interface IProduct {

  id: number;
  name: string;
  description?: string;
  price: number;
  qty: number;
  photo: string;
  categoryId?: number;
  categoryName?: ICategory | null;
  productImages: IProductImage[];
  discount?: number;
 
  oldPrice?: number;
  rating?: number;
  stock: number;
  createdAt: Date;

  brandId?: number;
  brandName?: string;
  departmentId: number;
  departmentName?: string;
}


