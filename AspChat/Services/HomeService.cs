using System;
using System.Web;
using AspChat.Models;

namespace AspChat.Services {
    public class HomeService : IHomeService {
        private HttpContextBase httpContext;

        public HomeService(HttpContextBase httpContext) {
            this.httpContext = httpContext;
        }

        public ChatViewModel GetIndexViewModel() {
            // Если это самый первый запрос от самого первого пользователя приложения
            ChatViewModel viewModel;
            if (ChatRoom.NumChatUsers == 0) {
                var newUser = new ChatUser(0, "Гость1");
                ChatRoom.ChatUsers.Add(newUser);
                viewModel = new ChatViewModel(newUser, ChatRoom.ChatMessages);
                CreateCookieForId(newUser.Id);
            }
                // Если это первый запрос для нового пользователя
            else if (httpContext.Request.Cookies["id"] == null) {
                var nextUserId = ChatRoom.NumChatUsers;
                ChatRoom.ChatUsers.Add(new ChatUser(nextUserId, "Гость" + (nextUserId + 1)));
                viewModel = new ChatViewModel(ChatRoom.ChatUsers[nextUserId], ChatRoom.ChatMessages);
                CreateCookieForId(nextUserId);
            }
                // Если это запрос от уже делавшего запросы пользователя
            else {
                var id = GetUserIdFromCookie();
                viewModel = new ChatViewModel(ChatRoom.ChatUsers[id], ChatRoom.ChatMessages);
            }
            return viewModel;
        }
        private int GetUserIdFromCookie() {
            var requestCookie = httpContext.Request.Cookies["id"];
            if (requestCookie != null)
                return Convert.ToInt32(requestCookie.Value);
            return -1;
        }

        private void CreateCookieForId(int id) {
            httpContext.Response.Cookies.Add(new HttpCookie("id", id.ToString()));
        }
    }
}