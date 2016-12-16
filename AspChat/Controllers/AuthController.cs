using System;
using System.Web.Mvc;

namespace AspChat.Controllers {
    public class AuthController : Controller {
        public ActionResult Register(string chatUserName, string password) {
            var obj = new { name = chatUserName, psw = password };
            return Redirect("/");
        }
    }
}