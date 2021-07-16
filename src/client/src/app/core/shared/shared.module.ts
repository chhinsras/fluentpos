import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ToastrModule } from 'ngx-toastr';
import { RouterModule } from '@angular/router';
import { NotFoundComponent } from './components/not-found/not-found.component';
import { ServerErrorComponent } from './components/server-error/server-error.component';
import { PaginationComponent } from './components/pagination/pagination.component';
import { MaterialModule } from '../material/material.module';
import {AuthService} from '../services/auth.service';
import {LocalStorageService} from '../services/local-storage.service';
import {MultilingualService} from '../services/multilingual.service';
import { TableComponent } from './components/table/table.component';
import { DataPropertyGetterPipe } from './components/table/pipes/data-property-getter.pipe';

@NgModule({
  declarations: [
    NotFoundComponent,
    ServerErrorComponent,
    PaginationComponent,
    TableComponent,
    DataPropertyGetterPipe
  ],
  imports: [
    CommonModule,
    RouterModule,
    ReactiveFormsModule,
    MaterialModule,
    FormsModule,
    ToastrModule.forRoot({
      positionClass: 'toast-bottom-right',
      preventDuplicates: true
    })
  ],
  exports: [
    ReactiveFormsModule,
    FormsModule,
    PaginationComponent,
    TableComponent
  ]
})
export class SharedModule { }
