import { Component, OnInit, Output, EventEmitter } from '@angular/core';
import { FormGroup, FormControl, Validators } from '@angular/forms';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { UserService } from 'src/app/services/user.service';
import { FormGroupCommon } from 'src/app/components/form-group-common';
import { ResultDataModel } from 'src/app/services/models/result/resultDataModel';
import { UserDTO } from 'src/app/services/models/user/UserDTO';
import { EditUserRequestModel } from 'src/app/services/models/user/EditUserRequestModel';

@Component({
  selector: 'app-user-edit',
  templateUrl: './user-edit.component.html',
  styleUrls: ['./user-edit.component.less']
})
export class UserEditComponent implements OnInit {
  public userID: string;
  public isAdd = false;
  public formData: FormGroup;
  public isTransmitting = false;
  @Output()
  public saveEnd = new EventEmitter<ResultModel>();
  constructor(private userService: UserService, private formGroupCommon: FormGroupCommon) { }
  public ngOnInit() {
    this.formData = new FormGroup({
      name: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required, Validators.maxLength(100)]),
      account: new FormControl({ value: null, disabled: this.isTransmitting }, [Validators.required, Validators.maxLength(100)]),
      password: new FormControl({ value: null, disabled: this.isTransmitting })
    });
  }
  public InitData(userID) {
    this.userID = userID;
    if (!this.userID) {
      this.isAdd = true;
      this.formData.setValue({
        name: null,
        account: null,
        password: null
      });
    } else {
      this.isAdd = false;
      this.isTransmitting = true;
      const success = (result: ResultDataModel<UserDTO>) => {
        this.formData.setValue({
          name: result.Data.Name,
          account: result.Data.Account,
          password: null
        });
      };
      const complete = () => {
        this.isTransmitting = false;
      };
      this.userService.getUserInfo(this.userID, success, complete);
    }
  }
  public save() {
    if (!this.formGroupCommon.canValid(this.formData)) { return; }
    this.isTransmitting = true;
    const data: EditUserRequestModel = {
      ID: this.userID,
      Account: this.formData.value.account,
      Name: this.formData.value.name,
      Password: this.formData.value.password
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
      this.userService.addUser(data, success, complete);
    } else {
      this.userService.editUser(data, success, complete);
    }
  }
}
