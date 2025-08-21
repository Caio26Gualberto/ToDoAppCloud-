using ToDoApp.Domain.Enums;

namespace ToDoApp.Domain.Entities
{
    public class TaskItem : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
        public string Description { get; set; } = string.Empty;
        public bool IsCompleted { get; set; } = false;
        public ETaskPriority TaskPriority { get; set; } = ETaskPriority.Normal;
        public DateTime? DueDate { get; set; }
        public List<TaskCategoryItem>? TaskCategories { get; set; } = new List<TaskCategoryItem>();
    }
}

    
