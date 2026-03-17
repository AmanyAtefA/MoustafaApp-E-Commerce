import { ProductPreset } from "./Enum/ProductPreset";

export interface IProductFilter {
  pageNumber?: number;
  pageSize?: number;

  brandId?: number;
  categoryId?: number;
  departmentId?: number;

  sizeId?: number;
  colorId?: number;

  minPrice?: number;
  maxPrice?: number;

  search?: string;

  sortBy?: string;
  sortDirection?: string;

  preset?: ProductPreset;
  onSale?: boolean;
}

