<template>
  <div class="task-form">
    <h2>{{ isEditing ? 'Editar Tarefa' : 'Criar Nova Tarefa' }}</h2>
    
    <form @submit.prevent="handleSubmit">
      <div class="form-group">
        <label for="title">Título *</label>
        <input
          id="title"
          v-model="formData.title"
          type="text"
          required
          placeholder="Digite o título da tarefa"
          class="form-control"
        />
      </div>
      
      <div class="form-group">
        <label for="description">Descrição</label>
        <textarea
          id="description"
          v-model="formData.description"
          rows="3"
          placeholder="Digite a descrição da tarefa"
          class="form-control"
        ></textarea>
      </div>
      
      <div class="form-row">
        <div class="form-group">
          <label for="priority">Prioridade *</label>
          <select
            id="priority"
            v-model="formData.taskPriority"
            required
            class="form-control"
          >
            <option value="">Selecione a prioridade</option>
            <option v-for="priority in availablePriorities" :key="priority.value" :value="priority.value">
              {{ priority.label }}
            </option>
          </select>
        </div>
        
        <div class="form-group">
          <label for="dueDate">Data de Vencimento</label>
          <input
            id="dueDate"
            v-model="formData.dueDate"
            type="date"
            class="form-control"
          />
        </div>
      </div>
      
              <div class="form-group">
          <label>Categorias</label>
          <div class="categories-grid">
            <label 
              v-for="category in availableCategories" 
              :key="category.value"
              class="category-checkbox"
            >
              <input
                type="checkbox"
                :value="category.value"
                v-model="formData.taskCategories"
                style="margin-right: 10px;"
              />
              <span>{{ category.label }}</span>
            </label>
          </div>
        </div>
      
      <div class="form-actions">
        <button 
          type="button" 
          @click="$emit('cancel')"
          class="btn btn-secondary"
        >
          Cancelar
        </button>
        <button 
          type="submit" 
          class="btn btn-primary"
          :disabled="loading"
        >
          {{ loading ? 'Salvando...' : (isEditing ? 'Atualizar Tarefa' : 'Criar Tarefa') }}
        </button>
      </div>
    </form>
  </div>
</template>

<script setup lang="ts">
import { ref, reactive, computed, onMounted } from 'vue'
import type { TaskItem, BooleanToDoAppResponse } from '@/gen/api/src/models'
import type { TaskFormData } from '@/types'
import { ETaskPriority, ETaskCategory } from '@/gen/api/src/models'
import { TaskApi } from '@/gen/api/src'
import { useAlert } from '@/composables/useAlert'

interface Props {
  task?: TaskItem | null
}

