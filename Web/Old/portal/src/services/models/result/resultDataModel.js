"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var resultTypeEnum_1 = require("./resultTypeEnum");
/**
 * 返回对象
 */
var ResultDataModel = /** @class */ (function () {
    function ResultDataModel() {
        this.Message = '';
        this.ResultType = resultTypeEnum_1.ResultTypeEnum.Success;
        this.Data = {};
    }
    return ResultDataModel;
}());
exports.ResultDataModel = ResultDataModel;
//# sourceMappingURL=resultDataModel.js.map