import { createRouter, createWebHistory } from 'vue-router'
import type { RouteRecordRaw } from 'vue-router'
import TaskList from '@/pages/TaskList.vue'
import About from '@/pages/About.vue'

const routes: RouteRecordRaw[] = [
  {
    path: '/',
    name: 'TaskList',
    component: TaskList,
            meta: {
          title: 'TaskMaster - Minhas Tarefas'
        }
  },
  {
    path: '/about',
    name: 'About',
    component: About,
            meta: {
          title: 'TaskMaster - Sobre'
        }
  },
  {
    path: '/:pathMatch(.*)*',
    redirect: '/'
  }
]

const router = createRouter({
  history: createWebHistory(),
  routes
})

// Middleware para atualizar o título da página
router.beforeEach((to, from, next) => {
  if (to.meta.title) {
    document.title = to.meta.title as string
  }
  next()
})

export default router
