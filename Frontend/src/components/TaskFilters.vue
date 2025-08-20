<template>
  <div class="task-filters">
    <div class="filters-header">
      <h3>Filtros</h3>
      <div class="filters-buttons">
        <button 
          @click="clearFilters" 
          class="btn-clear"
          title="Limpar todos os filtros"
        >
          Limpar
        </button>
        <button 
          @click="getTasks" 
          class="btn-search"
          title="Pesquisar tarefas"
        >
          Pesquisar
        </button>
      </div>
    </div>

    <div class="filters-content">
      <!-- Busca -->
      <div class="filter-group">
        <label for="search">Buscar</label>
        <input
          id="search"
          v-model="localFilters.searchTerm"
          type="text"
          placeholder="Buscar tarefas..."
          class="form-control"
          @input="updateFilters"
        />
      </div>

      <!-- Prioridade -->
      <div class="filter-group">
        <label for="priority">Prioridade</label>
        <select
          id="priority"
          v-model.number="localFilters.priority"
          class="form-control"
          @change="updateFilters"
        >
          <option :value="null">Todas as prioridades</option>
          <option :value="ETaskPriority.NUMBER_0">Baixa</option>
          <option :value="ETaskPriority.NUMBER_1">Normal</option>
          <option :value="ETaskPriority.NUMBER_2">Alta</option>
          <option :value="ETaskPriority.NUMBER_3">Crítica</option>
        </select>
      </div>

      <!-- Categoria -->
      <div class="filter-group">
        <label>Categoria</label>
        <div class="checkbox-group">
          <label>
            <input type="checkbox" :value="ETaskCategory.NUMBER_0" v-model="localFilters.category"/>
            Casa
          </label>
          <label>
            <input type="checkbox" :value="ETaskCategory.NUMBER_1" v-model="localFilters.category" />
            Trabalho
          </label>
          <label>
            <input type="checkbox" :value="ETaskCategory.NUMBER_2" v-model="localFilters.category" />
            Pessoal
          </label>
          <label>
            <input type="checkbox" :value="ETaskCategory.NUMBER_3" v-model="localFilters.category" />
            Compras
          </label>
        </div>
      </div>

      <!-- Status -->
      <div class="filter-group">
        <label for="status">Status</label>
        <select
          id="status"
          v-model="localFilters.isCompleted"
          class="form-control"
          @change="updateFilters"
        >
          <option :value="null">Todas as tarefas</option>
          <option :value="false">Pendente</option>
          <option :value="true">Completa</option>
        </select>
      </div>
    </div>

    <!-- Data -->
    <div class="filters-date">
      <div class="filter-group">
        <label for="min-date">Data mínima</label>
        <input
          id="min-date"
          type="date"
          class="form-control"
          v-model="localFilters.minDueDate"
          @input="updateFilters"
        />
      </div>

      <div class="filter-group">
        <label for="max-date">Data máxima</label>
        <input
          id="max-date"
          type="date"
          class="form-control"
          v-model="localFilters.maxDueDate"
          @input="updateFilters"
        />
      </div>
    </div>

    <!-- Resumo dos filtros -->
    <div class="filters-summary" v-if="hasActiveFilters">
      <span class="summary-text">Filtros ativos:</span>
      <div class="active-filters">
        <span v-if="localFilters.searchTerm" class="filter-tag">
          Busca: "{{ localFilters.searchTerm }}"
        </span>
        <span v-if="localFilters.priority !== undefined" class="filter-tag">
          Prioridade: {{ formatPriority(localFilters.priority) }}
        </span>
        <span v-if="localFilters.category && localFilters.category.length > 0" class="filter-tag">
          Categoria: {{ formatCategory(localFilters.category) }}
        </span>
        <span v-if="localFilters.isCompleted !== null" class="filter-tag">
          Status: {{ localFilters.isCompleted ? 'Completa' : 'Pendente' }}
        </span>
        <span v-if="localFilters.isCompleted === null" class="filter-tag">
          Status: {{ 'Todas as tarefas' }}
        </span>
        <span v-if="localFilters.minDueDate" class="filter-tag">
          Data mínima: {{ localFilters.minDueDate }}
        </span>
        <span v-if="localFilters.maxDueDate" class="filter-tag">
          Data máxima: {{ localFilters.maxDueDate }}
        </span>
      </div>
    </div>
  </div>
</template>

<script setup lang="ts">
import { reactive, computed, watch } from 'vue'
import type { TaskFilters } from '@/types'
import { ETaskPriority, ETaskCategory } from '@/gen/api/src/models'

interface Props {
  filters: TaskFilters
}

interface Emits {
  (e: 'update-filters', filters: TaskFilters): void
  (e: 'getAll-tasks'): void
}

