import Vue from 'vue';
import SubApp from '@/components/SubApp.vue';
let app: any;
/**
 * 获得Vue渲染器
 */
function getVueRender({appContent, loading}: any) {
    return new Vue({
        el: '#subapp-container',
        data() {
            return {
                appContent,
                loading,
            };
        },
        render(event) {
            return event(SubApp, {
                props: {
                    appContent,
                    loading,
                },
            });
        },
    });
}
/**
 * 渲染器
 */
export default function render({ appContent, loading }: any) {
    if (!app) {
        app = getVueRender({ appContent, loading });
    } else {
        app.appContent = appContent;
        app.loading = loading;
    }
}
