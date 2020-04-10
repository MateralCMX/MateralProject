export class AuthorityHelper {
    /**
     * 获得Token
     */
    public static getToken() {
        return localStorage.getItem(this.tokenKey);
    }
    /**
     * 设置Token
     */
    public static setToken(token: string) {
        return localStorage.setItem(this.tokenKey, token);
    }
    /**
     * 移除Token
     */
    public static removeToken() {
        return localStorage.removeItem(this.tokenKey);
    }
    private static tokenKey = 'token';
}
