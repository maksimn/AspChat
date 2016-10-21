using System;
using System.Web;
using System.Web.Mvc;
using AspChat.Models;

namespace AspChat.Controllers {
    public class HomeController : Controller {
        private const string KeyChatRoom = "ChatRoom";

        public ActionResult Index() {
            ChatRoom chatRoom;
            // Если это самый первый запрос от самого первого пользователя приложения
            if (HttpContext.Cache[KeyChatRoom] == null) {
                // Тогда создаем комнату, объект 1-го юзера,
                // добавляем его куки и помещаем его комнату в кэш
                chatRoom = new ChatRoom();
                var newUser = new ChatUser(0, "Гость1");
                chatRoom.ChatUsers.Add(newUser);
                chatRoom.ThisUser = newUser;
                HttpContext.Response.Cookies.Add(new HttpCookie("id", newUser.Id.ToString()));
                HttpContext.Cache[KeyChatRoom] = chatRoom;
            }
            // Если это первый запрос для нового пользователя
            else if (HttpContext.Request.Cookies["id"] == null) {
                chatRoom = (ChatRoom) HttpContext.Cache[KeyChatRoom];
                var nextUserId = chatRoom.NumChatUsers;
                chatRoom.ChatUsers.Add(new ChatUser(nextUserId, "Гость" + (nextUserId + 1)));
                chatRoom.ThisUser = chatRoom.ChatUsers[nextUserId];
                HttpContext.Response.Cookies.Add(new HttpCookie("id", nextUserId.ToString()));
            }
            // Если это запрос от уже делавшего запросы пользователя
            else {
                chatRoom = (ChatRoom) HttpContext.Cache[KeyChatRoom];
                var id = Convert.ToInt32(HttpContext.Request.Cookies["id"].Value);
                chatRoom.ThisUser = chatRoom.ChatUsers[id];
            }
            HttpContext.Cache[KeyChatRoom] = chatRoom;
            return View(chatRoom);
        }
	}
}