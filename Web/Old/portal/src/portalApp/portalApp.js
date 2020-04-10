import { registerMicroApps, runAfterFirstMounted, setDefaultMountApp, start} from 'qiankun';
import render from './vueRender';

export const PortalApp = function () {
    const startOptions = {
        prefetch: true,
        jsSandbox: true,
        singular: true,
    };
    const lifeCycles = {
        beforeLoad: [app => beforeLoad(app)],
        beforeMount: [app => beforeMount(app)],
        afterMount: [app => afterMount(app)],
        beforeUnmount: [app => beforeUnmount(app)],
        afterUnmount: [app => afterUnmount(app)]
    };
    /**
     * 路由监听
     */
    function activeRule(routerPrefix) {
        return () => location.hash.startsWith('#/' + routerPrefix);
    }
    /**
     * 加载前回调
     */
    function beforeLoad(app) {
    }
    /**
     * 挂载前回调
     */
    function beforeMount(app) {
    }
    /**
     * 挂载后回调
     */
    function afterMount(app) {
    }
    /**
     * 卸载前回调
     */
    function beforeUnmount(app) {
    }
    /**
     * 卸载后回调
     */
    function afterUnmount(app) {
    }
    /**
     * 第一次挂载完毕
     */
    function onFirstMounted() {
    }
    /**
     * 初始化
     */
    function Init(subAppList) {
        const subApps = [];
        subAppList.forEach(subApp => {
            subApps.push({
                name: subApp.name,
                entry: subApp.address,
                render,
                activeRule: activeRule(subApp.name),
            });
        });
        registerMicroApps(subApps, lifeCycles);
        runAfterFirstMounted(() => onFirstMounted());
        setDefaultMountApp('/');
        start(startOptions);
    }
    return {
        Init
    };
};