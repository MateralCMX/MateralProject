import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { SubSystemRoutingModule } from './sub-system-routing.module';
import { SubSystemListComponent } from './sub-system-list/sub-system-list.component';
import { SubSystemEditComponent } from './sub-system-edit/sub-system-edit.component';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { FormsModule, ReactiveFormsModule } from '@angular/forms';
import { ComponentsModule } from '../components/components.module';


@NgModule({
  declarations: [
    SubSystemListComponent,
    SubSystemEditComponent
  ],
  imports: [
    CommonModule,
    SubSystemRoutingModule,
    NgZorroAntdModule,
    FormsModule,
    ReactiveFormsModule,
    ComponentsModule
  ]
})
export class SubSystemModule { }
