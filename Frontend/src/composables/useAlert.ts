import { ref } from 'vue'

export interface Alert {
    id: string
    type: 'success' | 'error' | 'warning' | 'info'
    message: string
    duration?: number
}

const alerts = ref<Alert[]>([])

export const useAlert = () => {
    const addAlert = (alert: Omit<Alert, 'id'>) => {
        const id = Date.now().toString()
        const newAlert: Alert = {
            id,
            type: alert.type,
            message: alert.message,
            duration: alert.duration || 5000
        }

        alerts.value.push(newAlert)

        if (newAlert.duration && newAlert.duration > 0) {
            setTimeout(() => {
                removeAlert(id)
            }, newAlert.duration)
        }
    }

    const removeAlert = (id: string) => {
        const index = alerts.value.findIndex(alert => alert.id === id)
        if (index > -1) {
            alerts.value.splice(index, 1)
        }
    }

    const clearAll = () => {
        alerts.value = []
    }

    const success = (message: string, duration?: number) => {
        addAlert({ type: 'success', message, duration })
    }

    const error = (message: string, duration?: number) => {
        addAlert({ type: 'error', message, duration })
    }

    const warning = (message: string, duration?: number) => {
        addAlert({ type: 'warning', message, duration })
    }

    const info = (message: string, duration?: number) => {
        addAlert({ type: 'info', message, duration })
    }

    // Função para mostrar alertas baseados na resposta da API
    const showApiResponse = (response: any, successMessage?: string) => {
        if (response?.isSuccess) {
            const message = successMessage || response.message || 'Operação realizada com sucesso!'
            success(message)
        } else {
            const message = response?.message || 'Ocorreu um erro inesperado.'
            error(message)
        }
    }

    return {
        alerts,
        addAlert,
        removeAlert,
        clearAll,
        success,
        error,
        warning,
        info,
        showApiResponse
    }
}
