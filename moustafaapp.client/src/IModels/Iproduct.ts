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
  categoryName?: string 
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


//export interface Product {
//  productId: number;
//  name: string;
//  description?: string;

//  price: number;
//  oldPrice?: number;
//  discount?: number;
//  rating?: number;

//  photo?: string;

//  brandName?: string;
//  categoryName?: string;
//  departmentName?: string;
//}
