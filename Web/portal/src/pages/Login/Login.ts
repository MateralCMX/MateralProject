import './Login.css';
import { BasiceViewModel } from '../../common/BasiceViewModel';
import { FormHelper } from '../../common/FormHelper';
import { UserService } from '../../services/UserService'
import { ResultTypeEnum } from '../../services/models/result/ResultTypeEnum';

class LoginViewModel extends BasiceViewModel {
    private userService: UserService = new UserService();
    private loginFormHelper: FormHelper;
    private btnLogin: HTMLButtonElement;
    /**
     * 构造方法
     */
    public constructor() {
        super(false);
        this.loginFormHelper = new FormHelper('loginForm');
        this.initDom();
        this.initEvent();
    }
    /**
     * 设置登录状态
     * @param state 是
     */
    private setLoggingState(state: boolean) {
        if (state) {
            this.btnLogin.setAttribute('disabled', 'disabled');
        } else {
            this.btnLogin.removeAttribute('disabled');
        }
    }
    /**
     * 初始化Dom
     */
    private initDom() {
        this.btnLogin = document.getElementById('btnLogin') as HTMLButtonElement;
    }
    /**
     * 初始化事件
     */
    private initEvent() {
        if (this.btnLogin) {
            this.btnLogin.addEventListener('click', async () => await this.onBtnLoginClickAsync());
        }
    }
    /**
     * 登录按钮单击
     */
    private async onBtnLoginClickAsync() {
        this.setLoggingState(true);
        if (!this.loginFormHelper.checkValidity()) {
            this.setLoggingState(false);
            return;
        }
        this.loginFormHelper.setDisabled(true);
        const result = await this.userService.login({
            Account: this.loginFormHelper.value.account,
            Password: this.loginFormHelper.value.password
        });
        if (result.ResultType === ResultTypeEnum.Success) {
            window.location.href = "/";
            return;
        }
        this.loginFormHelper.setDisabled(false);
        this.setLoggingState(false);
    }
}
window.addEventListener('load', () => {
    new LoginViewModel();
})