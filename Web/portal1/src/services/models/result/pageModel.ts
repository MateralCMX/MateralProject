import { PageRequestModel } from './pageRequestModel';

/**
 * 分页模型
 */
export class PageModel implements PageRequestModel {
    public PageIndex: number = 1;
    public PageSize: number = 10;
    public PageCount: number = 0;
    public DataCount: number = 0;
}
