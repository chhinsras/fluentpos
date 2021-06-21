import { NgModule } from '@angular/core';
import { BrowserModule } from '@angular/platform-browser';

import { MaterialModule } from './core/material/material.module';

import { AppRoutingModule } from './app-routing.module';
import { AppComponent } from './app.component';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { HttpClientModule, HTTP_INTERCEPTORS } from '@angular/common/http';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { PosLayoutComponent } from './layouts/pos-layout/pos-layout.component';
import { SideNavigationComponent } from './layouts/admin-layout/side-navigation/side-navigation.component';
import { HomeLayoutComponent } from './layouts/home-layout/home-layout.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { ToolbarComponent } from './layouts/admin-layout/toolbar/toolbar.component';
import { CatalogRoutingModule } from './modules/admin/catalog/catalog-routing.module';
import { ErrorInterceptor } from './core/interceptors/error.interceptor';
import { SharedModule } from './core/shared/shared.module';

@NgModule({
  declarations: [
    AppComponent,
    AuthLayoutComponent,
    PosLayoutComponent,
    SideNavigationComponent,
    ToolbarComponent,
    HomeLayoutComponent,
    AdminLayoutComponent
  ],
  imports: [
    BrowserModule,
    AppRoutingModule,
    BrowserAnimationsModule,
    MaterialModule,
    SharedModule,
    HttpClientModule,
    CatalogRoutingModule
  ],
  providers: [
    { provide: HTTP_INTERCEPTORS, useClass: ErrorInterceptor, multi: true }
  ],
  bootstrap: [AppComponent]
})
export class AppModule { }
