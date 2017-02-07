using System.Web.Mvc;
using AspChat.ViewModels;
using AspChat.ChatData;

namespace AspChat.Controllers {
    public class ChatController : Controller {
        public ActionResult Index() {
            if (User.Identity.IsAuthenticated) {
                IChatData chatData = new StaticChatData();
                var viewModel = new ChatIndexViewModel(this.User.Identity.Name, chatData.ChatMessages);
                return View(viewModel);
            }
            return View((ChatIndexViewModel)null);
        }
	}
}
