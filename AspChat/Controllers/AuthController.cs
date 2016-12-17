using System.Web.Mvc;
using AspChat.Services;

namespace AspChat.Controllers {
    using AspChat.ChatData;
    using AspChat.ViewModels;
    using System.Web.Security;

    public class AuthController : Controller {
        public ActionResult Register(string chatUserName, string password) {
            var service = new AuthRegisterService();

            var result = service.RegisterChatUser(chatUserName, password);

            TempData["message"] = result.ServiceMessage;

            return Redirect(result.RedirectUrl);
        }

        public ActionResult Login(string chatUserName, string password) {
            IChatData chatData = new StaticChatData();
            
            bool isAuth = chatData.AuthenticateUser(chatUserName, password);

            if (isAuth) {
                FormsAuthentication.SetAuthCookie(chatUserName, false);
                return Redirect("/");
            } else {
                TempData["message"] = "Неверное имя пользователя или пароль.";
                return Redirect("#/login");
            }
        }
    }
}
