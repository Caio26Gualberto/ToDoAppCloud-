using System.Text.Json.Serialization;
using ToDoApp.Domain.Enums;

namespace ToDoApp.Domain.Entities
{
    public class TaskCategoryItem : BaseEntity
    {
        public int TaskItemId { get; set; }
        [JsonIgnore]
        public TaskItem TaskItem { get; set; }

        public ETaskCategory Category { get; set; }
    }
}
