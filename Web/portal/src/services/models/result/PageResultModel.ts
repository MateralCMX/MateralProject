import { ResultModel } from './ResultModel';
import { ResultTypeEnum } from './ResultTypeEnum';
import { PageModel } from './PageModel';

/**
 * 分页返回模型
 */
export class PageResultModel<T> implements ResultModel {
    public Message: string = '';
    public ResultType: ResultTypeEnum = ResultTypeEnum.Success;
    public Data: T[] = [];
    public PageModel: PageModel = new PageModel();
}
