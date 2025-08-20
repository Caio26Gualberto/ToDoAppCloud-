using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Domain.Enums
{
    public enum ETaskPriority
    {
        [Display(Name = "Baixa")]
        Low = 0,
        [Display(Name = "Normal")]
        Normal = 1,
        [Display(Name = "Alta")]
        High = 2,
        [Display(Name = "Crítica")]
        Critical = 3
    }
}
