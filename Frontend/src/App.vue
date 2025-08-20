<template>
  <div id="app">
    <header class="app-header">
      <div class="header-content">
        <h1 class="app-title">📝 TaskMaster</h1>
        <nav class="app-nav">
          <router-link to="/" class="nav-link">Tarefas</router-link>
          <router-link to="/about" class="nav-link">Sobre</router-link>
        </nav>
      </div>
    </header>

    <main class="app-main">
      <router-view />
    </main>

    <footer class="app-footer">
      <p>&copy; 2024 TaskMaster - Aplicação de To-Do com Vue 3 + TypeScript</p>
    </footer>

    <!-- Sistema de Alertas Global -->
    <AlertSystem />
  </div>
</template>

<script setup lang="ts">
import AlertSystem from '@/components/AlertSystem.vue'
// Componente principal da aplicação
</script>

<style>
/* Reset e estilos globais */
* {
  margin: 0;
  padding: 0;
  box-sizing: border-box;
}

body {
  font-family: -apple-system, BlinkMacSystemFont, 'Segoe UI', Roboto, Oxygen, Ubuntu, Cantarell, sans-serif;
  line-height: 1.6;
  color: #333;
  background: #f8f9fa;
}

#app {
  min-height: 100vh;
  display: flex;
  flex-direction: column;
}

.app-header {
  background: white;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  position: sticky;
  top: 0;
  z-index: 100;
}

.header-content {
  max-width: 1200px;
  margin: 0 auto;
  padding: 16px 24px;
  display: flex;
  justify-content: space-between;
  align-items: center;
}

.app-title {
  font-size: 1.8rem;
  font-weight: 700;
  color: #007bff;
  margin: 0;
}

.app-nav {
  display: flex;
  gap: 24px;
}

.nav-link {
  text-decoration: none;
  color: #666;
  font-weight: 500;
  padding: 8px 16px;
  border-radius: 6px;
  transition: all 0.2s ease;
}

.nav-link:hover {
  color: #007bff;
  background: #f8f9fa;
}

.nav-link.router-link-active {
  color: #007bff;
  background: #e3f2fd;
}

.app-main {
  flex: 1;
  padding: 0;
}

.app-footer {
  background: white;
  border-top: 1px solid #e0e0e0;
  padding: 24px;
  text-align: center;
  color: #666;
  margin-top: auto;
}

/* Utilitários globais */
.btn {
  display: inline-block;
  padding: 8px 16px;
  border: none;
  border-radius: 4px;
  cursor: pointer;
  text-decoration: none;
  font-size: 0.9rem;
  font-weight: 500;
  transition: all 0.2s ease;
}

.btn-primary {
  background: #007bff;
  color: white;
}

.btn-primary:hover {
  background: #0056b3;
}

.btn-secondary {
  background: #6c757d;
  color: white;
}

.btn-secondary:hover {
  background: #545b62;
}

.btn-success {
  background: #28a745;
  color: white;
}

.btn-success:hover {
  background: #218838;
}

.btn-danger {
  background: #dc3545;
  color: white;
}

.btn-danger:hover {
  background: #c82333;
}

/* Responsividade */
@media (max-width: 768px) {
  .header-content {
    padding: 16px;
    flex-direction: column;
    gap: 16px;
  }
  
  .app-nav {
    gap: 16px;
  }
  
  .nav-link {
    padding: 6px 12px;
    font-size: 0.9rem;
  }
}
</style>
