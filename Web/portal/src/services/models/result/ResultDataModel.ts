import { ResultModel } from './ResultModel';
import { ResultTypeEnum } from './ResultTypeEnum';

/**
 * 返回对象
 */
export class ResultDataModel<T> implements ResultModel {
    public Message: string = '';
    public ResultType: ResultTypeEnum = ResultTypeEnum.Success;
    public Data: T = {} as T;
}
