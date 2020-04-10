import { Component, OnInit, ViewChild } from '@angular/core';
import { UserEditComponent } from '../user-edit/user-edit.component';
import { FormGroup, FormControl } from '@angular/forms';
import { UserListDTO } from 'src/app/services/models/user/UserListDTO';
import { PageModel } from 'src/app/services/models/result/pageModel';
import { UserService } from 'src/app/services/user.service';
import { NzMessageService } from 'ng-zorro-antd';
import { QueryUserFilterRequestModel } from 'src/app/services/models/user/QueryUserFilterRequestModel';
import { PageResultModel } from 'src/app/services/models/result/pageResultModel';
import { ResultModel } from 'src/app/services/models/result/resultModel';
import { DeleteModalComponent } from 'src/app/components/delete-modal/delete-modal.component';

@Component({
  selector: 'app-user-list',
  templateUrl: './user-list.component.html',
  styleUrls: ['./user-list.component.less']
})
export class UserListComponent implements OnInit {
  @ViewChild('userEditComponent', { static: false })
  public userEditComponent: UserEditComponent;
  @ViewChild('deleteModalComponent', { static: false })
  public deleteModalComponent: DeleteModalComponent;
  public searchModel: FormGroup;
  public dataLoading = false;
  public tableData: UserListDTO[] = [];
  public pageModel: PageModel = {
    DataCount: 0,
    PageCount: 0,
    PageIndex: 1,
    PageSize: 10
  };
  public isAdd = false;
  public drawerVisible = false;
  public constructor(private userService: UserService, private message: NzMessageService) { }
  /**
   * 初始化
   */
  public ngOnInit() {
    this.searchModel = new FormGroup({
      account: new FormControl({ value: null, disabled: this.dataLoading }),
      name: new FormControl({ value: null, disabled: this.dataLoading })
    });
    this.search(1);
  }
  /**
   * 打开抽屉
   * @param userID 用户唯一标识
   */
  public openDrawer(userID: string): void {
    this.userEditComponent.InitData(userID);
    this.isAdd = this.userEditComponent.isAdd;
    this.drawerVisible = true;
  }
  /**
   * 查询
   * @param index 页码
   */
  public search(index) {
    this.dataLoading = true;
    const data: QueryUserFilterRequestModel = {
      Account: this.searchModel.value.account,
      Name: this.searchModel.value.name,
      PageIndex: index,
      PageSize: this.pageModel.PageSize
    };
    const success = (result: PageResultModel<UserListDTO>) => {
      this.tableData = result.Data;
      this.pageModel = result.PageModel;
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.userService.getUserList(data, success, complete);
  }
  /**
   * 关闭抽屉
   */
  public closeDrawer() {
    this.drawerVisible = false;
  }
  /**
   * 保存结束
   * @param result 返回结果
   */
  public saveEnd(result: ResultModel) {
    this.message.success(result.Message);
    this.search(this.pageModel.PageIndex);
    this.closeDrawer();
  }
  /**
   * 删除用户
   * @param userID 用户唯一标识
   */
  public deleteUser(userID: string): void {
    this.dataLoading = true;
    const success = (result: ResultModel) => {
      if (this.pageModel.PageIndex === this.pageModel.PageCount && this.pageModel.DataCount % this.pageModel.PageSize === 1) {
        this.search(this.pageModel.PageIndex - 1);
      } else {
        this.search(this.pageModel.PageIndex);
      }
      this.message.success(result.Message);
    };
    const complete = () => {
      this.dataLoading = false;
      this.deleteModalComponent.close();
    };
    this.userService.deleteUser(userID, success, complete);
  }
  /**
   * 页码改变
   * @param index 页码
   */
  public onPageChange(index) {
    this.search(index);
  }
  /**
   * 打开删除窗口
   * @param userID 用户唯一标识
   */
  public openDeleteModal(userID) {
    this.deleteModalComponent.open(userID);
  }
}
