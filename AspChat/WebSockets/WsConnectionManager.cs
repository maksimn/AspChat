using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspChat.WebSockets {
    public static class WsConnectionManager {
        private static List<WebSocket> _webSockets = new List<WebSocket>();

        public static void AddWebSocket(WebSocket ws) {
            _webSockets.Add(ws);
        }

        public static async Task SendChatMessageToAll(string message) {
            var outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            foreach (var webSocket in _webSockets) {
                if (webSocket.State == WebSocketState.Open) {                  
                    await webSocket.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }

        public static void DeleteWebSocket(WebSocket webSocket) {
            _webSockets.Remove(webSocket);
        }
    }
}