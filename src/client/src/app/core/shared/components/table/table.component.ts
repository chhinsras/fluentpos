import {AfterViewInit, Component, EventEmitter, Input, OnInit, Output, ViewChild} from '@angular/core';
import {TableColumn} from "./table-column";
import {MatSort, Sort} from "@angular/material/sort";
import {MatTableDataSource} from "@angular/material/table";
import { DeleteDialogComponent } from 'src/app/modules/admin/shared/components/delete-dialog/delete-dialog.component';
import { MatDialog } from '@angular/material/dialog';

@Component({
  selector: 'app-table',
  templateUrl: './table.component.html',
  styleUrls: ['./table.component.scss']
})
export class TableComponent implements OnInit, AfterViewInit {

  public tableDataSource = new MatTableDataSource([]);
  public displayedColumns: string[];
  searchString: string;

  @ViewChild(MatSort, {static: true}) matSort: MatSort;

  @Input() title: string;
  @Input() subtitle: string;

  @Input() isSortable = false;
  @Input() isFilterable = false;
  @Input() columns: TableColumn[];

  @Input() set data(data: any[]) {
    this.setTableDataSource(data);
  }

  @Output() onFilter: EventEmitter<string> = new EventEmitter<string>();
  @Output() onReload: EventEmitter<any> = new EventEmitter<any>();
  @Output() onSort: EventEmitter<Sort> = new EventEmitter<Sort>();

  @Output() onCreateForm: EventEmitter<any> = new EventEmitter();
  @Output() onEditForm: EventEmitter<any> = new EventEmitter();
  @Output() onDelete: EventEmitter<string> = new EventEmitter<string>();

  constructor(public dialog: MatDialog) {
  }

  ngOnInit(): void {
    const columnNames = this.columns.map((tableColumn: TableColumn) => tableColumn.name);
    this.displayedColumns = columnNames;
  }

  ngAfterViewInit(): void {
     this.tableDataSource.sort = this.matSort;
  }

  setTableDataSource(data: any) {
    this.tableDataSource = new MatTableDataSource<any>(data);
  }

  openCreateForm(){
    this.onCreateForm.emit();
  }

  openEditForm($event?){
    this.onEditForm.emit($event);
  }

  handleReload(){
    this.searchString = '';
    this.onReload.emit();
  }

  handleFilter(){
    this.onFilter.emit(this.searchString);
  }

  handleSort(sortParams: Sort) {
    sortParams.active = this.columns.find(column => column.name === sortParams.active).dataKey;
    this.onSort.emit(sortParams);
  }

  openDeleteConfirmationDialog($event: string)
  {
    const dialogRef = this.dialog.open(DeleteDialogComponent, {
      data: 'Do you confirm the removal of this brand?'
    });
    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.onDelete.emit($event);
      }
    });
  }
}