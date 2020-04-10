import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { LocationStrategy, HashLocationStrategy } from '@angular/common';
import { IndexComponent } from './index/index.component';
import { PageNotFoundComponent } from './page-not-found/page-not-found.component';
import { LoginGuard } from './login.guard';


const routes: Routes = [
  {
    path: 'authority',
    children: [
      {
        path: 'User', component: IndexComponent,
        loadChildren: () => import('./user/user.module').then(mod => mod.UserModule),
      },
      {
        path: 'SubSystem', component: IndexComponent,
        loadChildren: () => import('./sub-system/sub-system.module').then(mod => mod.SubSystemModule),
      },
      {
        path: 'Role', component: IndexComponent,
        loadChildren: () => import('./role/role.module').then(mod => mod.RoleModule),
      },
      {
        path: 'WebMenu', component: IndexComponent,
        loadChildren: () => import('./web-menu/web-menu.module').then(mod => mod.WebMenuModule),
      },
      { path: '**', component: PageNotFoundComponent }
    ],
    canActivate: [LoginGuard]
  },
  { path: '**', component: PageNotFoundComponent }
];

@NgModule({
  imports: [RouterModule.forRoot(routes)],
  exports: [RouterModule],
  providers: [
    { provide: LocationStrategy, useClass: HashLocationStrategy }
  ]
})
export class AppRoutingModule { }
