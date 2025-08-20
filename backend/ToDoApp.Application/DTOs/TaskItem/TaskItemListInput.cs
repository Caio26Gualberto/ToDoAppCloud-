using ToDoApp.Api.DTOs.Api;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Application.DTOs.TaskItem
{
    public class TaskItemListInput : Pagination
    {
        public string? Title { get; set; }
        public ETaskPriority? TaskPriority { get; set; }
        public List<ETaskCategory>? TaskCategory { get; set; }
        public DateTime? MinDueDate { get; set; }
        public DateTime? MaxDueDate { get; set; }
        public bool? IsCompleted { get; set; }
    }
}
