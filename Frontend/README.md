# 📝 TaskMaster - Aplicação de To-Do com Vue 3 + TypeScript

Uma aplicação moderna e funcional de Lista de Tarefas construída com Vue 3, TypeScript e Pinia, demonstrando as melhores práticas de desenvolvimento frontend.

## ✨ Características

- **Vue 3 + Composition API**: Framework moderno com API de composição
- **TypeScript**: Tipagem estática para maior segurança e produtividade
- **Pinia**: Gerenciamento de estado reativo e eficiente
- **Vue Router 4**: Sistema de roteamento moderno
- **Vite**: Ferramenta de build rápida para desenvolvimento
- **Design Responsivo**: Interface adaptável para todos os dispositivos
- **CRUD Completo**: Criar, ler, atualizar e excluir tarefas
- **Filtros Avançados**: Filtrar por prioridade, categoria, status e busca
- **Interface Intuitiva**: UI limpa e fácil de usar

## 🚀 Funcionalidades

### Gerenciamento de Tarefas
- ✅ Criar novas tarefas
- ✏️ Editar tarefas existentes
- 🗑️ Deletar tarefas
- ☑️ Marcar como completa/incompleta

### Organização
- 🎯 **Prioridades**: Low, Medium, High, Urgent
- 🏷️ **Categorias**: Work, Personal, Shopping, Health, Education, Other
- 📅 **Datas de Vencimento**: Definir deadlines para tarefas
- 🔍 **Busca e Filtros**: Encontrar tarefas rapidamente

### Interface
- 📱 **Responsivo**: Funciona perfeitamente em desktop, tablet e mobile
- 🎨 **Design Moderno**: Interface limpa e profissional
- 🔄 **Atualizações em Tempo Real**: Feedback instantâneo das ações
- 📊 **Visualizações**: Lista e grid view para diferentes preferências

## 🛠️ Stack Tecnológica

- **Frontend**: Vue 3.3+
- **Linguagem**: TypeScript 5.1+
- **Estado**: Pinia 2.1+
- **Roteamento**: Vue Router 4.2+
- **Build Tool**: Vite 4.4+
- **Estilos**: CSS3 com design responsivo

## 📁 Estrutura do Projeto

```
src/
├── components/          # Componentes reutilizáveis
│   ├── TaskCard.vue    # Card individual de tarefa
│   ├── TaskForm.vue    # Formulário de criação/edição
│   └── TaskFilters.vue # Interface de filtros
├── pages/              # Páginas da aplicação
│   ├── TaskList.vue    # Lista principal de tarefas
│   └── About.vue       # Página sobre a aplicação
├── stores/             # Gerenciamento de estado (Pinia)
│   └── taskStore.ts    # Store das tarefas
├── types/              # Definições TypeScript
│   └── index.ts        # Interfaces e enums
├── utils/              # Funções utilitárias
│   └── api.ts          # Simulação de API
├── router/             # Configuração de rotas
│   └── index.ts        # Definição das rotas
├── App.vue             # Componente principal
└── main.ts             # Ponto de entrada
```

## 🚀 Instalação e Uso

### Pré-requisitos
- Node.js 16+ 
- npm ou yarn

### 1. Clone o repositório
```bash
git clone <url-do-repositorio>
cd todo-app-vue3
```

### 2. Instale as dependências
```bash
npm install
```

### 3. Execute em modo de desenvolvimento
```bash
npm run dev
```

### 4. Abra no navegador
A aplicação estará disponível em `http://localhost:5173`

## 📜 Scripts Disponíveis

```bash
# Desenvolvimento
npm run dev          # Inicia servidor de desenvolvimento

# Build
npm run build        # Build para produção
npm run preview      # Preview do build de produção

# Qualidade
npm run type-check   # Verificação de tipos TypeScript
```

## 🔧 Configuração

### TypeScript
- Configurado com `tsconfig.json` otimizado para Vue 3
- Path mapping configurado para `@/*` apontando para `src/`
- Verificação de tipos estrita habilitada

### Vite
- Plugin Vue 3 configurado
- Hot Module Replacement (HMR) habilitado
- Build otimizado para produção

### Pinia
- Store centralizado para gerenciamento de estado
- Actions assíncronas para operações de API
- Computed properties para filtros e cálculos

## 📱 Responsividade

A aplicação é totalmente responsiva e inclui:
- **Mobile First**: Design otimizado para dispositivos móveis
- **Breakpoints**: Adaptação para tablet e desktop
- **Touch Friendly**: Interface otimizada para toque
- **Performance**: Carregamento rápido em todos os dispositivos

## 🎨 Personalização

### Cores e Temas
As cores principais podem ser facilmente alteradas editando as variáveis CSS no arquivo `App.vue`:

```css
:root {
  --primary-color: #007bff;
  --secondary-color: #6c757d;
  --success-color: #28a745;
  --danger-color: #dc3545;
}
```

### Componentes
Todos os componentes são modulares e podem ser personalizados:
- Estilos isolados com `scoped` CSS
- Props configuráveis
- Eventos customizáveis
- Slots para conteúdo flexível
