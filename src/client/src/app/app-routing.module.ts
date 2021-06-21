import { Host, NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { NotFoundComponent } from './core/shared/components/not-found/not-found.component';
import { ServerErrorComponent } from './core/shared/components/server-error/server-error.component';
import { AdminLayoutComponent } from './layouts/admin-layout/admin-layout.component';
import { AuthLayoutComponent } from './layouts/auth-layout/auth-layout.component';
import { HomeLayoutComponent } from './layouts/home-layout/home-layout.component';
import { PosLayoutComponent } from './layouts/pos-layout/pos-layout.component';

const routes: Routes = [
  {
    path: '',
    component: AuthLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: '/login',
        pathMatch: 'full'
      },
      {
        path: 'login',
        loadChildren: () => import('./modules/auth/auth.module').then(m => m.AuthModule)
      }
    ]
  },
  {
    path: '',
    component: HomeLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: '/home',
        pathMatch: 'full'
      },
      {
        path: 'home',
        loadChildren: () => import('./modules/home/home.module').then(m => m.HomeModule)
      }
    ]
  },
  {
    path: '',
    component: AdminLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: '/admin',
        pathMatch: 'full'
      },
      {
        path: 'admin',
        loadChildren: () => import('./modules/admin/admin.module').then(m => m.AdminModule)
      }
    ]
  },
  {
    path: '',
    component: PosLayoutComponent,
    children: [
      {
        path: '',
        redirectTo: '/pos',
        pathMatch: 'full'
      },
      {
        path: 'pos',
        loadChildren: () => import('./modules/pos/pos.module').then(m => m.PosModule)
      }
    ]
  },
  {
    path: 'not-found', component: NotFoundComponent
  },
  {
    path: 'server-error', component: ServerErrorComponent
  },
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule]
})
export class AppRoutingModule { }
