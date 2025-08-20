<template>
  <div class="task-list-page">
    <div class="page-header">
      <div class="header-content">
        <h1>Minhas Tarefas</h1>
        <p style="margin-left: 10px;" class="subtitle">Gerencie e organize suas tarefas diárias</p>
      </div>
      <button 
        @click="showCreateForm = true" 
        class="btn-create"
      >
        + Nova Tarefa
      </button>
    </div>
    
    <TaskFilters 
      :filters="filters"
      @update-filters="updateFilters"
      @get-all-tasks="fetchTasks"
    />

    <div class="tasks-section">
      <div class="section-header">
        <h2>Tarefas ({{ tasks.data?.totalCount }})</h2>
        <div class="view-options">
          <button 
            @click="viewMode = 'list'"
            class="view-btn"
            :class="{ active: viewMode === 'list' }"
            title="Visualização em lista"
          >
            📋
          </button>
          <button 
            @click="viewMode = 'grid'"
            class="view-btn"
            :class="{ active: viewMode === 'grid' }"
            title="Visualização em grade"
          >
            🔲
          </button>
        </div>
      </div>

      <div v-if="loading" class="loading-state">
        <div class="spinner"></div>
        <p>Carregando tarefas...</p>
      </div>

      <div v-else-if="error" class="error-state">
        <p class="error-message">{{ error }}</p>
        <button @click="retryLoad" class="btn-retry">Tentar Novamente</button>
      </div>

      <div v-else-if="tasks.data?.totalCount === 0" class="empty-state">
        <div class="empty-icon">📝</div>
        <h3>Nenhuma tarefa encontrada</h3>
        <p v-if="hasActiveFilters">
          Tente ajustar seus filtros ou 
          <button @click="clearAllFilters" class="btn-link">limpar todos os filtros</button>
        </p>
        <p v-else>
          Comece criando sua primeira tarefa!
        </p>
        <button 
          v-if="!hasActiveFilters"
          @click="showCreateForm = true" 
          class="btn-primary"
        >
          Criar Primeira Tarefa
        </button>
      </div>

      <div v-else class="tasks-container" :class="viewMode">
        <TaskCard
          v-for="task in tasks.data?.items"
          :key="task.id"
          :task="task"
          @edit="startEditing"
          @deleted="handleTaskDeleted"
        />
      </div>

      <!-- PAGINAÇÃO -->
      <div v-if="tasks.data?.totalCount! > 0 && totalPages > 0" class="pagination">
        <button 
          :disabled="currentPage === 1" 
          @click="fetchTasks(currentPage - 1)"
        >
          ← Anterior
        </button>

        <span>Página {{ currentPage }} de {{ totalPages }}</span>

        <button 
          :disabled="currentPage === totalPages" 
          @click="fetchTasks(currentPage + 1)"
        >
          Próxima →
        </button>
      </div>
    </div>

    <!-- Modal para criação/edição -->
    <div v-if="showCreateForm || editingTask" class="modal-overlay" @click="closeModal">
      <div class="modal-content" @click.stop>
        <TaskForm
          :task="editingTask"
          @saved="handleTaskSaved"
          @cancel="closeModal"
        />
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { ref, computed, onMounted } from 'vue'
import type { ETaskCategory, TaskItem } from '@/gen/api/src/models'
import type { TaskFilters as TaskFiltersType } from '@/types'
import TaskCard from '@/components/TaskCard.vue'
import TaskForm from '@/components/TaskForm.vue'
import TaskFilters from '@/components/TaskFilters.vue'
import { TaskApi, TaskItemPagedResultToDoAppResponse } from '@/gen/api/src'
import { useAlert } from '@/composables/useAlert'

const loading = ref(false)
const error = ref<string | null>(null)
const showCreateForm = ref(false)
const editingTask = ref<TaskItem | null>(null)
const viewMode = ref<'list' | 'grid'>('list')
const tasks = ref<TaskItemPagedResultToDoAppResponse>({data: {items: [], totalCount: 0, pageNumber: 0, pageSize: 0}})
  const currentPage = ref(1)
