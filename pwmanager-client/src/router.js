import { createWebHistory, createRouter } from "vue-router";
import Login from "./components/Login.vue";

const BoardUser = () => import("./components/BoardUser.vue")

const routes = [
  {
    path: "/",
    name: "home",
    component: BoardUser,
  },
  {
    path: "/home",
    component: BoardUser,
  },
  {
    path: "/login",
    component: Login,
  }
];

const router = createRouter({
  history: createWebHistory(),
  routes,
});

router.beforeEach((to, from, next) => {
    const publicPages = ['/login'];
    const authRequired = !publicPages.includes(to.path);
    const loggedIn = localStorage.getItem('user');
      
    if (authRequired && !loggedIn) {
      next('/login');
    } else {
      next();
    }
  });

export default router;