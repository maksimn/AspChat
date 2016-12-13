using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace AspChat.WebSockets {
    public static class WsConnectionManager {
        private static List<WsChatEntity> _wsChatEntities = new List<WsChatEntity>();

        public static void AddWsChatEntity(WsChatEntity wsChatEntity) {
            _wsChatEntities.Add(wsChatEntity);
        }

        public static async Task SendChatMessageToAll(string message) {
            var outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(message));
            foreach (var wsChatEntity in _wsChatEntities) {
                var client = wsChatEntity.WebSocket;
                if (client.State == WebSocketState.Open) {                  
                    await client.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                }
            }
        }

        public static void DeleteWsChatEntity(WsChatEntity entity) {
            _wsChatEntities.Remove(entity);
        }
    }
}