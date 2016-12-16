using System.Net.WebSockets;
using AspChat.ViewModels;

namespace AspChat.WebSockets {
    public class WsChatEntity {
        private readonly WebSocket _webSocket;
        private ChatUserViewModel _chatUser;

        public WsChatEntity(WebSocket webSocket, ChatUserViewModel chatUser) {
            _webSocket = webSocket;
            _chatUser = chatUser;
        }

        public WebSocket WebSocket { get { return _webSocket; } }
        public ChatUserViewModel ChatUser { get { return _chatUser; } }
    }
}