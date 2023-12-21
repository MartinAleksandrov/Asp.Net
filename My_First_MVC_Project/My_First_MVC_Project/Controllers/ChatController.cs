namespace My_First_MVC_Project.Controllers
{
    using Microsoft.AspNetCore.Mvc;
    using My_First_MVC_Project.ViewModels;

    public class ChatController : Controller
    {
        private static List<KeyValuePair<string, string>> messages =
            new List<KeyValuePair<string, string>>();

        public IActionResult Show()
        {
            if (messages.Count < 1)
            {
                return View(new ChatViewModel());
            }

            var chatModel = new ChatViewModel
            {
                Messages = messages
                .Select(m => new MessageViewModel
                {
                    Sender = m.Key,
                    MessageText = m.Value
                })
                .ToList()
            };


            return View(chatModel);
        }

        [HttpPost]
        public IActionResult Send(ChatViewModel chat)
        {
            var newMassage = chat.CurrentMessage;

            messages.Add(new KeyValuePair<string, string>
                (newMassage.Sender,newMassage.MessageText));

            return RedirectToAction("Show");
        }
    }
}
