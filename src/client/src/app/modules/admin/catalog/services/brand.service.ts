import { Injectable } from '@angular/core';
import { map } from 'rxjs/internal/operators/map';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { BrandApiService } from '../api/brand-api.service';
import { Brand } from '../models/brand';

@Injectable({
  providedIn: 'root'
})
export class BrandService {

  constructor(private api: BrandApiService) {
  }

  getBrands(){
    return this.api.getAlls().pipe(
      map((response: PaginatedResult<Brand>) => response)
    );
  }

  getBrandById(id: string){
    return this.api.getById(id).pipe(
      map((response: Brand) => response)
    );
  }
}
