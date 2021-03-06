"use strict";
var __awaiter = (this && this.__awaiter) || function (thisArg, _arguments, P, generator) {
    function adopt(value) { return value instanceof P ? value : new P(function (resolve) { resolve(value); }); }
    return new (P || (P = Promise))(function (resolve, reject) {
        function fulfilled(value) { try { step(generator.next(value)); } catch (e) { reject(e); } }
        function rejected(value) { try { step(generator["throw"](value)); } catch (e) { reject(e); } }
        function step(result) { result.done ? resolve(result.value) : adopt(result.value).then(fulfilled, rejected); }
        step((generator = generator.apply(thisArg, _arguments || [])).next());
    });
};
var __generator = (this && this.__generator) || function (thisArg, body) {
    var _ = { label: 0, sent: function() { if (t[0] & 1) throw t[1]; return t[1]; }, trys: [], ops: [] }, f, y, t, g;
    return g = { next: verb(0), "throw": verb(1), "return": verb(2) }, typeof Symbol === "function" && (g[Symbol.iterator] = function() { return this; }), g;
    function verb(n) { return function (v) { return step([n, v]); }; }
    function step(op) {
        if (f) throw new TypeError("Generator is already executing.");
        while (_) try {
            if (f = 1, y && (t = op[0] & 2 ? y["return"] : op[0] ? y["throw"] || ((t = y["return"]) && t.call(y), 0) : y.next) && !(t = t.call(y, op[1])).done) return t;
            if (y = 0, t) op = [op[0] & 2, t.value];
            switch (op[0]) {
                case 0: case 1: t = op; break;
                case 4: _.label++; return { value: op[1], done: false };
                case 5: _.label++; y = op[1]; op = [0]; continue;
                case 7: op = _.ops.pop(); _.trys.pop(); continue;
                default:
                    if (!(t = _.trys, t = t.length > 0 && t[t.length - 1]) && (op[0] === 6 || op[0] === 2)) { _ = 0; continue; }
                    if (op[0] === 3 && (!t || (op[1] > t[0] && op[1] < t[3]))) { _.label = op[1]; break; }
                    if (op[0] === 6 && _.label < t[1]) { _.label = t[1]; t = op; break; }
                    if (t && _.label < t[2]) { _.label = t[2]; _.ops.push(op); break; }
                    if (t[2]) _.ops.pop();
                    _.trys.pop(); continue;
            }
            op = body.call(thisArg, _);
        } catch (e) { op = [6, e]; y = 0; } finally { f = t = 0; }
        if (op[0] & 5) throw op[1]; return { value: op[0] ? op[1] : void 0, done: true };
    }
};
var __importDefault = (this && this.__importDefault) || function (mod) {
    return (mod && mod.__esModule) ? mod : { "default": mod };
};
Object.defineProperty(exports, "__esModule", { value: true });
var axios_1 = __importDefault(require("axios"));
var resultTypeEnum_1 = require("./models/result/resultTypeEnum");
var authorityHelper_1 = require("../common/authorityHelper");
var BasiceService = /** @class */ (function () {
    function BasiceService(router, message) {
        this.router = router;
        this.message = message;
        this.baseUrl = 'http://127.0.0.1:18100/';
        this.apiName = "api";
    }
    /**
     * 发送Post请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    BasiceService.prototype.sendPostUrl = function (url, data) {
        return __awaiter(this, void 0, void 0, function () {
            var respones, result;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0: return [4 /*yield*/, axios_1.default({
                            headers: this.getHttpHeaders(),
                            method: 'post',
                            url: url,
                            data: data
                        })];
                    case 1:
                        respones = _a.sent();
                        if (respones.status !== 200) {
                            this.handlerError(respones);
                        }
                        else {
                            result = respones.data;
                            if (result.ResultType !== resultTypeEnum_1.ResultTypeEnum.Success) {
                                this.message.warning(result.Message);
                            }
                            return [2 /*return*/, result];
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    /**
     * 发送Post请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    BasiceService.prototype.sendPost = function (url, data) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        url = "" + this.baseUrl + this.apiName + url;
                        return [4 /*yield*/, this.sendPostUrl(url, data)];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    };
    /**
     * 发送Get请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    BasiceService.prototype.sendGetUrl = function (url, data) {
        return __awaiter(this, void 0, void 0, function () {
            var key, item, respones, result;
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        if (data) {
                            url += '?';
                            for (key in data) {
                                if (data.hasOwnProperty(key)) {
                                    item = data[key];
                                    url += key + "=" + item + "&";
                                }
                            }
                            url = url.substr(0, url.length - 1);
                        }
                        return [4 /*yield*/, axios_1.default({
                                headers: this.getHttpHeaders(),
                                method: 'get',
                                url: url
                            })];
                    case 1:
                        respones = _a.sent();
                        if (respones.status !== 200) {
                            this.handlerError(respones);
                        }
                        else {
                            result = respones.data;
                            if (result.ResultType !== resultTypeEnum_1.ResultTypeEnum.Success) {
                                this.message.warning(result.Message);
                            }
                            return [2 /*return*/, result];
                        }
                        return [2 /*return*/];
                }
            });
        });
    };
    /**
     * 发送Post请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    BasiceService.prototype.sendGet = function (url, data) {
        return __awaiter(this, void 0, void 0, function () {
            return __generator(this, function (_a) {
                switch (_a.label) {
                    case 0:
                        url = "" + this.baseUrl + this.apiName + url;
                        return [4 /*yield*/, this.sendGetUrl(url, data)];
                    case 1: return [2 /*return*/, _a.sent()];
                }
            });
        });
    };
    /**
     * 获得Http请求头
     */
    BasiceService.prototype.getHttpHeaders = function () {
        var data = { 'Content-Type': 'application/json' };
        var token = authorityHelper_1.AuthorityHelper.getToken();
        if (token) {
            data.Authorization = "Bearer " + token;
        }
        return data;
    };
    /**
     * 处理错误
     * @param error 错误
     */
    BasiceService.prototype.handlerError = function (error) {
        switch (error.status) {
            case 401:
                this.message.warning('认证失败，请重新登录');
                this.router.push({ name: 'Login' });
                break;
            case 500:
                this.message.error('服务器发生错误');
                break;
            default:
                this.message.error('网络请求错误');
                break;
        }
    };
    return BasiceService;
}());
exports.BasiceService = BasiceService;
//# sourceMappingURL=basiceService.js.map