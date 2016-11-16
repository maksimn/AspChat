using System;
using System.Web;
using AspChat.ChatData;
using AspChat.Models;
using AspChat.ViewModels;

namespace AspChat.Services {
    public class HomeService : IHomeService {
        private readonly HttpContextBase _httpContext;
        private readonly IChatData _chatStorage = new StaticChatData();

        public HomeService(HttpContextBase httpContext) {
            _httpContext = httpContext;
        }

        public ChatViewModel GetIndexViewModel() {
            // Если это первый запрос от пользователя
            if (_httpContext.Request.Cookies["id"] == null) {
                var nextUserId = _chatStorage.GetIdForNewUser();
                var userIdForName = nextUserId + 1;
                var newChatUser = new ChatUser(nextUserId, "Гость" + userIdForName);
                _chatStorage.AddChatUser(newChatUser);
                CreateCookieForId(nextUserId);
                return new ChatViewModel(newChatUser, _chatStorage.ChatMessages);                
            }
            // Если это запрос от уже делавшего запросы пользователя
            var id = GetUserIdFromCookie();               
            return new ChatViewModel(_chatStorage.GetChatUserById(id), _chatStorage.ChatMessages);
        }
        private int GetUserIdFromCookie() {
            var requestCookie = _httpContext.Request.Cookies["id"];
            if (requestCookie != null) {
                return Convert.ToInt32(requestCookie.Value);
            }
            return -1;
        }

        private void CreateCookieForId(int id) {
            _httpContext.Response.Cookies.Add(new HttpCookie("id", id.ToString()));
        }
    }
}