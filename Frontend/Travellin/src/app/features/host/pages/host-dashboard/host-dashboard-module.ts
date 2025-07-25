import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';
import { HostDashboard } from './host-dashboard';


const routes: Routes = [
  { path: '', component: HostDashboard },
  { path: 'new', loadChildren: () => import('../../../listing-creation/listing-creation-module').then(m => m.ListingCreationRoutingModule) }
];

@NgModule({
  declarations: [
    HostDashboard
  ],
  imports: [
    CommonModule,
    RouterModule.forChild(routes)
  ]
})
export class HostDashboardModule { }
