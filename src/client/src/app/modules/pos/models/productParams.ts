import { PaginatedFilter } from 'src/app/core/models/Filters/PaginatedFilter';

export class ProductParams implements PaginatedFilter {
  searchString: string;
  brandIds: string[];
  categoryIds: string[];
  pageNumber: number;
  pageSize: number;
  orderBy: string;
}
