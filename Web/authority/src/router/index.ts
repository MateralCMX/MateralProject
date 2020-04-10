import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import Index from '../views/Index.vue';
import Home from '../views/Home.vue';
import About from '../views/About.vue';

Vue.use(VueRouter);

const routes: RouteConfig[] = [
  {
    path: '/authority',
    name: 'Index',
    component: Index,
    children: [
      {
        path: '/',
        name: 'Home',
        component: Home,
      },
      {
        path: '/about',
        name: 'About',
        component: About,
      },
    ],
  },
];
const router = new VueRouter({
  routes,
});

export default router;
