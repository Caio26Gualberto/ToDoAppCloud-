using Microsoft.EntityFrameworkCore;
using ToDoApp.Application.DTOs;
using ToDoApp.Application.DTOs.TaskItem;
using ToDoApp.Application.Interfaces;
using ToDoApp.Application.Utils.Extensions;
using ToDoApp.Domain.Entities;
using ToDoApp.Domain.Enums;
using ToDoApp.Domain.Repositories;

namespace ToDoApp.Application.Services
{
    public class TaskService : ITaskService
    {
        private readonly IRepository<TaskItem> _taskItemRepository;
        private readonly IRepository<TaskCategoryItem> _taskCategoryItemRepository;

        public TaskService(IRepository<TaskItem> repository, IRepository<TaskCategoryItem> taskCategoryRespository)
        {
            _taskItemRepository = repository;
            _taskCategoryItemRepository = taskCategoryRespository;
        }

        public async Task AddTaskAsync(TaskItemInput input)
        {
            ValidateTaskInput(input);

            var taskItem = new TaskItem
            {
                Title = input.Title,
                Description = input.Description,
                TaskPriority = input.TaskPriority,
                DueDate = input.DueDate,
                IsCompleted = false,
                TaskCategories = input.TaskCategories?.Any() == true ?
                input.TaskCategories.Select(c => new TaskCategoryItem
                {
                    Category = c,
                    CreatedAt = DateTime.UtcNow,
                    UpdatedAt = DateTime.UtcNow
                }).ToList() : null
            };

            await _taskItemRepository.AddAsync(taskItem);
        }

        public async Task DeleteTaskAsync(int id)
        {
            TaskItem task = await _taskItemRepository.GetByIdAsync(id)
                ?? throw new Exception("Tarefa não encontrada");

            await _taskItemRepository.DeleteAsync(task);
        }

        public async Task<PagedResult<TaskItem>> GetAllTasksAsync(TaskItemListInput input)
        {
            var taskItems = _taskItemRepository.GetAllAsync().Include(x => x.TaskCategories);

            var query = taskItems
                .WhereIf(!string.IsNullOrEmpty(input.Title), x => x.Title.ToLower().Contains(input.Title!.ToLower()))
                .WhereIf(input.TaskPriority != default, x => x.TaskPriority == input.TaskPriority)
                .WhereIf(input.TaskCategory != null && input.TaskCategory.Any(), x => input.TaskCategory!.All(c => x.TaskCategories.Select(tc => tc.Category).Contains(c)))
                .WhereIf(input.MinDueDate.HasValue, x => x.DueDate.HasValue && x.DueDate.Value.Date >= input.MinDueDate!.Value.Date)
                .WhereIf(input.MaxDueDate.HasValue, x => x.DueDate.HasValue && x.DueDate.Value.Date <= input.MaxDueDate!.Value.Date)
                .WhereIf(input.IsCompleted.HasValue, x => x.IsCompleted == input.IsCompleted);

            int totalCount = query.Count();
            var items = await query
                .OrderBy(x => x.DueDate)
                .Skip((input.PageNumber - 1) * input.PageSize)
                .Take(input.PageSize)
                .ToListAsync();

            return new PagedResult<TaskItem>
            {
                Items = items,
                TotalCount = totalCount,
                PageNumber = input.PageNumber,
                PageSize = input.PageSize
            };

        }

        public async Task<TaskItem?> GetTaskByIdAsync(int id)
        {
            TaskItem? task = await _taskItemRepository.GetAllAsync()
                .FirstOrDefaultAsync(x => x.Id == id);

            if (task == null)
                throw new Exception("Tarefa não encontrada");

            return task;
        }

        public async Task UpdateTaskAsync(TaskItemUpdateInput input)
        {
            var task = await _taskItemRepository.GetAllAsync()
                .Include(t => t.TaskCategories)
                .FirstOrDefaultAsync(x => x.Id == input.Id)
                ?? throw new Exception("Tarefa não encontrada");

            bool hasChanges = task.ApplyChanges(input);

            if (input.TaskCategories != null)
            {
                task.TaskCategories.Clear();

                task.TaskCategories = input.TaskCategories
                    .Select(c => new TaskCategoryItem
                    {
                        Category = c,
                        CreatedAt = DateTime.UtcNow,
                        UpdatedAt = DateTime.UtcNow,
                        TaskItemId = task.Id
                    })
                    .ToList();

                hasChanges = true;
            }

            if (hasChanges)
            {
                task.UpdatedAt = DateTime.UtcNow;
                await _taskItemRepository.UpdateAsync(task);
            }
        }

        public async Task UpdateTaskCompletionAsync(TaskCompletionInput input)
        {
            var task = await _taskItemRepository.GetAllAsync()
                .FirstOrDefaultAsync(x => x.Id == input.Id)
                ?? throw new Exception("Tarefa não encontrada");

            if (task.IsCompleted != input.IsCompleted)
            {
                task.IsCompleted = input.IsCompleted;
                task.UpdatedAt = DateTime.UtcNow;
                await _taskItemRepository.UpdateAsync(task);
            }
        }

        private void ValidateTaskInput(TaskItemInput input)
        {
            if (input == null)
                throw new ArgumentNullException(nameof(input), "A tarefa não pode ser vazia");

            if (string.IsNullOrWhiteSpace(input.Title) || string.IsNullOrWhiteSpace(input.Description))
                throw new ArgumentException("Título e descrição são obrigatórios");

            if (!Enum.IsDefined(typeof(ETaskPriority), input.TaskPriority))
                throw new ArgumentException("Prioridade inválida");

            if (input.TaskCategories != null || input.TaskCategories!.Any())
                foreach (var category in input.TaskCategories!)
                {
                    if (!Enum.IsDefined(typeof(ETaskCategory), category))
                        throw new ArgumentException($"Categoria inválida: {category}");
                }

            if (input.DueDate == default)
                throw new ArgumentException("Data de vencimento inválida");
        }
    }
}
