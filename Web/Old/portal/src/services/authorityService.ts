import { BasiceService } from './basiceService';
import { LoginModel } from './models/authority/user/loginModel';
import { ResultDataModel } from './models/result/resultDataModel';
import { TokenResultModel } from './models/authority/user/tokenResultModel';
import { ResultTypeEnum } from './models/result/resultTypeEnum';
import { AuthorityHelper } from '../common/authorityHelper';

export class AuthorityService extends BasiceService {
    public constructor(protected router: any, protected message: any) {
        super(router, message);
        this.apiName = "AuthorityAPI"
    }
    /**
    * 登录
    */
    public async login(data: LoginModel) {
        const result = await this.sendPost('/User/Login', data) as ResultDataModel<TokenResultModel>;
        if (result.ResultType === ResultTypeEnum.Success) {
            AuthorityHelper.setToken(result.Data.AccessToken);
        }
        return result;
    }
}