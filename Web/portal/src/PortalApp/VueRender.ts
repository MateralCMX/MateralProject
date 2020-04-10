import Vue from 'vue/dist/vue.esm';
let app: any = null;
/**
 * 获得Vue渲染器
 */
function getVueRender({ appContent, loading }: any) {
    return new Vue({
        template: `
          <div id="subapp-container">
            <h4 v-if="loading" class="subapp-loading">Loading...</h4>
            <div v-html="appContent" />
          </div>
        `,
        el: '#subapp-container',
        data() {
          return {
            appContent,
            loading,
          };
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
