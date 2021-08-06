import { SelectionModel } from '@angular/cdk/collections';
import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { Permission, RoleClaim } from '../../../models/permission';
import { Role } from '../../../models/role';
import { RoleService } from '../../../services/role.service';

@Component({
  selector: 'app-permission-form',
  templateUrl: './permission-form.component.html',
  styleUrls: ['./permission-form.component.scss']
})
export class PermissionFormComponent implements OnInit {
  permissionForm: FormGroup;
  formTitle: string;

  displayedColumns: string[] = ['id', 'type', 'group', 'value', 'description', 'selected'];
  permissions = new MatTableDataSource<Permission>();
  
  constructor(@Inject(MAT_DIALOG_DATA) public data: Role, private roleService: RoleService, private toastr: ToastrService, private fb: FormBuilder) { }

  ngOnInit(): void {
   this.getRolePermissions();
  }

  getRolePermissions() {
    this.roleService.getRolePermissionsByRoleId(this.data.id).subscribe(response => {
      this.permissions.data = [...this.permissions.data, response.data];
    });
  }
  togglePermission(claim: RoleClaim) {
    console.log(this.permissions.data[0].roleClaims);
  }


}
