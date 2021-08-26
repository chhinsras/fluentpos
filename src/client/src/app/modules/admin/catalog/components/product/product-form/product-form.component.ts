import {Component, Inject, OnInit} from '@angular/core';
import {FormBuilder, FormControl, FormGroup, Validators} from '@angular/forms';
import {MAT_DIALOG_DATA} from '@angular/material/dialog';
import {ToastrService} from 'ngx-toastr';
import { Upload } from 'src/app/core/models/uploads/upload';
import { UploadType } from 'src/app/core/models/uploads/upload-type';
import { PaginatedResult } from 'src/app/core/models/wrappers/PaginatedResult';
import { Brand } from '../../../models/brand';
import { BrandParams } from '../../../models/brandParams';
import { Category } from '../../../models/category';
import { CategoryParams } from '../../../models/categoryParams';
import {Product} from '../../../models/product';
import { BrandService } from '../../../services/brand.service';
import { CategoryService } from '../../../services/category.service';
import {ProductService} from '../../../services/product.service';

@Component({
  selector: 'app-product-form',
  templateUrl: './product-form.component.html',
  styleUrls: ['./product-form.component.scss']
})
export class ProductFormComponent implements OnInit {
  productForm: FormGroup;
  formTitle: string;
  brands: PaginatedResult<Brand>;
  brandParams = new BrandParams();
  categories: PaginatedResult<Category>;
  categoryParams = new CategoryParams();

  upload = new Upload();

  url: any = [];

  constructor(@Inject(MAT_DIALOG_DATA) public data: Product, private productService: ProductService, private brandService: BrandService, private categoryService: CategoryService,
        private toastr: ToastrService, private fb: FormBuilder) {
  }

  ngOnInit(): void {
    this.initializeForm();
    this.brandParams.pageSize = 50;
    this.categoryParams.pageSize = 50;
    this.getBrands();
    this.getCategories();
  }

  initializeForm() {
    this.productForm = this.fb.group({
      id: [this.data && this.data.id],
      name: [this.data && this.data.name, Validators.required],
      brandId: [this.data && this.data.brandId, Validators.required], // todo get brands and show dropdown to select brand instead of hidden input
      categoryId: [this.data && this.data.categoryId, Validators.required], // todo get categories and show dropdown list to select category
      localeName: [this.data && this.data.localeName, Validators.required],
      price: [this.data && this.data.price, Validators.required],
      cost: [this.data && this.data.cost, Validators.required],
      tax: [this.data && this.data.tax , Validators.required],
      taxMethod: [this.data && this.data.taxMethod, Validators.required],
      barcodeSymbology: [this.data && this.data.barcodeSymbology, Validators.required],
      isAlert: [!!(this.data && this.data.isAlert), Validators.required],
      alertQuantity: [this.data && this.data.alertQuantity, Validators.required],
      detail: [this.data && this.data.detail, Validators.required]
    });
    if (this.productForm.get('id').value === '' || this.productForm.get('id').value == null) {
      this.formTitle = 'Register Product';
    } else {
      this.formTitle = 'Edit Product';
    }
  }

  getBrands() {
    this.brandService.getBrands(this.brandParams).subscribe((response) => { this.brands = response; });
  }

  getCategories() {
    this.categoryService.getCategories(this.categoryParams).subscribe((response) => { this.categories = response; });
  }

  onSelectFile(event) {
    if (event.target.files && event.target.files[0]) {
      var reader = new FileReader();
      reader.readAsDataURL(event.target.files[0]); // read file as data url

      this.upload.fileName = event.target.files[0].name;
      this.upload.extension = event.target.files[0].name.split('.').shift();
      this.upload.uploadType = UploadType.Product;

      reader.onloadend = (event) => { // called once readAsDataURL is completed
        this.url = event.target.result;
        this.upload.data = event.target.result;
      }
    }
  }

  onSubmit() {
    // TODO after successful update/insert, refresh table view in component product.component.ts
    console.log(this.url);
    // var upload = new Upload();
    // upload.data = this.url;
    // upload.name = this.url.
    if (this.productForm.valid) {
      if (this.productForm.get('id').value === '' || this.productForm.get('id').value == null) {
        this.productService.createProduct(this.productForm.value).subscribe(response => {
          this.toastr.success(response.messages[0]);
        });
      } else {
        this.productService.updateProduct(this.productForm.value).subscribe(response => {
          this.toastr.success(response.messages[0]);
        });
      }
    }
  }

}
