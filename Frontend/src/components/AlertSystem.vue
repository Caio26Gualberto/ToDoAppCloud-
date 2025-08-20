<template>
  <div class="alert-container">
    <TransitionGroup name="alert" tag="div">
      <div
        v-for="alert in alerts"
        :key="alert.id"
        :class="['alert', `alert-${alert.type}`]"
        @click="removeAlert(alert.id)"
      >
        <div class="alert-content">
          <span class="alert-icon">
            {{ getAlertIcon(alert.type) }}
          </span>
          <span class="alert-message">{{ alert.message }}</span>
        </div>
        <button class="alert-close" @click.stop="removeAlert(alert.id)">
          ×
        </button>
      </div>
    </TransitionGroup>
  </div>
</template>

<script setup lang="ts">
import { getCurrentInstance } from 'vue'
import { useAlert } from '@/composables/useAlert'

const { alerts, removeAlert } = useAlert()

const getAlertIcon = (type: string): string => {
  switch (type) {
    case 'success': return '✓'
    case 'error': return '✗'
    case 'warning': return '⚠'
    case 'info': return 'ℹ'
    default: return '•'
  }
}
</script>

<style scoped>
.alert-container {
  position: fixed;
  top: 20px;
  right: 20px;
  z-index: 9999;
  max-width: 400px;
}

.alert {
  display: flex;
  align-items: center;
  justify-content: space-between;
  margin-bottom: 12px;
  padding: 16px;
  border-radius: 8px;
  box-shadow: 0 4px 12px rgba(0, 0, 0, 0.15);
  cursor: pointer;
  transition: all 0.3s ease;
  min-width: 300px;
}

.alert:hover {
  transform: translateX(-5px);
}

.alert-content {
  display: flex;
  align-items: center;
  gap: 12px;
  flex: 1;
}

.alert-icon {
  font-size: 1.2rem;
  font-weight: bold;
}

.alert-message {
  font-size: 0.9rem;
  line-height: 1.4;
}

.alert-close {
  background: none;
  border: none;
  font-size: 1.5rem;
  cursor: pointer;
  padding: 0;
  width: 24px;
  height: 24px;
  display: flex;
  align-items: center;
  justify-content: center;
  border-radius: 50%;
  transition: background-color 0.2s ease;
}

.alert-close:hover {
  background-color: rgba(0, 0, 0, 0.1);
}

/* Alert types */
.alert-success {
  background: #d4edda;
  color: #155724;
  border-left: 4px solid #28a745;
}

.alert-error {
  background: #f8d7da;
  color: #721c24;
  border-left: 4px solid #dc3545;
}

.alert-warning {
  background: #fff3cd;
  color: #856404;
  border-left: 4px solid #ffc107;
}

.alert-info {
  background: #d1ecf1;
  color: #0c5460;
  border-left: 4px solid #17a2b8;
}

/* Animations */
.alert-enter-active,
.alert-leave-active {
  transition: all 0.3s ease;
}

.alert-enter-from {
  opacity: 0;
  transform: translateX(100%);
}

.alert-leave-to {
  opacity: 0;
  transform: translateX(100%);
}

.alert-move {
  transition: transform 0.3s ease;
}

@media (max-width: 768px) {
  .alert-container {
    top: 10px;
    right: 10px;
    left: 10px;
    max-width: none;
  }
  
  .alert {
    min-width: auto;
  }
}
</style>
