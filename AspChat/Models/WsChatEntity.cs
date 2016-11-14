using System.Net.WebSockets;

namespace AspChat.Models {
    public class WsChatEntity {
        private readonly WebSocket _webSocket;
        private ChatUser _chatUser;

        public WsChatEntity(WebSocket webSocket, ChatUser chatUser) {
            _webSocket = webSocket;
            _chatUser = chatUser;
        }

        public WebSocket WebSocket { get { return _webSocket; } }
        public ChatUser ChatUser { get { return _chatUser; } }
    }
}