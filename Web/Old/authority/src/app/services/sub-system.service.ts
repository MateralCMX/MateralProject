import { Injectable } from '@angular/core';
import { BasiceService } from './BasiceService';
import { Router } from '@angular/router';
import { HttpClient } from '@angular/common/http';
import { NzMessageService } from 'ng-zorro-antd';
import { EditSubSystemRequestModel } from './models/sub-system/EditSubSystemRequestModel';
import { ResultModel } from './models/result/resultModel';
import { ResultDataModel } from './models/result/resultDataModel';
import { SubSystemDTO } from './models/sub-system/SubSystemDTO';
import { QuerySubSystemFilterRequestModel } from './models/sub-system/QuerySubSystemFilterRequestModel';
import { PageResultModel } from './models/result/pageResultModel';
import { SubSystemListDTO } from './models/sub-system/SubSystemListDTO';
import { ExchangeIndexRequestModel } from './models/base/ExchangeIndexRequestModel';

@Injectable({
  providedIn: 'root'
})
export class SubSystemService extends BasiceService {
  constructor(protected route: Router, protected http: HttpClient, protected message: NzMessageService) {
    super(route, http, message);
  }
  /**
   * 添加子系统
   */
  public addSubSystem(data: EditSubSystemRequestModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/SubSystem/AddSubSystem', data, success, null, complete);
  }
  /**
   * 修改子系统
   */
  public editSubSystem(data: EditSubSystemRequestModel, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/SubSystem/EditSubSystem', data, success, null, complete);
  }
  /**
   * 删除子系统
   */
  public deleteSubSystem(id: string, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendGet('/SubSystem/DeleteSubSystem', { id }, success, null, complete);
  }
  /**
   * 获得子系统信息
   */
  public getSubSystemInfo(id: string, success?: (value: ResultDataModel<SubSystemDTO>) => void, complete?: () => void) {
    return this.sendGet('/SubSystem/GetSubSystemInfo', { id }, success, null, complete);
  }
  /**
   * 获得子系统列表
   */
  public getSubSystemList(data: QuerySubSystemFilterRequestModel,
                          success?: (value: PageResultModel<SubSystemListDTO>) => void, complete?: () => void) {
    return this.sendPost('/SubSystem/GetSubSystemList', data, success, null, complete);
  }
  /**
   * 调换子系统位序
   */
  public exchangeSubSystemIndex(data: ExchangeIndexRequestModel<string>, success?: (value: ResultModel) => void, complete?: () => void) {
    return this.sendPost('/SubSystem/ExchangeSubSystemIndex', data, success, null, complete);
  }
}
