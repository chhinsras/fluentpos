import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { IdentityComponent } from './identity.component';
import { ProfileComponent } from './components/profile/profile.component';
import { MaterialModule } from 'src/app/core/material/material.module';
import { SharedModule } from 'src/app/core/shared/shared.module';
import { IdentityRoutingModule } from './identity-routing.module';
import { TranslateModule } from '@ngx-translate/core';
import { UserComponent } from './components/user/user.component';



@NgModule({
  declarations: [
    IdentityComponent,
    ProfileComponent,
    UserComponent
  ],
  imports: [
    CommonModule,
    IdentityRoutingModule,
    MaterialModule,
    SharedModule,
    TranslateModule
  ]
})
export class IdentityModule { }
