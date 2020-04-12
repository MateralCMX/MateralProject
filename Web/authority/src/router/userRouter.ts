import { RouteConfig } from 'vue-router';
import UserIndex from '../views/user/UserIndex.vue';
import UserList from '../views/user/UserList.vue';

const userRoutes: RouteConfig = {
    path: 'User',
    name: 'User',
    component: UserIndex,
    children: [
        {
            path: 'List',
            name: 'UserList',
            component: UserList,
        },
    ],
};
export default userRoutes;
