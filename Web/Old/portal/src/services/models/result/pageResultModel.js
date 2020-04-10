"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var resultTypeEnum_1 = require("./resultTypeEnum");
var pageModel_1 = require("./pageModel");
/**
 * 分页返回模型
 */
var PageResultModel = /** @class */ (function () {
    function PageResultModel() {
        this.Message = '';
        this.ResultType = resultTypeEnum_1.ResultTypeEnum.Success;
        this.Data = [];
        this.PageModel = new pageModel_1.PageModel();
    }
    return PageResultModel;
}());
exports.PageResultModel = PageResultModel;
//# sourceMappingURL=pageResultModel.js.map