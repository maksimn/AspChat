using System;
using System.Web;
using AspChat.ChatData;
using AspChat.Models;
using AspChat.ViewModels;

namespace AspChat.Services {
    public class HomeService : IHomeService {
        private HttpContextBase httpContext;
        private IChatData chatStorage = new StaticChatData();

        public HomeService(HttpContextBase httpContext) {
            this.httpContext = httpContext;
        }

        public ChatViewModel GetIndexViewModel() {
            // Если это самый первый запрос от самого первого пользователя приложения
            ChatViewModel viewModel;
            if (chatStorage.NumChatUsers == 0) {
                var newUser = new ChatUser(0, "Гость1");
                chatStorage.AddChatUser(newUser);
                viewModel = new ChatViewModel(newUser, chatStorage.ChatMessages);
                CreateCookieForId(newUser.Id);
            }
            // Если это первый запрос для нового пользователя
            else if (httpContext.Request.Cookies["id"] == null) {
                var nextUserId = chatStorage.NumChatUsers;
                chatStorage.AddChatUser(new ChatUser(nextUserId, "Гость" + (nextUserId + 1)));
                
                viewModel = new ChatViewModel(chatStorage.GetChatUserById(nextUserId), chatStorage.ChatMessages);
                CreateCookieForId(nextUserId);
            }
            // Если это запрос от уже делавшего запросы пользователя
            else {
                var id = GetUserIdFromCookie();
                
                viewModel = new ChatViewModel(chatStorage.GetChatUserById(id), chatStorage.ChatMessages);
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