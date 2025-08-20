import { ETaskPriority, ETaskCategory } from "../gen/api/src"

// Enums para prioridade e categoria das tarefas
export enum TaskPriority {
  LOW = 'low',
  MEDIUM = 'medium',
  HIGH = 'high',
  URGENT = 'urgent'
}

export enum TaskCategory {
  WORK = 'work',
  PERSONAL = 'personal',
  SHOPPING = 'shopping',
  HEALTH = 'health',
  EDUCATION = 'education',
  OTHER = 'other'
}

// Interface para uma tarefa
export interface TaskItem {
  id: string
  title: string
  description: string
  taskPriority: ETaskPriority
  taskCategories: ETaskCategory[]
  dueDate: Date | null
  isCompleted: boolean
  createdAt: Date
  updatedAt: Date
}

// Resposta genérica da API
export interface ApiResponse<T> {
  isSuccess: boolean
  message: string
  data: T
}

// Filtros para a lista de tarefas
export interface TaskFilters {
  priority?: ETaskPriority | null
  category?: ETaskCategory[] | null
  isCompleted?: boolean | null
  searchTerm?: string | null,
  minDueDate?: Date | null,
  maxDueDate?: Date | null
}

// Dados para criação/edição de tarefa
export interface TaskFormData {
  title: string
  description: string
  taskPriority: ETaskPriority
  taskCategories: ETaskCategory[]
  dueDate: Date | null
}
