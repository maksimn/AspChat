using System.Collections.Generic;
using System.Web.Mvc;
using AspChat.Services;
using AspChat.ViewModels;
using AspChat.ChatData;

namespace AspChat.Controllers {
    public class ChatController : Controller {
        public ActionResult Index() {
            if (this.User.Identity.IsAuthenticated) {
                IChatData chatData = new StaticChatData();
                var chatUserVM = chatData.GetChatUserViewModelByName(this.User.Identity.Name);
                var viewModel = new ChatIndexViewModel(chatUserVM, chatData.ChatMessages);
                return View(viewModel);
            }
            return View((ChatIndexViewModel)null);
        }
	}
}