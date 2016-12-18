﻿using System.Web.Mvc;
using AspChat.ViewModels;
using AspChat.ChatData;

namespace AspChat.Controllers {
    public class ChatController : Controller {
        public ActionResult Index() {
            if (User.Identity.IsAuthenticated) {
                IChatData chatData = new StaticChatData();
                var chatUserVM = chatData.GetChatUserViewModelByName(this.User.Identity.Name);
                var viewModel = new ChatIndexViewModel(chatUserVM, chatData.ChatMessages);
                return View(viewModel);
            }
            return View((ChatIndexViewModel)null);
        }
	}
}
