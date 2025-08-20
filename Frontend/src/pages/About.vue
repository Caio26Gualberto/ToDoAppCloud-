<template>
  <div class="about-page">
    <div class="about-content">
      <h1>Sobre o TaskMaster</h1>
      
      <div class="about-section">
        <h2>🚀 O que é o TaskMaster?</h2>
        <p>
          TaskMaster é uma aplicação moderna e rica em recursos de Lista de Tarefas construída com Vue 3 e TypeScript. 
          Ela fornece uma interface intuitiva para gerenciar suas tarefas diárias, projetos e objetivos com 
          capacidades poderosas de filtragem e organização.
        </p>
      </div>

      <div class="about-section">
        <h2>✨ Funcionalidades</h2>
        <ul class="features-list">
          <li><strong>Gerenciamento de Tarefas:</strong> Criar, editar, excluir e marcar tarefas como concluídas</li>
          <li><strong>Níveis de Prioridade:</strong> Definir prioridade da tarefa (Baixa, Média, Alta, Urgente)</li>
          <li><strong>Categorias:</strong> Organizar tarefas por trabalho, pessoal, compras, saúde, educação ou outro</li>
          <li><strong>Datas de Vencimento:</strong> Definir prazos para suas tarefas</li>
          <li><strong>Filtragem Avançada:</strong> Filtrar por prioridade, categoria, status e termos de busca</li>
          <li><strong>Design Responsivo:</strong> Funciona perfeitamente em desktop, tablet e dispositivos móveis</li>
          <li><strong>Atualizações em Tempo Real:</strong> Feedback instantâneo e gerenciamento de estado</li>
        </ul>
      </div>

      <div class="about-section">
        <h2>🛠️ Stack Tecnológica</h2>
        <div class="tech-stack">
          <div class="tech-item">
            <h3>Framework Frontend</h3>
            <p>Vue 3 com Composition API</p>
          </div>
          <div class="tech-item">
            <h3>Linguagem</h3>
            <p>TypeScript para segurança de tipos</p>
          </div>
          <div class="tech-item">
            <h3>Gerenciamento de Estado</h3>
            <p>Pinia para estado reativo</p>
          </div>
          <div class="tech-item">
            <h3>Ferramenta de Build</h3>
            <p>Vite para desenvolvimento rápido</p>
          </div>
          <div class="tech-item">
            <h3>Roteamento</h3>
            <p>Vue Router 4</p>
          </div>
          <div class="tech-item">
            <h3>Estilização</h3>
            <p>CSS com design responsivo</p>
          </div>
        </div>
      </div>

      <div class="about-section">
                <h2>📁 Estrutura do Projeto</h2>
        <div class="structure-tree">
          <pre><code>src/
 ├── components/          # Componentes reutilizáveis
 │   ├── TaskCard.vue    # Exibição individual de tarefa
 │   ├── TaskForm.vue    # Formulário de criação/edição de tarefa
 │   └── TaskFilters.vue # Interface de filtragem de tarefas
 ├── pages/              # Páginas da aplicação
 │   ├── TaskList.vue    # Página principal da lista de tarefas
 │   └── About.vue       # Página sobre
 ├── stores/             # Gerenciamento de estado Pinia
 │   └── taskStore.ts    # Estado e ações das tarefas
 ├── types/              # Definições de tipos TypeScript
 │   └── index.ts        # Interfaces e enums de tarefas
 ├── utils/              # Funções utilitárias
 │   └── api.ts          # Funções de simulação da API
 └── App.vue             # Componente principal da aplicação</code></pre>
        </div>
      </div>

      <div class="about-section">
        <h2>🚀 Como Começar</h2>
        <div class="getting-started">
          <h3>Instalação</h3>
          <pre><code>npm install</code></pre>
          
          <h3>Desenvolvimento</h3>
          <pre><code>npm run dev</code></pre>
          
          <h3>Build para Produção</h3>
          <pre><code>npm run build</code></pre>
          
          <h3>Verificação de Tipos</h3>
          <pre><code>npm run type-check</code></pre>
        </div>
      </div>

      <div class="about-section">
        <h2>🔧 Personalização</h2>
        <p>
          A aplicação é projetada para ser facilmente personalizável. Você pode modificar cores, layouts, 
          e funcionalidades editando as variáveis CSS e a lógica dos componentes. A estrutura modular 
          torna simples adicionar novas funcionalidades ou modificar as existentes.
        </p>
      </div>

      <div class="about-section">
        <h2>📱 Design Responsivo</h2>
        <p>
          TaskMaster é construído com princípios de design responsivo mobile-first. A interface 
          se adapta automaticamente a diferentes tamanhos de tela, garantindo uma excelente experiência 
          do usuário em todos os dispositivos.
        </p>
      </div>

      <div class="about-section">
        <h2>🤝 Contribuição</h2>
        <p>
          Este é um projeto de demonstração que mostra práticas modernas de desenvolvimento Vue 3 + TypeScript. 
          Sinta-se livre para usá-lo como ponto de partida para suas próprias aplicações ou como recurso de aprendizado.
        </p>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