const pageSize = ref(10)

const totalPages = computed(() => {
  return Math.ceil((tasks.value.data?.totalCount ?? 0) / pageSize.value)
})


const hasActiveFilters = computed(() => {
  const filters = {
    searchTerm: '',
    priority: null,
    category: [],
    isCompleted: null
  }
  return !!(
    filters.searchTerm ||
    filters.priority !== null ||
    (filters.category && filters.category.length > 0) ||
    filters.isCompleted !== null
  )
})

const filters = ref<TaskFiltersType>({
  priority: null,
  category: [] as ETaskCategory[],
  isCompleted: null,
  searchTerm: null
})

const updateFilters = (newFilters: any) => {
  filters.value = { ...filters.value, ...newFilters }
}

const clearAllFilters = () => {
  filters.value = { 
    priority: null,
    category: [],
    isCompleted: null,
    searchTerm: null
  }
}

const startEditing = (id: number) => {
  const task = tasks.value.data?.items!.find(t => t.id === id)
  if (!task) return
  editingTask.value = task
  showCreateForm.value = true
}

const handleTaskSaved = () => {
  showCreateForm.value = false
  editingTask.value = null
  fetchTasks()
}

const closeModal = () => {
  showCreateForm.value = false
  editingTask.value = null
}

const retryLoad = () => {
  fetchTasks()
}

const { showApiResponse } = useAlert()

const fetchTasks = async (page: number = currentPage.value) => {
  loading.value = true
  error.value = null
  try {
    const api = new TaskApi()
    
    const result = await api.getAllTasks({
      taskItemListInput: {
        pageNumber: page,
        pageSize: pageSize.value,
        taskPriority: filters.value.priority ?? undefined,
        taskCategory: filters.value.category && filters.value.category.length > 0 ? [...filters.value.category] : undefined,
        isCompleted: filters.value.isCompleted ?? undefined,
        title: filters.value.searchTerm ?? undefined,
        minDueDate: filters.value.minDueDate ? new Date(filters.value.minDueDate) : undefined,
        maxDueDate: filters.value.maxDueDate ? new Date(filters.value.maxDueDate) : undefined
      }
    })

    if (result?.isSuccess) {
      tasks.value = result
      currentPage.value = result.data?.pageNumber ?? 1
      pageSize.value = result.data?.pageSize ?? 10
    } else {
      showApiResponse(result, result.message)
    }
  } catch (err: any) {
    error.value = "Erro ao carregar tarefas"
    console.error(err)
    showApiResponse({ isSuccess: false, message: 'Erro ao carregar tarefas' })
  } finally {
    loading.value = false
  }
}

const handleTaskDeleted = (taskId: number) => {
  // Remover a tarefa da lista local
  if (tasks.value?.data?.items) {
    tasks.value.data.items = tasks.value.data.items.filter((task: TaskItem) => task.id !== taskId)
  }
}

onMounted(() => {
  fetchTasks()
})
</script>

<style scoped>
.task-list-page {
  max-width: 1200px;
  margin: 0 auto;
  padding: 24px;
}

.page-header {
  display: flex;
  justify-content: space-between;
  align-items: flex-start;
  padding-bottom: 24px;
  border-bottom: 2px solid #e0e0e0;
}

.header-content h1 {
  margin: 0 0 8px 0;
  color: #333;
  font-size: 2.5rem;
  font-weight: 700;
}

.subtitle {
  margin: 0;
  color: #666;
  font-size: 1.1rem;
}

.btn-create {
  background: #28a745;
  color: white;
  border: none;
  padding: 12px 24px;
  border-radius: 8px;
  font-size: 1rem;
  font-weight: 600;
  cursor: pointer;
  transition: all 0.2s ease;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
}

.btn-create:hover {
  background: #218838;
  transform: translateY(-2px);
  box-shadow: 0 4px 8px rgba(0, 0, 0, 0.15);
}

