import Vue from 'vue';
import VueRouter from 'vue-router';
import Index from '../views/Index.vue';
import Login from '../views/Login.vue';
import { AuthorityHelper } from '../common/authorityHelper';
import Enumerable from 'linq';

Vue.use(VueRouter);

const routes = [
  {
    path: '/Login',
    name: 'Login',
    component: Login,
  },
  {
    path: '*',
    name: 'Index',
    component: Index,
  },
];

const router = new VueRouter({
  routes,
});
const whiteList = ['Login'];
router.beforeEach((to, from, next) => {
  if (Enumerable.from(whiteList).any((m) => m === to.name)) {
    next();
  } else {
    const token = AuthorityHelper.getToken();
    if (token) {
      next();
    } else {
      next({ name: 'Login' });
    }
  }
});
(window as any).$router = router;
export default router;
