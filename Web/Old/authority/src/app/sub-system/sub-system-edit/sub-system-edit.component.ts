import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { SubSystemService } from 'src/app/services/sub-system.service';
import { FormGroupCommon } from 'src/app/components/form-group-common';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { SubSystemDTO } from 'src/app/services/models/sub-system/SubSystemDTO';
import { EditSubSystemRequestModel } from 'src/app/services/models/sub-system/EditSubSystemRequestModel';

@Component({
  selector: 'app-sub-system-edit',
  templateUrl: './sub-system-edit.component.html',
  styleUrls: ['./sub-system-edit.component.less']
})
export class SubSystemEditComponent implements OnInit {
  public subSystemID: string;
  public isAdd = false;
  public formData: FormGroup;
  public isTransmitting = false;
  @Output()
  public saveEnd = new EventEmitter<ResultModel>();
  constructor(private subSystemService: SubSystemService, private formGroupCommon: FormGroupCommon) { }
  public ngOnInit() {
    this.formData = new FormGroup({
      name: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required, Validators.maxLength(100)]),
      code: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required, Validators.maxLength(100)]),
      style: new FormControl({ value: null, disabled: this.isTransmitting }),
      display: new FormControl({ value: null, disabled: this.isTransmitting }),
      data: new FormControl({ value: null, disabled: this.isTransmitting })
    });
  }
  public InitData(subSystemID) {
    this.subSystemID = subSystemID;
    if (!this.subSystemID) {
      this.isAdd = true;
      this.formData.setValue({
        name: null,
        code: null,
        style: null,
        display: null,
        data: null
      });
    } else {
      this.isAdd = false;
      this.isTransmitting = true;
      const success = (result: ResultDataModel<SubSystemDTO>) => {
        this.formData.setValue({
          name: result.Data.Name,
          code: result.Data.Code,
          style: result.Data.Style,
          display: result.Data.Display,
          data: result.Data.Data
        });
      };
      const complete = () => {
        this.isTransmitting = false;
      };
      this.subSystemService.getSubSystemInfo(this.subSystemID, success, complete);
    }
  }
  public save() {
    if (!this.formGroupCommon.canValid(this.formData)) { return; }
    this.isTransmitting = true;
    const data: EditSubSystemRequestModel = {
      ID: this.subSystemID,
      Name: this.formData.value.name,
      Code: this.formData.value.code,
      Style: this.formData.value.style,
      Display: this.formData.value.display,
      Data: this.formData.value.data
    };
    const success = (result: ResultModel) => {
      if (this.saveEnd) {
        this.saveEnd.emit(result);
      }
    };
    const complete = () => {
      this.isTransmitting = false;
    };
    if (this.isAdd) {
      this.subSystemService.addSubSystem(data, success, complete);
    } else {
      this.subSystemService.editSubSystem(data, success, complete);
    }
  }
}
