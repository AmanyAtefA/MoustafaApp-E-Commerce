import { IColors } from "./IColors";
import { ISizes } from "./ISizes";
import { ICategory } from "./Icategory";
import { IProductImage } from "./Iproduct-image";

export interface IProduct {

  productId: number;
  name: string;
  description?: string;
  price: number;
  qty: number;
  photo: string;
  categoryId?: number;
  categoryName?: ICategory | null;
  images: IProductImage[];
  discount?: number;

  colors: IColors[];
  sizes: ISizes[];
  oldPrice?: number;
  rating?: number;
  stock: number;
  createdAt: Date;

  brandId?: number;
  brandName?: string;
  departmentId: number;
  departmentName?: string;
}


