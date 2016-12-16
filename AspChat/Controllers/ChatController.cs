using System.Collections.Generic;
using System.Web.Mvc;
using AspChat.Services;
using AspChat.ViewModels;

namespace AspChat.Controllers {
    public class ChatController : Controller {
        public ActionResult Index() {
            if (Session["userId"] == null) {
                return View((ChatIndexViewModel)null);
            } else {
                return null;
            }
        }
	}
}