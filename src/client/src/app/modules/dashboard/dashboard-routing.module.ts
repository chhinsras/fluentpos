import { NgModule } from '@angular/core';
import { RouterModule, Routes } from '@angular/router';
import { StatsComponent } from './stats/stats.component';

const routes: Routes = [
  {
    path: '',
    redirectTo: 'stats'
  },
  {
    path: 'stats',
    component: StatsComponent
  }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class DashboardRoutingModule { }