interface Emits {
  (e: 'saved', task: BooleanToDoAppResponse): void
  (e: 'cancel'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const loading = ref(false)

const isEditing = computed(() => !!props.task)

const formData = reactive<TaskFormData>({
  title: '',
  description: '',
  taskPriority: ETaskPriority.NUMBER_1,
  taskCategories: [],
  dueDate: null
})

const availableCategories = [
  { value: ETaskCategory.NUMBER_0, label: 'Casa' },
  { value: ETaskCategory.NUMBER_1, label: 'Trabalho' },
  { value: ETaskCategory.NUMBER_2, label: 'Pessoal' },
  { value: ETaskCategory.NUMBER_3, label: 'Compras' }
]

const availablePriorities = [
  { value: ETaskPriority.NUMBER_0, label: 'Baixa' },
  { value: ETaskPriority.NUMBER_1, label: 'Normal' },
  { value: ETaskPriority.NUMBER_2, label: 'Alta' },
  { value: ETaskPriority.NUMBER_3, label: 'Crítica' }
]

const resetForm = () => {
  formData.title = ''
  formData.description = ''
  formData.taskPriority = ETaskPriority.NUMBER_1
  formData.taskCategories = []
  formData.dueDate = null
}

const loadTaskData = () => {
  if (props.task) {
    formData.title = props.task.title!
    formData.description = props.task.description!
    formData.taskPriority = props.task.taskPriority!
    formData.taskCategories = props.task.taskCategories!.map(tc => tc.category).filter((c): c is ETaskCategory => c !== undefined);
    formData.dueDate = props.task.dueDate ? new Date(props.task.dueDate) : null
  }
}

const { showApiResponse } = useAlert()

const handleSubmit = async () => {
  loading.value = true
  
  try {
    let result: BooleanToDoAppResponse | null = null
    let api = new TaskApi()
    
    if (isEditing.value && props.task) {
      result = await api.updateTask({
        taskItemUpdateInput: {
          id: Number(props.task.id),
          title: formData.title,
          description: formData.description,
          taskPriority: formData.taskPriority,
          taskCategories: formData.taskCategories,
          dueDate: formData.dueDate ? new Date(formData.dueDate) : undefined
        }
      })
    } else {
      result = await api.createTask({
        taskItemInput: {
          title: formData.title,
          description: formData.description,
          taskPriority: formData.taskPriority,
          taskCategories: formData.taskCategories,
          dueDate: formData.dueDate ? new Date(formData.dueDate) : undefined
        }
      })
    }
    
    if (result) {
      if (result.isSuccess) {
        showApiResponse(result, result.message)
        emit('saved', result)
        resetForm()
      } else {
        showApiResponse(result, result.message)
      }
    }
  } catch (error) {
    console.error('Error saving task:', error)
    showApiResponse({ isSuccess: false, message: 'Erro ao salvar tarefa' })
  } finally {
    loading.value = false
  }
}

onMounted(() => {
  if (props.task) {
    loadTaskData()
  }
})
</script>

<style scoped>
.task-form {
  background: white;
  padding: 24px;
  border-radius: 8px;
  box-shadow: 0 2px 8px rgba(0, 0, 0, 0.1);
}

.task-form h2 {
  margin: 0 0 24px 0;
  color: #333;
  font-size: 1.5rem;
}

.form-group {
  margin-bottom: 20px;
}

.form-row {
  display: grid;
  grid-template-columns: 1fr 1fr;
  gap: 20px;
}

.form-group label {
  display: block;
  margin-bottom: 8px;
  font-weight: 600;
  color: #333;
}

.form-control {
  width: 100%;
  padding: 12px;
  border: 1px solid #ddd;
  border-radius: 6px;
  font-size: 1rem;
  transition: border-color 0.2s ease;
}

.form-control:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.25);
}

textarea.form-control {
  resize: vertical;
  min-height: 80px;
}

.categories-grid {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(120px, 1fr));
  gap: 12px;
  margin-top: 8px;
}

.category-checkbox {
  display: flex;
  align-items: center;
  gap: 8px;
  cursor: pointer;
  padding: 8px;
  border: 1px solid #e0e0e0;
  border-radius: 6px;
  transition: all 0.2s ease;
}

.category-checkbox:hover {
  background: #f8f9fa;
  border-color: #007bff;
}

.category-checkbox input[type="checkbox"] {
  margin: 0;
}

.form-actions {
  display: flex;
  gap: 16px;
  justify-content: flex-end;
  margin-top: 32px;
  padding-top: 24px;
  border-top: 1px solid #e0e0e0;
}

.btn {
  padding: 12px 24px;
  border: none;
  border-radius: 6px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
}

.btn:disabled {
  opacity: 0.6;
  cursor: not-allowed;
}

.btn-primary {
  background: #007bff;
  color: white;
}

.btn-primary:hover:not(:disabled) {
  background: #0056b3;
}

.btn-secondary {
  background: #6c757d;
  color: white;
}

.btn-secondary:hover {
  background: #545b62;
}

@media (max-width: 768px) {
  .form-row {
    grid-template-columns: 1fr;
  }
  
  .categories-grid {
    grid-template-columns: repeat(auto-fit, minmax(100px, 1fr));
  }
  
  .form-actions {
    flex-direction: column;
  }
  
  .btn {
    width: 100%;
  }
}
</style>
