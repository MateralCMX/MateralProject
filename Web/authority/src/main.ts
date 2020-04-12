import Vue from 'vue';
import App from './App.vue';
import ElementUI from 'element-ui';
import router from './router';
import 'element-ui/lib/theme-chalk/index.css';
Vue.config.productionTip = false;
Vue.use(ElementUI);
let instance: Vue | null = null;
function render() {
  instance = new Vue({
    router,
    render: (h) => h(App),
  }).$mount('#app');
}
if (!(window as any).__POWERED_BY_QIANKUN__) {
  render();
} else {
  __webpack_public_path__ = (window as any).__INJECTED_PUBLIC_PATH_BY_QIANKUN__;
}

// tslint:disable-next-line: no-empty
export async function bootstrap() { }

export async function mount(props: any) {
  render();
  const data: any = (window as any).historyData;
  if (data && data.name) {
    (window as any).$router.push({ name: data.name });
  }
}

export async function unmount() {
  if (instance) {
    (instance as Vue).$destroy();
    instance = null;
  }
}
