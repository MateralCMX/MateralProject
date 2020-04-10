import axios, { AxiosResponse } from 'axios';
import { ResultModel } from '../services/models/result/ResultModel';
import { ResultTypeEnum } from '../services/models/result/ResultTypeEnum';
import { AuthorityHelper } from './AuthorityHelper';

export class BasiceService {
    protected baseUrl = 'http://127.0.0.1:18100/';
    protected apiName = 'api';
    /**
     * 发送Post请求
     * @param url url地址
     * @param data 数据
     * @param success 成功回调
     * @param fail 失败回调
     * @param complete 都执行回调
     */
    protected async sendPostUrl<T extends object>(url: string, data: T) {
        const respones = await axios({
            headers: this.getHttpHeaders(),
            method: 'post',
            url,
            data,
        });
        if (respones.status !== 200) {
            this.handlerError(respones);
        } else {
            const result = respones.data as ResultModel;
            if (result.ResultType !== ResultTypeEnum.Success) {
                (window as any).$mssageService.warning(result.Message);
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
    protected async sendPost<T extends object>(url: string, data: T) {
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
    protected async sendGetUrl<T extends object>(url: string, data: T) {
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
            url,
        });
        if (respones.status !== 200) {
            this.handlerError(respones);
        } else {
            const result = respones.data as ResultModel;
            if (result.ResultType !== ResultTypeEnum.Success) {
                (window as any).$mssageService.warning(result.Message);
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
    protected async sendGet<T extends object>(url: string, data: T) {
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
                (window as any).$mssageService.warning('认证失败，请重新登录');
                (window as any).$goToLogin();
                break;
            case 500:
                (window as any).$mssageService.error('服务器发生错误');
                break;
            default:
                (window as any).$mssageService.error('网络请求错误');
                break;
        }
    }
}
