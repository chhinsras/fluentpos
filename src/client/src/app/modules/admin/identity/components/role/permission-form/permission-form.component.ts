import { Component, Inject, OnInit } from '@angular/core';
import { FormBuilder, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Permission } from '../../../models/permission';
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

  permissions: Permission;

  constructor(@Inject(MAT_DIALOG_DATA) public data: Role, private roleService: RoleService, private toastr: ToastrService, private fb: FormBuilder) { }

  ngOnInit(): void {
   this.getPermissions();
  }

  getPermissions() {
    this.roleService.getPermissionsByRoleId(this.data.id).subscribe(response => {
      console.log(response);
      this.permissions = response;

   console.log(this.permissions);
    });
  }
}
