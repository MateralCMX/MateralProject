"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
var AuthorityHelper = /** @class */ (function () {
    function AuthorityHelper() {
    }
    AuthorityHelper.getToken = function () {
        return localStorage.getItem(this.tokenKey);
    };
    AuthorityHelper.setToken = function (token) {
        return localStorage.setItem(this.tokenKey, token);
    };
    AuthorityHelper.removeToken = function () {
        return localStorage.removeItem(this.tokenKey);
    };
    AuthorityHelper.tokenKey = 'token';
    return AuthorityHelper;
}());
exports.AuthorityHelper = AuthorityHelper;
//# sourceMappingURL=authorityHelper.js.map