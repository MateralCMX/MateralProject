import { registerMicroApps, setDefaultMountApp, start, RegistrableApp } from 'qiankun';
import render from './VueRender';
import { SubAppModel } from './SubAppModel';

export class PortalApp {
    private startOptions = {
        jsSandbox: true,
        singular: false,
    };
    private lifeCycles = {
        beforeLoad: [],
        beforeMount: [],
        afterMount: [],
        beforeUnmount: [],
        afterUnmount: [],
    };
    /**
     * 初始化
     * @param subAppList 子应用列表
     */
    public Init(subAppList: SubAppModel[]) {
        const subApps: RegistrableApp[] = [];
        subAppList.forEach((subApp: SubAppModel) => {
            subApps.push({
                name: subApp.name,
                entry: subApp.address,
                render,
                activeRule: this.activeRule(subApp.name),
            });
        });
        registerMicroApps(subApps, this.lifeCycles);
        // runAfterFirstMounted(() => onFirstMounted());
        setDefaultMountApp('/');
        start(this.startOptions);
    }
    /**
     * 路由监听
     */
    private activeRule(routerPrefix: string) {
        return () => location.hash.startsWith('#/' + routerPrefix);
    }
}
