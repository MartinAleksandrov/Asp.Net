namespace My_First_MVC_Project.ViewModels
{
    public class ChatViewModel
    {
        public MessageViewModel CurrentMessage { get; set; } = null!;

        public List<MessageViewModel> Messages { get; set; } = null!;
    }
}
