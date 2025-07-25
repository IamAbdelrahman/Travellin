import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { Routes, RouterModule } from '@angular/router';

const routes: Routes = [{ path: 'host', loadChildren: () => import('../host/pages/host-dashboard/host-dashboard-module').then(m => m.HostDashboardModule) }
];

@NgModule({
  declarations: [],
  imports: [
    CommonModule,
    RouterModule.forChild([
      {
        path: 'host/listings/new',
        loadChildren: () => import('../listing-creation/listing-creation-module')
                          .then(m => m.ListingCreationRoutingModule) //possible error ???
      }
    ])
  ]
})

export class PropertiesModule { }
