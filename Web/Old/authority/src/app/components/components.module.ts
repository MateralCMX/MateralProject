import { NgModule } from '@angular/core';
import { CommonModule } from '@angular/common';
import { NgZorroAntdModule } from 'ng-zorro-antd';
import { DeleteModalComponent } from './delete-modal/delete-modal.component';
import { FormGroupCommon } from './form-group-common';



@NgModule({
  declarations: [
    DeleteModalComponent
  ],
  imports: [
    CommonModule,
    NgZorroAntdModule
  ],
  exports: [
    DeleteModalComponent
  ],
  providers: [
    FormGroupCommon
  ]
})
export class ComponentsModule { }
