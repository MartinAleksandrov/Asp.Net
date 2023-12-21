using System.ComponentModel.DataAnnotations;

namespace My_First_MVC_Project.ViewModels
{
    public class MessageViewModel
    {
        [Required]
        public string Sender { get; set; } = null!;

        [Required]
        public string MessageText { get; set; } = null!;
    }
}
