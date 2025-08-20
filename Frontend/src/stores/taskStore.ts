import { defineStore } from 'pinia'
import { ref, computed } from 'vue'
import type { TaskItem, TaskFilters, TaskFormData } from '@/types'
import { TaskPriority } from '@/types'
import * as api from '@/utils/api'

export const useTaskStore = defineStore('tasks', () => {
    // Estado
    const tasks = ref<TaskItem[]>([])
    const loading = ref(false)
    const error = ref<string | null>(null)
    const filters = ref<TaskFilters>({})

    // Getters
    const filteredTasks = computed(() => {
        let filtered = [...tasks.value]

        // Filtro por prioridade
        if (filters.value.priority) {
            filtered = filtered.filter(task => task.taskPriority === filters.value.priority)
        }

        // Filtro por categoria
        if (filters.value.category) {
            filtered = filtered.filter(task =>
                task.taskCategories.includes(filters.value.category!)
            )
        }

        // Filtro por status de conclusão
        if (filters.value.isCompleted !== undefined) {
            filtered = filtered.filter(task => task.isCompleted === filters.value.isCompleted)
        }

        // Filtro por termo de busca
        if (filters.value.searchTerm) {
            const searchTerm = filters.value.searchTerm.toLowerCase()
            filtered = filtered.filter(task =>
                task.title.toLowerCase().includes(searchTerm) ||
                task.description.toLowerCase().includes(searchTerm)
            )
        }

        return filtered
    })

    const completedTasks = computed(() =>
        tasks.value.filter(task => task.isCompleted)
    )

    const pendingTasks = computed(() =>
        tasks.value.filter(task => !task.isCompleted)
    )

    const tasksByPriority = computed(() => {
        const grouped = {
            urgent: tasks.value.filter(task => task.taskPriority === TaskPriority.URGENT),
            high: tasks.value.filter(task => task.taskPriority === TaskPriority.HIGH),
            medium: tasks.value.filter(task => task.taskPriority === TaskPriority.MEDIUM),
            low: tasks.value.filter(task => task.taskPriority === TaskPriority.LOW)
        }
        return grouped
    })

    // Actions
    const setFilters = (newFilters: TaskFilters) => {
        filters.value = { ...filters.value, ...newFilters }
    }

    const clearFilters = () => {
        filters.value = {}
    }

    const fetchTasks = async () => {
        loading.value = true
        error.value = null

        try {
            const response = await api.fetchTasks()
            if (response.isSuccess) {
                tasks.value = response.data
            } else {
                error.value = response.message
            }
        } catch (err) {
            error.value = 'Failed to fetch tasks'
            console.error('Error fetching tasks:', err)
        } finally {
            loading.value = false
        }
    }

    const fetchTaskById = async (id: string) => {
        loading.value = true
        error.value = null

        try {
            const response = await api.fetchTaskById(id)
            if (response.isSuccess && response.data) {
                // Atualiza a tarefa na lista se ela existir
                const index = tasks.value.findIndex(t => t.id === id)
                if (index !== -1) {
                    tasks.value[index] = response.data
                }
                return response.data
            } else {
                error.value = response.message
                return null
            }
        } catch (err) {
            error.value = 'Failed to fetch task'
            console.error('Error fetching task:', err)
            return null
        } finally {
            loading.value = false
        }
    }

    const createTask = async (taskData: TaskFormData) => {
        loading.value = true
        error.value = null

        try {
            const response = await api.createTask(taskData)
            if (response.isSuccess) {
                tasks.value.push(response.data)
                return response.data
            } else {
                error.value = response.message
                return null
            }
        } catch (err) {
            error.value = 'Failed to create task'
            console.error('Error creating task:', err)
            return null
        } finally {
            loading.value = false
        }
    }

    const updateTask = async (id: string, taskData: Partial<TaskFormData>) => {
        loading.value = true
        error.value = null

        try {
            const response = await api.updateTask(id, taskData)
            if (response.isSuccess && response.data) {
                const index = tasks.value.findIndex(t => t.id === id)
                if (index !== -1) {
                    tasks.value[index] = response.data
                }
                return response.data
            } else {
                error.value = response.message
                return null
            }
        } catch (err) {
            error.value = 'Failed to update task'
            console.error('Error updating task:', err)
            return null
        } finally {
            loading.value = false
        }
    }

    const deleteTask = async (id: string) => {
        loading.value = true
        error.value = null

        try {
            const response = await api.deleteTask(id)
            if (response.isSuccess) {
                const index = tasks.value.findIndex(t => t.id === id)
                if (index !== -1) {
                    tasks.value.splice(index, 1)
                }
                return true
            } else {
                error.value = response.message
                return false
            }
        } catch (err) {
            error.value = 'Failed to delete task'
            console.error('Error deleting task:', err)
            return false
        } finally {
            loading.value = false
        }
    }

    const toggleTaskCompletion = async (id: string) => {
        try {
            const response = await api.toggleTaskCompletion(id)
            if (response.isSuccess && response.data) {
                const index = tasks.value.findIndex(t => t.id === id)
                if (index !== -1) {
                    tasks.value[index] = response.data
                }
                return response.data
            } else {
                error.value = response.message
                return null
            }
        } catch (err) {
            error.value = 'Failed to toggle task completion'
            console.error('Error toggling task completion:', err)
            return null
        }
    }

    const clearError = () => {
        error.value = null
    }

    return {
        // Estado
        tasks,
        loading,
        error,
        filters,

        // Getters
        filteredTasks,
        completedTasks,
        pendingTasks,
        tasksByPriority,

        // Actions
        setFilters,
        clearFilters,
        fetchTasks,
        fetchTaskById,
        createTask,
        updateTask,
        deleteTask,
        toggleTaskCompletion,
        clearError
    }
})
