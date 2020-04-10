import 'zone.js';
import Vue from 'vue';
// import Antd from 'ant-design-vue';
import App from './App.vue';
import router from './router';
import store from './store';
import { PortalApp } from './portalApp/portalApp';
import { subAppList } from './portalApp/subAppList';
import { AuthorityHelper } from './common/authorityHelper';
// import 'ant-design-vue/dist/antd.css';

if (subAppList.length > 0) {
  const portalApp = new PortalApp();
  portalApp.Init(subAppList);
}
window.AuthorityHelper = AuthorityHelper;
window.GotoLogin = () => {
  AuthorityHelper.removeToken();
  window.$router.push({ name: 'Login' });
};
Vue.config.productionTip = false;
// Vue.use(Antd);
new Vue({
  router,
  store,
  render: h => h(App)
}).$mount('#app');
