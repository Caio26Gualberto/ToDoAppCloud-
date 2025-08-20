import type { ApiResponse, TaskItem, TaskFormData } from '@/types'
import { TaskPriority, TaskCategory } from '@/types'

// Simulação de dados iniciais
const mockTasks: TaskItem[] = [
  {
    id: '1',
    title: 'Implementar login',
    description: 'Criar sistema de autenticação com JWT',
    taskPriority: TaskPriority.HIGH,
    taskCategories: [TaskCategory.WORK],
    dueDate: new Date('2024-01-15'),
    isCompleted: false,
    createdAt: new Date('2024-01-01'),
    updatedAt: new Date('2024-01-01')
  },
  {
    id: '2',
    title: 'Fazer exercícios',
    description: 'Treino de cardio por 30 minutos',
    taskPriority: TaskPriority.MEDIUM,
    taskCategories: [TaskCategory.HEALTH, TaskCategory.PERSONAL],
    dueDate: new Date('2024-01-10'),
    isCompleted: true,
    createdAt: new Date('2024-01-01'),
    updatedAt: new Date('2024-01-05')
  }
]

// Simulação de delay da API
const delay = (ms: number) => new Promise(resolve => setTimeout(resolve, ms))

// Função para simular resposta da API
const createApiResponse = <T>(data: T, isSuccess = true, message = 'Success'): ApiResponse<T> => ({
  isSuccess,
  message,
  data
})

// API para buscar todas as tarefas
export const fetchTasks = async (): Promise<ApiResponse<TaskItem[]>> => {
  await delay(500) // Simula delay da rede
  return createApiResponse([...mockTasks])
}

// API para buscar tarefa por ID
export const fetchTaskById = async (id: string): Promise<ApiResponse<TaskItem | null>> => {
  await delay(300)
  const task = mockTasks.find(t => t.id === id)
  
  if (!task) {
    return createApiResponse(null, false, 'Task not found')
  }
  
  return createApiResponse(task)
}

// API para criar nova tarefa
export const createTask = async (taskData: TaskFormData): Promise<ApiResponse<TaskItem>> => {
  await delay(400)
  
  const newTask: TaskItem = {
    id: Date.now().toString(),
    ...taskData,
    isCompleted: false,
    createdAt: new Date(),
    updatedAt: new Date()
  }
  
  mockTasks.push(newTask)
  return createApiResponse(newTask, true, 'Task created successfully')
}

// API para atualizar tarefa
export const updateTask = async (id: string, taskData: Partial<TaskFormData>): Promise<ApiResponse<TaskItem | null>> => {
  await delay(400)
  
  const taskIndex = mockTasks.findIndex(t => t.id === id)
  
  if (taskIndex === -1) {
    return createApiResponse(null, false, 'Task not found')
  }
  
  const updatedTask = {
    ...mockTasks[taskIndex],
    ...taskData,
    updatedAt: new Date()
  }
  
  mockTasks[taskIndex] = updatedTask
  return createApiResponse(updatedTask, true, 'Task updated successfully')
}

// API para deletar tarefa
export const deleteTask = async (id: string): Promise<ApiResponse<boolean>> => {
  await delay(300)
  
  const taskIndex = mockTasks.findIndex(t => t.id === id)
  
  if (taskIndex === -1) {
    return createApiResponse(false, false, 'Task not found')
  }
  
  mockTasks.splice(taskIndex, 1)
  return createApiResponse(true, true, 'Task deleted successfully')
}

// API para marcar tarefa como completa/incompleta
export const toggleTaskCompletion = async (id: string): Promise<ApiResponse<TaskItem | null>> => {
  await delay(300)
  
  const task = mockTasks.find(t => t.id === id)
  
  if (!task) {
    return createApiResponse(null, false, 'Task not found')
  }
  
  task.isCompleted = !task.isCompleted
  task.updatedAt = new Date()
  
  return createApiResponse(task, true, 'Task status updated successfully')
}
