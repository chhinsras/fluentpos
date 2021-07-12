import {AfterViewInit, Component, OnInit, ViewChild} from '@angular/core';
import {PaginatedResult} from '../../../../../core/models/wrappers/PaginatedResult';
import {Category} from '../../models/category';
import {CategoryParams} from '../../models/categoryParams';
import {MatTableDataSource} from '@angular/material/table';
import {MatSort, Sort} from '@angular/material/sort';
import {CategoryService} from '../../services/category.service';
import {MatDialog} from '@angular/material/dialog';
import {ToastrService} from 'ngx-toastr';
import {PaginatedFilter} from '../../../../../core/models/Filters/PaginatedFilter';
import {CategoryFormComponent} from './category-form/category-form.component';
import {DeleteDialogComponent} from '../../../shared/components/delete-dialog/delete-dialog.component';

@Component({
  selector: 'app-category',
  templateUrl: './category.component.html',
  styleUrls: ['./category.component.scss']
})
export class CategoryComponent implements OnInit, AfterViewInit {

  categories: PaginatedResult<Category>;
  categoryColumns: string[] = ['id', 'name', 'detail', 'action'];
  categoryParams = new CategoryParams();
  dataSource = new MatTableDataSource<Category>([]);
  searchString: string;
  @ViewChild(MatSort) sort: MatSort;

  constructor(public categoryService: CategoryService, public dialog: MatDialog, public toastr: ToastrService) {
  }

  ngOnInit(): void {
    this.getCategories();
  }

  ngAfterViewInit(): void {
    this.dataSource.sort = this.sort;
  }

  getCategories(): void {
    this.categoryService.getCategories(this.categoryParams).subscribe((result) => {
      this.categories = result;
      this.dataSource.data = this.categories.data;
    });
  }

  handlePageChange(event: PaginatedFilter): void {
    this.categoryParams.pageNumber = event.pageNumber;
    this.categoryParams.pageSize = event.pageSize;
    this.getCategories();
  }

  openCategoryForm(category?: Category): void {
    const dialogRef = this.dialog.open(CategoryFormComponent, {
      data: category
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.getCategories();
      }
    });
  }

  openDeleteConfirmationDialog(id: string): void {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: 'Do you confirm the removal of this category?'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.removeCategory(id);
      }
    });
  }

  removeCategory(id: string): void {
    this.categoryService.deleteCategory(id).subscribe(() => {
      this.getCategories();
      this.toastr.info('Category Removed');
    });
  }

  doSort(sort: Sort): void {
    this.categoryParams.orderBy = sort.active + ' ' + sort.direction;
    this.getCategories();
  }

  public doFilter(): void {
    this.categoryParams.searchString = this.searchString.trim().toLocaleLowerCase();
    this.categoryParams.pageNumber = 0;
    this.categoryParams.pageSize = 0;
    this.getCategories();
  }

  public reload(): void {
    this.searchString = this.categoryParams.searchString = '';
    this.categoryParams.pageNumber = 0;
    this.categoryParams.pageSize = 0;
    this.getCategories();
  }

}
