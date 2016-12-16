using System;
using System.Web.Mvc;

using AspChat.ChatData;

namespace AspChat.Controllers {
    public class AuthController : Controller {
        public ActionResult Register(string chatUserName, string password) {
            IChatData chatData = new StaticChatData();

            return Redirect("/#/login");
        }
    }
}