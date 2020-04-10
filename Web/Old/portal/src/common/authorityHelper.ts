export class AuthorityHelper {
    private static tokenKey = 'token'
    public static getToken() {
        return localStorage.getItem(this.tokenKey);
    }
    public static setToken(token: string) {
        return localStorage.setItem(this.tokenKey, token);
    }
    public static removeToken() {
        return localStorage.removeItem(this.tokenKey);
    }
}