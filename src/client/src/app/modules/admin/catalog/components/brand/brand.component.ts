import { Component, OnInit, ViewChild } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { PaginatedFilter } from 'src/app/core/models/Filters/PaginatedFilter';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { DeleteDialogComponent } from '../../../shared/components/delete-dialog/delete-dialog.component';
import { Brand } from '../../models/brand';
import { BrandParams } from '../../models/brandParams';
import { BrandService } from '../../services/brand.service';
import { BrandFormComponent } from './brand-form/brand-form.component';
import { ToastrService } from 'ngx-toastr';
import { MatTableDataSource } from '@angular/material/table';
import { MatSort, Sort } from '@angular/material/sort';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.scss']
})
export class BrandComponent implements OnInit {

  brands: PaginatedResult<Brand>;
  brandColumns: string[] = ['id', 'name', 'detail', 'action'];
  brandParams = new BrandParams();
  dataSource = new MatTableDataSource<Brand>([]);
  @ViewChild(MatSort) sort: MatSort;
  constructor(public brandService: BrandService, public dialog: MatDialog, public toastr: ToastrService) { }

  ngOnInit(): void {
    this.getBrands();
  }
  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }
  getBrands() {
    this.brandService.getBrands(this.brandParams).subscribe((result) => {
      this.brands = result;
      this.dataSource.data = this.brands.data;
    });
  }

  handlePageChange(event: PaginatedFilter) {
    this.brandParams.pageNumber = event.pageNumber;
    this.brandParams.pageSize = event.pageSize;
    this.getBrands();
  }

  openBrandForm(brand?: Brand) {
    const dialogRef = this.dialog.open(BrandFormComponent, {
      data: brand
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.getBrands();
    });
  }
  openDeleteConfirmationDialog(id: string) {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: "Do you confirm the removal of this brand?"
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) this.deleteUser(id);
    });
  }

  deleteUser(id: string): void {
    this.brandService.deleteBrand(id).subscribe(() => { this.getBrands(); this.toastr.info('Brand Removed'); });
  }
  customSort(sort: Sort) {
    this.brandParams.orderBy = sort.active + " " + sort.direction;
    this.getBrands();
  }
}
