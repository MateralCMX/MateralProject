export interface QuerySubSystemFilterRequestModel {
    /**
     * 名称
     */
    Name: string;
    /**
     * 代码
     */
    Code: string;
    /**
     * 是否显示
     */
    Display: boolean;
    /**
     * 页码
     */
    PageIndex: number;
    /**
     * 每页显示数量
     */
    PageSize: number;
}
