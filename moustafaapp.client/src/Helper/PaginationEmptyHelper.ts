import { PagedResult } from "../IModels/pagedResult";

export function emptyPagedResult<T>(): PagedResult<T> {
  return {
    pageNumber: 1,
    pageSize: 0,
    totalCount: 0,
    totalPages: 0,
    items: []
  };
}
