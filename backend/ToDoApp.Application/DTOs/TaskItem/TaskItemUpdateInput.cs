using ToDoApp.Domain.Enums;

namespace ToDoApp.Application.DTOs.TaskItem
{
    public class TaskItemUpdateInput
    {
        public int Id { get; set; }
        public string? Title { get; set; }
        public string? Description { get; set; }
        public ETaskPriority? TaskPriority { get; set; }
        public List<ETaskCategory>? TaskCategories { get; set; } = new List<ETaskCategory>();
        public DateTime? DueDate { get; set; }
    }
}
