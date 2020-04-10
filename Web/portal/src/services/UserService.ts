import { LoginModel } from './models/authority/user/LoginModel';
import { ResultDataModel } from './models/result/ResultDataModel';
import { TokenResultModel } from './models/authority/user/TokenResultModel';
import { ResultTypeEnum } from './models/result/ResultTypeEnum';
import { AuthorityHelper } from '../common/AuthorityHelper';
import { BasiceService } from '../common/BasiceService';

export class UserService extends BasiceService {
    public constructor() {
        super();
        this.apiName = 'AuthorityAPI';
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
