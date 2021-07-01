import { Component, OnInit } from '@angular/core';
import { MatDialog } from '@angular/material/dialog';
import { PageEvent } from '@angular/material/paginator';
import { PaginatedFilter } from 'src/app/core/models/Filters/PaginatedFilter';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { DeleteDialogComponent } from '../../../shared/components/delete-dialog/delete-dialog.component';
import { Brand } from '../../models/brand';
import { BrandParams } from '../../models/brandParams';
import { BrandService } from '../../services/brand.service';
import { BrandFormComponent } from './brand-form/brand-form.component';

@Component({
  selector: 'app-brand',
  templateUrl: './brand.component.html',
  styleUrls: ['./brand.component.scss']
})
export class BrandComponent implements OnInit {

  brands: PaginatedResult<Brand>;
  brandColumns: string[] = ['id', 'name', 'detail', 'action'];
  brandParams = new BrandParams();

  constructor(public brandService: BrandService, public dialog: MatDialog) { }

  ngOnInit(): void {
    this.getBrands();
  }

  getBrands() {
    this.brandService.getBrands(this.brandParams).subscribe(brands => this.brands = brands && brands);
  }

  handlePageChange(event: PaginatedFilter) {
    this.brandParams.pageNumber = event.pageNumber;
    this.brandParams.pageSize = event.pageSize;
    this.getBrands();
  }

  openEditForm() {
    const dialogRef = this.dialog.open(BrandFormComponent);
    dialogRef.afterClosed().subscribe(result => {
      console.log(`Dialog result: ${result}`);
    });
  }
  openDeleteConfirmationDialog(id : string) {
    console.log(id);
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: "Do you confirm the removal of this brand?"
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.brandService.deleteBrand(id);
        this.getBrands();
      }
    });
  }

}
