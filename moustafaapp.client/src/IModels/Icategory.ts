import { IProduct } from "./Iproduct";

export interface ICategory {

    id: number ; 
  name?: string;
 
  Product?: IProduct[];

}
