using System.Web;
using System.Web.Mvc;
using AspChat.Models;

namespace AspChat.Controllers {
    public class HomeController : Controller {
        private const string KeyChatRoom = "ChatRoom";
        public ActionResult Index() {
            ChatRoom chatRoom;
            if (HttpContext.Cache[KeyChatRoom] == null) {
                chatRoom = new ChatRoom();
                chatRoom.ChatUsers.Add(new ChatUser(1, "Гость1"));
                HttpContext.Response.Cookies.Add(new HttpCookie("id", 1.ToString()));
                HttpContext.Cache[KeyChatRoom] = chatRoom;
            }
            else {
                chatRoom = (ChatRoom)HttpContext.Cache[KeyChatRoom];
                if (HttpContext.Request.Cookies["id"] == null) {
                    var nextUserId = chatRoom.NumChatUsers + 1;
                    chatRoom.ChatUsers.Add(new ChatUser(nextUserId, "Гость" + nextUserId));
                    HttpContext.Response.Cookies.Add(new HttpCookie("id", nextUserId.ToString()));
                }
            }
            return View(chatRoom);
        }
	}
}