import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {PaginatedResult} from '../../../../../core/models/wrappers/PaginatedResult';
import {Product} from '../../models/product';
import {ProductParams} from '../../models/productParams';
import {MatTableDataSource} from '@angular/material/table';
import {MatSort, Sort} from '@angular/material/sort';
import {ProductService} from '../../services/product.service';
import {MatDialog} from '@angular/material/dialog';
import {ToastrService} from 'ngx-toastr';
import {PaginatedFilter} from '../../../../../core/models/Filters/PaginatedFilter';
import {ProductFormComponent} from '../product/product-form/product-form.component';
import {DeleteDialogComponent} from '../../../shared/components/delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-product',
  templateUrl: './product.component.html',
  styleUrls: ['./product.component.scss']
})
export class ProductComponent implements OnInit, AfterViewInit {

  products: PaginatedResult<Product>;
  productColumns: string[] = [
    'id',
    'name',
    'localeName',
    'brandId', // todo show name instead of id, make it clickable (maybe redirect to brand page or open brand dialog in view mode)
    'categoryId', // todo show name instead of id, make it clickable (maybe redirect to brand page or open category dialog in view mode)
    'detail',
    'price',
    'cost',
    'tax',
    'barcodeSymbology',
    'isAlert',
    'action'
  ];
  productParams = new ProductParams();
  dataSource = new MatTableDataSource<Product>([]);
  searchString: string;
  @ViewChild(MatSort) sort: MatSort;

  constructor(public productService: ProductService, public dialog: MatDialog, public toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.getProducts();
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  getProducts(): void {
    this.productService.getProducts(this.productParams).subscribe((result) => {
      this.products = result;
      this.dataSource.data = this.products.data;
    });
  }

  handlePageChange(event: PaginatedFilter): void {
    this.productParams.pageNumber = event.pageNumber;
    this.productParams.pageSize = event.pageSize;
    this.getProducts();
  }

  openProductForm(product?: Product): void {
    const dialogRef = this.dialog.open(ProductFormComponent, {
      data: product
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getProducts();
      }
    });
  }

  openDeleteConfirmationDialog(id: string): void {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: 'Do you confirm the removal of this product?'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.removeProduct(id);
      }
    });
  }

  removeProduct(id: string): void {
    this.productService.deleteProduct(id).subscribe(() => {
      this.getProducts();
      this.toastr.info('Product Removed');
    });
  }

  doSort(sort: Sort): void {
    this.productParams.orderBy = sort.active + ' ' + sort.direction;
    this.getProducts();
  }

  public doFilter(): void {
    this.productParams.searchString = this.searchString.trim().toLocaleLowerCase();
    this.productParams.pageNumber = 0;
    this.productParams.pageSize = 0;
    this.getProducts();
  }

  public reload(): void {
    this.searchString = this.productParams.searchString = '';
    this.productParams.pageNumber = 0;
    this.productParams.pageSize = 0;
    this.getProducts();
  }
}
