using System.Web;
using AspChat.Services;

namespace AspChat {
    public class ChatHandler : IHttpHandler {
        public bool IsReusable {
            get { return false; }
        }

        public void ProcessRequest(HttpContext context) {
            if (context.IsWebSocketRequest) {
                IChatMessageService chatMessageService = new ChatMessagesService();
                context.AcceptWebSocketRequest(chatMessageService.WebSocketRequest);
            }
        }
    }
}