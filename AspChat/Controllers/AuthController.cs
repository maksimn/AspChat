using System.Web.Mvc;
using AspChat.Services;

namespace AspChat.Controllers {
    public class AuthController : Controller {
        public ActionResult Register(string chatUserName, string password) {
            var service = new AuthRegisterService();
            var result = service.RegisterChatUser(chatUserName, password);
            TempData["message"] = result.ServiceMessage;
            return Redirect(result.RedirectUrl);
        }
    }
}
