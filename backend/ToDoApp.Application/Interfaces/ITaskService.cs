using ToDoApp.Application.DTOs;
using ToDoApp.Application.DTOs.TaskItem;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Application.Interfaces
{
    public interface ITaskService
    {
        Task<PagedResult<TaskItem>> GetAllTasksAsync(TaskItemListInput input);
        Task<TaskItem?> GetTaskByIdAsync(int id);
        Task AddTaskAsync(TaskItemInput input);
        Task UpdateTaskAsync(TaskItemUpdateInput input);
        Task DeleteTaskAsync(int id);
        Task UpdateTaskCompletionAsync(TaskCompletionInput input);
    }
}
