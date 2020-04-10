export interface ExchangeIndexRequestModel<T> {
    /**
     * 交换唯一标识
     */
    ExchangeID: T;
    /**
     * 目标唯一标识
     */
    TargetID: T;
    /**
     * 位置
     */
    ForUnder: boolean;
}