const props = defineProps<Props>()
const emit = defineEmits<Emits>()

const localFilters = reactive<TaskFilters>({
  ...props.filters,
  category: props.filters.category ?? [] as ETaskCategory[]
})

const hasActiveFilters = computed(() => {
  return !!(
    localFilters.searchTerm ||
    localFilters.priority !== undefined ||
    (localFilters.category && localFilters.category.length > 0) ||
    localFilters.isCompleted !== undefined ||
    localFilters.minDueDate !== undefined ||
    localFilters.maxDueDate !== undefined
  )
})

const formatPriority = (priority: number | null): string => {
  if (priority === null) return 'Todas as prioridades'

  const map: Record<number, string> = {
    [ETaskPriority.NUMBER_0]: 'Baixa',
    [ETaskPriority.NUMBER_1]: 'Normal',
    [ETaskPriority.NUMBER_2]: 'Alta',
    [ETaskPriority.NUMBER_3]: 'Crítica'
  }
  return map[priority] ?? 'Todas as prioridades'
}

const formatCategory = (categories: ETaskCategory[] | null): string => {
  
  if (!Array.isArray(categories) || categories.length === 0) return 'Todas as categorias'

  const map: Record<number, string> = {
    [ETaskCategory.NUMBER_0]: 'Casa',
    [ETaskCategory.NUMBER_1]: 'Trabalho',
    [ETaskCategory.NUMBER_2]: 'Pessoal',
    [ETaskCategory.NUMBER_3]: 'Compras'
  }

  return categories.map(c => map[c] ?? '').join(', ')
}

const updateFilters = () => {
  emit('update-filters', { ...localFilters, category: [...localFilters.category!] })
}

const getTasks = () => {
  updateFilters()
  emit('getAll-tasks')
}

const clearFilters = () => {
  localFilters.searchTerm = null
  localFilters.priority = null
  localFilters.category = []
  localFilters.isCompleted = null,
  localFilters.minDueDate = null,
  localFilters.maxDueDate = null
  updateFilters()
}

watch(() => props.filters, (newFilters) => {
  Object.assign(localFilters, newFilters)
}, { deep: true })
</script>


<style scoped>
.task-filters {
  background: white;
  padding: 20px;
  border-radius: 8px;
  box-shadow: 0 2px 4px rgba(0, 0, 0, 0.1);
  margin-bottom: 24px;
}

.filters-header {
  display: flex;
  justify-content: space-between;
  align-items: center;
  margin-bottom: 20px;
}

.filters-buttons {
  display: flex;
  gap: 10px;
  align-items: center;
}

.filters-header h3 {
  margin: 0;
  color: #333;
  font-size: 1.2rem;
}

.btn-clear {
  background: #6c757d;
  color: white;
  border: none;
  padding: 6px 12px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 0.875rem;
  transition: background-color 0.2s ease;
}

.btn-clear:hover {
  background: #545b62;
}

.btn-search {
  background: #007bff;
  color: white;
  border: none;
  padding: 6px 12px;
  border-radius: 4px;
  cursor: pointer;
  font-size: 0.875rem;
  transition: background-color 0.2s ease;
}

.btn-search:hover {
  background: #0056b3;
}

.filters-content {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 16px;
  margin-bottom: 20px;
}

.filter-group {
  display: flex;
  flex-direction: column;
}

.filter-group label {
  margin-bottom: 6px;
  font-weight: 600;
  color: #555;
  font-size: 0.875rem;
}

.form-control {
  padding: 8px 12px;
  border: 1px solid #ddd;
  border-radius: 4px;
  font-size: 0.875rem;
  transition: border-color 0.2s ease;
}

.form-control:focus {
  outline: none;
  border-color: #007bff;
  box-shadow: 0 0 0 2px rgba(0, 123, 255, 0.25);
}

.filters-summary {
  padding-top: 16px;
  border-top: 1px solid #e0e0e0;
}

.filters-date {
  display: grid;
  grid-template-columns: repeat(auto-fit, minmax(200px, 1fr));
  gap: 16px;
  margin-bottom: 20px;
}

.summary-text {
  font-size: 0.875rem;
  color: #666;
  margin-right: 12px;
}

.active-filters {
  display: flex;
  flex-wrap: wrap;
  gap: 8px;
  margin-top: 8px;
}

.filter-tag {
  background: #e3f2fd;
  color: #1976d2;
  padding: 4px 8px;
  border-radius: 12px;
  font-size: 0.75rem;
  font-weight: 500;
}

@media (max-width: 768px) {
  .filters-content {
    grid-template-columns: 1fr;
  }
  
  .filters-header {
    flex-direction: column;
    align-items: flex-start;
    gap: 12px;
  }
  
  .btn-clear {
    align-self: flex-end;
  }
}
</style>
