import { Component, OnInit } from '@angular/core';
import { PageEvent } from '@angular/material/paginator';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Brand } from '../../models/brand';
import { BrandParams } from '../../models/brandParams';
import { BrandService } from '../../services/brand.service';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.scss']
})
export class BrandComponent implements OnInit {

  brands: PaginatedResult<Brand>;
  brandColumns: string[] = ['id', 'name', 'detail'];
  brandParams = new BrandParams();

  constructor(public brandService: BrandService) { }

  ngOnInit(): void {
    this.getBrands();
  }

  getBrands(){
    this.brandService.getBrands(this.brandParams).subscribe(brands => this.brands = brands && brands);
  }

  handlePageEvent(event: PageEvent){
   
    this.brandParams.pageNumber = event.pageIndex + 1;
    this.brandParams.pageSize = event.pageSize;
    this.getBrands();
  }

}
