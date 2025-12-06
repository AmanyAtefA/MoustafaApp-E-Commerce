import { ICartItem } from "./ICartItem";
import { IUser } from "./iuser";


export interface ICart {

  id: number;
  date: Date;
  orderId?: number;
  userId?: string;
  total: number;
  cash: number;
  discount: number;
  User?: IUser;
  CartDetails: ICartItem[];
}
