import { Component, OnInit } from '@angular/core';
import { FormControl, FormGroup, Validators } from '@angular/forms';

@Component({
  selector: 'app-brand-form',
  templateUrl: './brand-form.component.html',
  styleUrls: ['./brand-form.component.scss']
})
export class BrandFormComponent implements OnInit {

  brandForm: FormGroup;
  constructor() { }

  ngOnInit(): void {
    this.initializeForm();
  }

  initializeForm() {
    this.brandForm = new FormGroup({
      name: new FormControl('', Validators.required),
      detail: new FormControl('', Validators.required)
    })
  }

  onFileChange(event: any){
    
  }

  onSubmit() {
    console.log(this.brandForm);
  }

}
