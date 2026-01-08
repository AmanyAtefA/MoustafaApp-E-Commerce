import { IProduct } from "./Iproduct";
export interface IDepartment {
  departmentId: number;
  departmentName: string;

  Product: IProduct[];
}
