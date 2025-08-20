<template>
  <div class="task-card" :class="{ 'completed': task.isCompleted }">
    <div class="task-header">
      <div class="task-title">
        <h3>{{ task.title }}</h3>
        <span class="priority-badge" :class="getPriorityClass(task.taskPriority!)">
          {{ formatPriority(task.taskPriority!) }}
        </span>
      </div>
      <div class="task-actions">
        <button 
          @click="toggleCompletion" 
          class="btn-toggle"
          :title="task.isCompleted ? 'Marcar como incompleta' : 'Marcar como completa'"
        >
          {{ task.isCompleted ? '✓' : '○' }}
        </button>
        <button 
          @click="$emit('edit', task.id!)" 
          class="btn-edit"
          title="Editar tarefa"
        >
          ✏️
        </button>
        <button 
          @click="confirmDelete" 
          class="btn-delete"
          title="Excluir tarefa"
        >
          🗑️
        </button>
      </div>
    </div>
    
    <p class="task-description">{{ task.description }}</p>
    
    <div class="task-meta">
      <div class="categories">
        <span 
          v-for="catItem in task.taskCategories" 
          :key="catItem.id || catItem.category" 
          class="category-badge"
        >
          {{ formatCategory(catItem.category!) }}
        </span>
      </div>
      
      <div class="dates">
        <span v-if="task.dueDate" class="due-date">
          Vencimento: {{ formatDate(task.dueDate) }}
        </span>
        <span class="created-date">
          Criada em: {{ formatDate(task.createdAt!) }}
        </span>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { TaskApi } from '@/gen/api/src';
import type { TaskItem } from '@/gen/api/src/models'
import { useAlert } from '@/composables/useAlert'

interface Props {
  task: TaskItem
}

interface Emits {
  (e: 'edit', id: number): void
  (e: 'deleted', id: number): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()
const { showApiResponse } = useAlert()

const formatPriority = (priority: string | number): string => {
  switch (priority) {
    case 0: case '0': return 'Baixa'
    case 1: case '1': return 'Normal'
    case 2: case '2': return 'Alta'
    case 3: case '3': return 'Crítica'
    default: return 'Desconhecida'
  }
}

const getPriorityClass = (priority: string | number): string => {
  switch (priority) {
    case 0: case '0': return 'low'
    case 1: case '1': return 'medium'
    case 2: case '2': return 'high'
    case 3: case '3': return 'urgent'
    default: return ''
  }
}


const formatCategory = (category: string | number): string => {
  switch (category) {
    case 0: case '0': return 'Casa'
    case 1: case '1': return 'Trabalho'
    case 2: case '2': return 'Pessoal'
    case 3: case '3': return 'Compras'
    default: return 'Outros'
  }
}

const formatDate = (date: Date): string => {
  return new Date(date).toLocaleDateString('pt-BR')
}

const toggleCompletion = async () => {
  try {
    const api = new TaskApi()
    const result = await api.updateTaskCompletion({
      taskCompletionInput: {
        id: props.task.id,
        isCompleted: !props.task.isCompleted
      }
    })

    if (result.isSuccess) {
      props.task.isCompleted = !props.task.isCompleted
      showApiResponse(result, result.message)
    } else {
      showApiResponse(result, result.message)
    }
  } catch (error) {
    console.error('Erro ao atualizar status da tarefa:', error)
    showApiResponse({ isSuccess: false, message: 'Erro ao atualizar status da tarefa' })
  }
}

const confirmDelete = async () => {
  if (confirm('Tem certeza que deseja excluir esta tarefa?')) {
    try {
      const api = new TaskApi()
      const result = await api.deleteTask({id: props.task.id!})
      showApiResponse(result, result.message)
      // Emitir evento para atualizar a lista
      if (props.task.id) {
        emit('deleted', props.task.id)
      }
    } catch (error) {
      console.error('Erro ao excluir tarefa:', error)
      showApiResponse({ isSuccess: false, message: 'Erro ao excluir tarefa' })
    }
  }
}
</script>

<style scoped>
.task-card {
  background: white;
  border: 1px solid #e0e0e0;
  border-radius: 8px;
  padding: 16px;
  margin-bottom: 16px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  transition: all 0.2s ease;
}

.task-card:hover {
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
  transform: translateY(-2px);
}

.task-card.completed {
  opacity: 0.7;
  background: #f8f9fa;
}

.task-card.completed .task-title h3 {
  text-decoration: line-through;
  color: #6c757d;
}

.task-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  margin-bottom: 12px;
}

.task-title {
  display: flex;
  align-items: center;
  gap: 12px;
  flex: 1;
}

.task-title h3 {
  margin: 0;
  font-size: 1.1rem;
  color: #333;
}

.priority-badge {
  padding: 4px 8px;
  border-radius: 12px;
  font-size: 0.75rem;
  font-weight: 600;
  text-transform: uppercase;
}

.priority-badge.urgent {
  background: #dc3545;
  color: white;
}

.priority-badge.high {
  background: #fd7e14;
  color: white;
}

.priority-badge.medium {
  background: #ffc107;
  color: #212529;
}

.priority-badge.low {
  background: #28a745;
  color: white;
}

.task-actions {
  display: flex;
  gap: 8px;
}

.task-actions button {
  background: none;
  border: none;
  cursor: pointer;
  padding: 4px;
  border-radius: 4px;
  transition: background-color 0.2s ease;
}

.task-actions button:hover {
  background: #f8f9fa;
}

.btn-toggle {
  font-size: 1.2rem;
  color: #28a745;
}

.btn-edit {
  font-size: 1rem;
}

.btn-delete {
  font-size: 1rem;
}

.task-description {
  color: #666;
  margin: 12px 0;
  line-height: 1.5;
}

.task-meta {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-top: 16px;
  padding-top: 12px;
  border-top: 1px solid #e0e0e0;
}

.categories {
  display: flex;
  gap: 8px;
}

.category-badge {
  background: #e9ecef;
  color: #495057;
  padding: 2px 8px;
  border-radius: 12px;
  font-size: 0.75rem;
}

.dates {
  display: flex;
  flex-direction: column;
  align-items: flex-end;
  gap: 4px;
  font-size: 0.8rem;
  color: #6c757d;
}

@media (max-width: 768px) {
  .task-header {
    flex-direction: column;
    align-items: flex-start;
    gap: 12px;
  }
  
  .task-actions {
    align-self: flex-end;
  }
  
  .task-meta {
    flex-direction: column;
    align-items: flex-start;
    gap: 12px;
  }
  
  .dates {
    align-items: flex-start;
  }
}
</style>