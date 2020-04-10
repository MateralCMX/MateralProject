import { ResultTypeEnum } from './ResultTypeEnum';

/**
 * 返回对象
 */
export interface ResultModel {
    Message: string;
    ResultType: ResultTypeEnum;
}
