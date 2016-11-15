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
            // Если это самый первый запрос от самого первого пользователя приложения
            ChatViewModel viewModel;
            if (_chatStorage.NumChatUsers == 0 && _httpContext.Request.Cookies.Count == 0) {
                var newUser = new ChatUser(0, "Гость1");
                _chatStorage.AddChatUser(newUser);
                viewModel = new ChatViewModel(newUser, _chatStorage.ChatMessages);
                CreateCookieForId(newUser.Id);
            }
            // Если это первый запрос для нового пользователя
            else if (_httpContext.Request.Cookies["id"] == null) {
                var nextUserId = _chatStorage.GetIdForNewUser();
                var userIdForName = nextUserId + 1;
                _chatStorage.AddChatUser(new ChatUser(nextUserId, "Гость" + userIdForName));
                
                viewModel = new ChatViewModel(_chatStorage.GetChatUserById(nextUserId), _chatStorage.ChatMessages);
                CreateCookieForId(nextUserId);
            }
            // Если это запрос от уже делавшего запросы пользователя
            else {
                var id = GetUserIdFromCookie();               
                viewModel = new ChatViewModel(_chatStorage.GetChatUserById(id), _chatStorage.ChatMessages);
            }
            return viewModel;
        }
        private int GetUserIdFromCookie() {
            var requestCookie = _httpContext.Request.Cookies["id"];
            if (requestCookie != null)
                return Convert.ToInt32(requestCookie.Value);
            return -1;
        }

        private void CreateCookieForId(int id) {
            _httpContext.Response.Cookies.Add(new HttpCookie("id", id.ToString()));
        }
    }
}