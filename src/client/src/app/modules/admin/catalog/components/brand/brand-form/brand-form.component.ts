import { Component, Inject, Input, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';
import { MAT_DIALOG_DATA } from '@angular/material/dialog';
import { ToastrService } from 'ngx-toastr';
import { Brand } from '../../../models/brand';
import { BrandService } from '../../../services/brand.service';

@Component({
  selector: 'app-brand-form',
  templateUrl: './brand-form.component.html',
  styleUrls: ['./brand-form.component.scss']
})
export class BrandFormComponent implements OnInit {
  brandForm: FormGroup;
  constructor(@Inject(MAT_DIALOG_DATA) public data: Brand, private brandService: BrandService, private toastr: ToastrService) { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.brandForm = new FormGroup({
      id: new FormControl(this.data && this.data.id),
      name: new FormControl(this.data && this.data.name, Validators.required),
      detail: new FormControl(this.data && this.data.detail, Validators.required)
    })
  }

  onFileChange(event: any){
    
  }

  onSubmit() {
    if (this.brandForm.valid) {
      if (this.brandForm.get('id').value === "" || this.brandForm.get('id').value == null) {
        this.brandService.createBrand(this.brandForm.value).subscribe(response => {
          this.toastr.success(response.messages[0]);
        })
      } else {
        this.brandService.updateBrand(this.brandForm.value).subscribe(response => {
          this.toastr.success(response.messages[0]);
        })
      }
    }
  }

}
