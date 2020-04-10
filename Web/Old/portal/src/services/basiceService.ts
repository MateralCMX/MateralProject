import axios, { AxiosResponse } from 'axios';
import { ResultModel } from './models/result/resultModel';
import { ResultTypeEnum } from './models/result/resultTypeEnum';
import { AuthorityHelper } from '../common/authorityHelper';

export class BasiceService {
    protected baseUrl = 'http://127.0.0.1:18100/';
    protected apiName = "api";
    public constructor(protected router: any, protected message: any) { }
    /**
     * 发送Post请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    protected async sendPostUrl<T extends Object>(url: string, data: T) {
        const respones = await axios({
            headers: this.getHttpHeaders(),
            method: 'post',
            url: url,
            data: data
        });
        if (respones.status !== 200) {
            this.handlerError(respones);
        } else {
            const result = respones.data as ResultModel;
            if (result.ResultType !== ResultTypeEnum.Success) {
                this.message.warning(result.Message);
            }
            return result;
        }
    }
    /**
     * 发送Post请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    protected async sendPost<T extends Object>(url: string, data: T) {
        url = `${this.baseUrl}${this.apiName}${url}`;
        return await this.sendPostUrl(url, data);
    }
    /**
     * 发送Get请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    protected async sendGetUrl<T extends Object>(url: string, data: T) {
        if (data) {
            url += '?';
            for (const key in data) {
                if (data.hasOwnProperty(key)) {
                    const item = data[key];
                    url += `${key}=${item}&`;
                }
            }
            url = url.substr(0, url.length - 1);
        }
        const respones = await axios({
            headers: this.getHttpHeaders(),
            method: 'get',
            url: url
        });
        if (respones.status !== 200) {
            this.handlerError(respones);
        } else {
            const result = respones.data as ResultModel;
            if (result.ResultType !== ResultTypeEnum.Success) {
                this.message.warning(result.Message);
            }
            return result;
        }
    }
    /**
     * 发送Post请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    protected async sendGet<T extends Object>(url: string, data: T) {
        url = `${this.baseUrl}${this.apiName}${url}`;
        return await this.sendGetUrl(url, data);
    }
    /**
     * 获得Http请求头
     */
    protected getHttpHeaders() {
        const data: any = { 'Content-Type': 'application/json' };
        const token = AuthorityHelper.getToken();
        if (token) {
            data.Authorization = `Bearer ${token}`;
        }
        return data;
    }
    /**
     * 处理错误
     * @param error 错误
     */
    protected handlerError(error: AxiosResponse) {
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
    }
}