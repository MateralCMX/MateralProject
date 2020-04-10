import Vue from 'vue';
import ElementUI from 'element-ui';
import App from './App.vue';
import router from './router';
import { subAppList } from './portalApp/subAppList';
import { PortalApp } from './portalApp/PortalApp';
import { AuthorityHelper } from './common/authorityHelper';
import 'element-ui/lib/theme-chalk/index.css';

if (subAppList.length > 0) {
  const portalApp = new PortalApp();
  portalApp.Init(subAppList);
}
(window as any).AuthorityHelper = AuthorityHelper;
(window as any).GotoLogin = () => {
  AuthorityHelper.removeToken();
  (window as any).$router.push({ name: 'Login' });
};
Vue.config.productionTip = false;
Vue.use(ElementUI);
new Vue({
  router,
  render: (h) => h(App),
}).$mount('#portalApp');
