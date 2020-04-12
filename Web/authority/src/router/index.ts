import Vue from 'vue';
import VueRouter, { RouteConfig } from 'vue-router';
import Index from '../views/Index.vue';
import userRoutes from './userRouter';

Vue.use(VueRouter);

const routes: RouteConfig[] = [
  {
    path: '/authority',
    name: 'Authority',
    component: Index,
    children: [
      userRoutes,
    ],
  },
];
const router = new VueRouter({
  routes,
});
(window as any).$router = router;
(window as any).navigate = (data: any) => {
  if (data && data.name) {
    router.push({ name: data.name });
  }
};
export default router;
