using System;
using System.Web;
using AspChat.Services;

namespace AspChat {
    public class ChatHandler : IHttpHandler {
        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            if (context.IsWebSocketRequest) {
                context.AcceptWebSocketRequest(new ChatMessagesService().WebSocketRequest);
            }
        }
    }
}