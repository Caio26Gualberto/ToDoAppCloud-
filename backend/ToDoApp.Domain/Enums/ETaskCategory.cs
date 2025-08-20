using System.ComponentModel.DataAnnotations;

namespace ToDoApp.Domain.Enums
{
    public enum ETaskCategory
    {
        [Display(Name = "Casa")]
        Home = 0,

        [Display(Name = "Trabalho")]
        Work = 1,

        [Display(Name = "Pessoal")]
        Personal = 2,

        [Display(Name = "Compras")]
        Shopping = 3
    }
}
