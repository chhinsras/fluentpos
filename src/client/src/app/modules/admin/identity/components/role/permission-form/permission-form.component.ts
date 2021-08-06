import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { MatTableDataSource } from '@angular/material/table';
import { ToastrService } from 'ngx-toastr';
import { RoleClaim } from '../../../models/permission';
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
  permissions = new MatTableDataSource();
  filterValues = {};
  filterSelectObj = [];
  
  constructor(@Inject(MAT_DIALOG_DATA) public data: Role, private roleService: RoleService, private toastr: ToastrService, private fb: FormBuilder) {
    this.filterSelectObj = [
      {
        name: 'ID',
        columnProp: 'id',
        options: []
      }, {
        name: 'TYPE',
        columnProp: 'type',
        options: []
      }, {
        name: 'GROUP',
        columnProp: 'group',
        options: []
      }, {
        name: 'VALUE',
        columnProp: 'value',
        options: []
      }
    ]
  }

  ngOnInit(): void {
   this.getRolePermissions();
  }

  getRolePermissions() {
    this.roleService.getRolePermissionsByRoleId(this.data.id).subscribe(response => {
      this.permissions.data = response.data.roleClaims;
      this.filterSelectObj.filter((o) => {
        o.options = this.getFilterObject(response.data.roleClaims, o.columnProp);
      });
    });
    
  }

  getFilterObject(fullObj, key) {
    const uniqChk = [];
    fullObj.filter((obj) => {
      if (!uniqChk.includes(obj[key])) {
        uniqChk.push(obj[key]);
      }
      return obj;
    });
    return uniqChk;
  }

  togglePermission(claim: RoleClaim) {
    console.log(this.permissions.data);
  }

  filterChange(filter, $event) {
    this.permissions.filter = $event.value;
  }

  resetFilters() {
    this.filterValues = {}
    this.filterSelectObj.forEach((value, key) => {
      value.modelValue = undefined;
    })
    this.permissions.filter = "";
  }
}
