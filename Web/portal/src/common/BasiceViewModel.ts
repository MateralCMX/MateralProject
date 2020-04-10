import './style.css';
import { AuthorityHelper } from './AuthorityHelper';
import { MssageService } from './MssageService'

export class BasiceViewModel {
    constructor(private isLogin: boolean = true) {
        if (this.isLogin) {
            this.loginGuard();
        }
        this.bindPublicWindow();
    }
    /**
     * 绑定公用Window
     */
    private bindPublicWindow() {
        const windowObject = window as any;
        windowObject.$goToLogin = this.goToLogin;
        windowObject.$authorityHelper = AuthorityHelper;
        windowObject.$mssageService = MssageService;
    }
    /**
     * 登录守卫
     */
    private loginGuard() {
        const token = AuthorityHelper.getToken();
        if (token) return;
        this.goToLogin();
    }
    /**
     * 跳转到登录页面
     */
    private goToLogin() {
        const loginUrl = "/Login.html";
        if (window.location.pathname !== loginUrl) {
            window.location.href = "/Login.html";
        }
    }
}