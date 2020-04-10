import { Component, OnInit, ViewChild } from '@angular/core';
import { SubSystemEditComponent } from '../sub-system-edit/sub-system-edit.component';
import { DeleteModalComponent } from 'src/app/components/delete-modal/delete-modal.component';
import { FormGroup, FormControl } from '@angular/forms';
import { SubSystemListDTO } from 'src/app/services/models/sub-system/SubSystemListDTO';
import { PageModel } from 'src/app/services/models/result/pageModel';
import { SubSystemService } from 'src/app/services/sub-system.service';
import { NzMessageService } from 'ng-zorro-antd';
import { QuerySubSystemFilterRequestModel } from 'src/app/services/models/sub-system/QuerySubSystemFilterRequestModel';
import { PageResultModel } from 'src/app/services/models/result/pageResultModel';
import { ResultModel } from 'src/app/services/models/result/resultModel';

@Component({
  selector: 'app-sub-system-list',
  templateUrl: './sub-system-list.component.html',
  styleUrls: ['./sub-system-list.component.less']
})
export class SubSystemListComponent implements OnInit {
  @ViewChild('subSystemEditComponent', { static: false })
  public subSystemEditComponent: SubSystemEditComponent;
  @ViewChild('deleteModalComponent', { static: false })
  public deleteModalComponent: DeleteModalComponent;
  public searchModel: FormGroup;
  public dataLoading = false;
  public tableData: SubSystemListDTO[] = [];
  public pageModel: PageModel = {
    DataCount: 0,
    PageCount: 0,
    PageIndex: 1,
    PageSize: 10
  };
  public isAdd = false;
  public drawerVisible = false;
  public constructor(private subSystemService: SubSystemService, private message: NzMessageService) { }
  /**
   * 初始化
   */
  public ngOnInit() {
    this.searchModel = new FormGroup({
      name: new FormControl({ value: null, disabled: this.dataLoading }),
      code: new FormControl({ value: null, disabled: this.dataLoading }),
      display: new FormControl({ value: null, disabled: this.dataLoading })
    });
    this.search(1);
  }
  /**
   * 打开抽屉
   * @param subSystemID 子系统唯一标识
   */
  public openDrawer(subSystemID: string): void {
    this.subSystemEditComponent.InitData(subSystemID);
    this.isAdd = this.subSystemEditComponent.isAdd;
    this.drawerVisible = true;
  }
  /**
   * 查询
   * @param index 页码
   */
  public search(index) {
    this.dataLoading = true;
    const data: QuerySubSystemFilterRequestModel = {
      Name: this.searchModel.value.name,
      Code: this.searchModel.value.code,
      Display: this.searchModel.value.display,
      PageIndex: index,
      PageSize: this.pageModel.PageSize
    };
    const success = (result: PageResultModel<SubSystemListDTO>) => {
      this.tableData = result.Data;
      this.pageModel = result.PageModel;
    };
    const complete = () => {
      this.dataLoading = false;
    };
    this.subSystemService.getSubSystemList(data, success, complete);
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
   * 删除子系统
   * @param subSystemID 子系统唯一标识
   */
  public deleteSubSystem(subSystemID: string): void {
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
    this.subSystemService.deleteSubSystem(subSystemID, success, complete);
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
   * @param subSystemID 子系统唯一标识
   */
  public openDeleteModal(subSystemID) {
    this.deleteModalComponent.open(subSystemID);
  }
}
