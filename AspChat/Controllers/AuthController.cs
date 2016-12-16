using System;
using System.Web.Mvc;

namespace AspChat.Controllers {
    public class AuthController : Controller {
        public ActionResult Register(string chatUserName, string password) {

            return Redirect("/#/login");
        }
    }
}