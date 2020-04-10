import { NgModule } from '@angular/core';
import { Routes, RouterModule } from '@angular/router';
import { SubSystemListComponent } from './sub-system-list/sub-system-list.component';


const routes: Routes = [
  { path: 'List', component: SubSystemListComponent }
];

@NgModule({
  imports: [RouterModule.forChild(routes)],
  exports: [RouterModule]
})
export class SubSystemRoutingModule { }
