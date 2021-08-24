import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MatDialog, MAT_DIALOG_DATA } from '@angular/material/dialog';
import { Sort } from '@angular/material/sort';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { PaginatedFilter } from 'src/app/core/models/Filters/PaginatedFilter';
import { CustomAction } from 'src/app/core/shared/components/table/custom-action';
import { TableColumn } from 'src/app/core/shared/components/table/table-column';
import { Permission } from '../../../models/permission';
import { Role } from '../../../models/role';
import { RoleService } from '../../../services/role.service';

@Component({
  selector: 'app-role-Permission-form',
  templateUrl: './role-Permission-form.component.html',
  styleUrls: ['./role-Permission-form.component.scss']
})
export class RolePermissionFormComponent implements OnInit {
  rolePermission: Permission;
  rolePermissionColumns: TableColumn[];
  searchString: string;
  rolePermissionActionData: CustomAction = new CustomAction('Update Permission', 'update', 'primary');

  constructor(
    @Inject(MAT_DIALOG_DATA) public data: Role,
    private dialogRef: MatDialog,
    public roleService: RoleService,
    public toastr: ToastrService
  ) { }

  ngOnInit(): void {
    this.getPermissions();
    this.initColumns();
  }

  getPermissions(): void {
    this.roleService.getRolePermissionsByRoleId(this.data.id).subscribe((result) => {
      this.rolePermission = result.data;
    });
  }

  initColumns(): void {
    this.rolePermissionColumns = [
      { name: 'Id', dataKey: 'id', isSortable: true, isShowable: true },
      { name: 'Type', dataKey: 'type', isSortable: true, isShowable: true },
      { name: 'Group', dataKey: 'group', isSortable: true, isShowable: true },
      { name: 'Value', dataKey: 'value', isSortable: true, isShowable: true },
      { name: 'Description', dataKey: 'description', isSortable: true, isShowable: true },
      { name: 'Selected', dataKey: 'selected', isSortable: true, isShowable: true },
    ];
  }

  submitRolePermission($event: Permission): void{
    this.roleService.updateRolePermissionsByRoleId($event).subscribe((result) => {
      this.toastr.success(result.messages[0]);
      this.dialogRef.closeAll();
    });
  }
}
