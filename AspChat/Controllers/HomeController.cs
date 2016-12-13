using System.Collections.Generic;
using System.Web.Mvc;
using AspChat.Services;
using AspChat.Models;
using AspChat.ViewModels;

namespace AspChat.Controllers {
    public class HomeController : Controller {
        public ActionResult Index() {
            if (Session["userId"] == null) {
                return View((ChatViewModel)null);
            } else {
                return View(
                    new ChatViewModel(
                        new ChatUser(671, "SomeUser"), 
                        new List<ChatMessage>()
                    )
                );
            }
        }
	}
}