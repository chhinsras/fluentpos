import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {MatDialog} from '@angular/material/dialog';
import {PaginatedFilter} from 'src/app/core/models/Filters/PaginatedFilter';
import {PaginatedResult} from 'src/app/core/models/wrappers/PaginatedResult';
import {Brand} from '../../models/brand';
import {BrandParams} from '../../models/brandParams';
import {BrandService} from '../../services/brand.service';
import {BrandFormComponent} from './brand-form/brand-form.component';
import {ToastrService} from 'ngx-toastr';
import {MatTableDataSource} from '@angular/material/table';
import {MatSort, Sort} from '@angular/material/sort';
import { TableColumn } from 'src/app/core/shared/components/table/table-column';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.scss']
})
export class BrandComponent implements OnInit {

  brands: PaginatedResult<Brand>;
  brandColumns: TableColumn[];
  brandParams = new BrandParams();
  searchString: string;

  constructor(public brandService: BrandService, public dialog: MatDialog, public toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.getBrands();
    this.initColumns();
  }

  getBrands(): void {
    this.brandService.getBrands(this.brandParams).subscribe((result) => {
      this.brands = result;
    });
  }

  initColumns(): void {
    this.brandColumns = [
      { name: 'Id', dataKey: 'id', isSortable: true },
      { name: 'Name', dataKey: 'name', isSortable: true },
      { name: 'Detail', dataKey: 'detail', isSortable: true },
      { name: 'Action', dataKey: 'action', position: 'right' }
    ];
  }

  handlePageChange(event: PaginatedFilter): void {
    this.brandParams.pageNumber = event.pageNumber;
    this.brandParams.pageSize = event.pageSize;
    this.getBrands();
  }

  openBrandForm(brand?: Brand): void {
    const dialogRef = this.dialog.open(BrandFormComponent, {
      data: brand
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getBrands();
      }
    });
  }

  removeBrand($event: string): void {
    this.brandService.deleteBrand($event).subscribe(() => {
      this.getBrands();
      this.toastr.info('Brand Removed');
    });
  }

  doSort($event: Sort): void {
    this.brandParams.orderBy =  $event.active + ' ' + $event.direction;
    console.log(this.brandParams.orderBy);
    this.getBrands();
  }

  public doFilter($event: string): void {
    this.brandParams.searchString = $event.trim().toLocaleLowerCase();
    this.brandParams.pageNumber = 0;
    this.brandParams.pageSize = 0;
    this.getBrands();
  }

  public reload(): void {
    this.brandParams.searchString = '';
    this.brandParams.pageNumber = 0;
    this.brandParams.pageSize = 0;
    this.getBrands();
  }
}