// Página About com informações sobre a aplicação
</script>

<style scoped>
.about-page {
  max-width: 800px;
  margin: 0 auto;
  padding: 40px 24px;
}

.about-content h1 {
  text-align: center;
  color: #333;
  font-size: 2.5rem;
  margin-bottom: 40px;
  font-weight: 700;
}

.about-section {
  margin-bottom: 40px;
  padding: 24px;
  background: white;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.about-section h2 {
  color: #007bff;
  margin-bottom: 16px;
  font-size: 1.5rem;
  display: flex;
  align-items: center;
  gap: 8px;
}

.about-section h3 {
  color: #333;
  margin: 16px 0 8px 0;
  font-size: 1.1rem;
}

.about-section p {
  color: #666;
  line-height: 1.6;
  margin-bottom: 16px;
}

.features-list {
  list-style: none;
  padding: 0;
}

.features-list li {
  padding: 8px 0;
  border-bottom: 1px solid #f0f0f0;
  color: #666;
}

.features-list li:last-child {
  border-bottom: none;
}

.features-list strong {
  color: #333;
}

.tech-stack {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 20px;
}

.tech-item {
  padding: 16px;
  background: #f8f9fa;
  border-radius: 6px;
  border-left: 4px solid #007bff;
}

.tech-item h3 {
  margin: 0 0 8px 0;
  color: #333;
  font-size: 1rem;
}

.tech-item p {
  margin: 0;
  color: #666;
  font-size: 0.9rem;
}

.structure-tree {
  background: #f8f9fa;
  border-radius: 6px;
  padding: 20px;
  overflow-x: auto;
}

.structure-tree pre {
  margin: 0;
  font-family: 'Monaco', 'Menlo', 'Ubuntu Mono', monospace;
  font-size: 0.85rem;
  line-height: 1.4;
  color: #333;
}

.getting-started {
  background: #f8f9fa;
  border-radius: 6px;
  padding: 20px;
}

.getting-started pre {
  background: #2d3748;
  color: #e2e8f0;
  padding: 12px;
  border-radius: 4px;
  margin: 8px 0 16px 0;
  overflow-x: auto;
}

.getting-started code {
  font-family: 'Monaco', 'Menlo', 'Ubuntu Mono', monospace;
  font-size: 0.9rem;
}

@media (max-width: 768px) {
  .about-page {
    padding: 24px 16px;
  }
  
  .about-content h1 {
    font-size: 2rem;
  }
  
  .about-section {
    padding: 20px;
    margin-bottom: 24px;
  }
  
  .tech-stack {
    grid-template-columns: 1fr;
  }
  
  .structure-tree,
  .getting-started {
    padding: 16px;
  }
  
  .structure-tree pre,
  .getting-started pre {
    font-size: 0.75rem;
  }
}
</style>