.tasks-section {
  margin-top: 32px;
}

.section-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 24px;
}

.section-header h2 {
  margin: 0;
  color: #333;
  font-size: 1.5rem;
}

.view-options {
  display: flex;
  gap: 8px;
}

.view-btn {
  background: none;
  border: 1px solid #ddd;
  padding: 8px;
  border-radius: 6px;
  cursor: pointer;
  font-size: 1.2rem;
  transition: all 0.2s ease;
}

.view-btn:hover {
  background: #f8f9fa;
  border-color: #007bff;
}

.view-btn.active {
  background: #007bff;
  color: white;
  border-color: #007bff;
}

.tasks-container {
  display: flex;
  flex-direction: column;
  gap: 16px;
}

.tasks-container.grid {
  display: grid;
  grid-template-columns: repeat(auto-fill, minmax(350px, 1fr));
  gap: 20px;
}

.loading-state,
.error-state,
.empty-state {
  text-align: center;
  padding: 60px 20px;
  color: #666;
}

.spinner {
  width: 40px;
  height: 40px;
  border: 4px solid #f3f3f3;
  border-top: 4px solid #007bff;
  border-radius: 50%;
  animation: spin 1s linear infinite;
  margin: 0 auto 20px;
}

@keyframes spin {
  0% { transform: rotate(0deg); }
  100% { transform: rotate(360deg); }
}

.error-message {
  color: #dc3545;
  margin-bottom: 20px;
}

.btn-retry {
  background: #007bff;
  color: white;
  border: none;
  padding: 10px 20px;
  border-radius: 6px;
  cursor: pointer;
  font-size: 0.9rem;
}

.empty-icon {
  font-size: 4rem;
  margin-bottom: 20px;
}

.empty-state h3 {
  margin: 0 0 12px 0;
  color: #333;
}

.btn-link {
  background: none;
  border: none;
  color: #007bff;
  text-decoration: underline;
  cursor: pointer;
  padding: 0;
  font-size: inherit;
}

.btn-primary {
  background: #007bff;
  color: white;
  border: none;
  padding: 12px 24px;
  border-radius: 6px;
  cursor: pointer;
  font-size: 1rem;
  font-weight: 600;
  margin-top: 16px;
}

/* Modal */
.modal-overlay {
  position: fixed;
  top: 0;
  left: 0;
  right: 0;
  bottom: 0;
  background: rgba(0, 0, 0, 0.5);
  display: flex;
  align-items: center;
  justify-content: center;
  z-index: 1000;
  padding: 20px;
}

.modal-content {
  background: white;
  border-radius: 8px;
  max-width: 600px;
  width: 100%;
  max-height: 90vh;
  overflow-y: auto;
}

.pagination {
  display: flex;
  align-items: center;
  justify-content: center;
  gap: 12px;
  margin: 20px 0;
  font-family: Arial, sans-serif;
}

.pagination button {
  padding: 6px 12px;
  border: 1px solid #007bff;
  background-color: #fff;
  color: #007bff;
  border-radius: 4px;
  cursor: pointer;
  transition: all 0.2s ease-in-out;
}

.pagination button:hover:not(:disabled) {
  background-color: #007bff;
  color: #fff;
}

.pagination button:disabled {
  opacity: 0.5;
  cursor: not-allowed;
}

.pagination span {
  font-weight: 500;
  font-size: 14px;
  color: #333;
}


@media (max-width: 768px) {
  .task-list-page {
    padding: 16px;
  }
  
  .page-header {
    flex-direction: column;
    gap: 20px;
    align-items: stretch;
  }
  
  .header-content h1 {
    font-size: 2rem;
  }
  
  .btn-create {
    align-self: stretch;
  }
  
  .section-header {
    flex-direction: column;
    align-items: flex-start;
    gap: 16px;
  }
  
  .tasks-container.grid {
    grid-template-columns: 1fr;
  }
  
  .modal-content {
    margin: 20px;
    max-height: calc(100vh - 40px);
  }
}
</style>
