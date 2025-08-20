import { createApp } from 'vue'
import { createPinia } from 'pinia'
import App from './App.vue'
import router from './router'

// Cria a instância da aplicação
const app = createApp(App)

// Configura o Pinia para gerenciamento de estado
const pinia = createPinia()
app.use(pinia)

// Configura o roteador
app.use(router)

// Monta a aplicação
app.mount('#app')
