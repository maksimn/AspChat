using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web;
using System.Web.WebSockets;
using AspChat.Models;

namespace AspChat {
    public class ChatHandler : IHttpHandler {
        private int MSG_BUFFER_SIZE = 1000;
        // Список всех клиентов
        // WebSocket -- класс, позволяющий отправлять и получать данные по сети
        private static readonly List<WebSocket> Clients = new List<WebSocket>();

        // Блокировка для обеспечения потокобезопасности
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();

        public bool IsReusable {
            get { return true; }
        }

        public void ProcessRequest(HttpContext context) {
            if (context.IsWebSocketRequest) {
                context.AcceptWebSocketRequest(WebSocketRequest);
            }
        }

        private async Task WebSocketRequest(AspNetWebSocketContext context) {
            // Получаем сокет клиента из контекста запроса
            var socket = context.WebSocket;

            // Добавляем его в список клиентов
            Locker.EnterWriteLock();
            try {
                Clients.Add(socket);
            } finally {
                Locker.ExitWriteLock();
            }

            // Слушаем его
            while (true) {
                var buffer = new ArraySegment<byte>(new byte[MSG_BUFFER_SIZE]);

                // Ожидаем данные от него
                await socket.ReceiveAsync(buffer, CancellationToken.None);
                // Перекодируем результат в строку
                string result = BufferMsgToString(buffer);
                // Строку нужно распарсить и добавить в ChatRoom.ChatMessages
                AddReceivedMsgToChatRoom(result);

                //Передаём сообщение всем клиентам
                for (int i = 0; i < Clients.Count; i++) {

                    WebSocket client = Clients[i];

                    try {
                        if (client.State == WebSocketState.Open) {
                            await client.SendAsync(buffer, WebSocketMessageType.Text, true, CancellationToken.None);
                        }
                    } catch (ObjectDisposedException) {
                        Locker.EnterWriteLock();
                        try {
                            Clients.Remove(client);
                            i--;
                        } finally {
                            Locker.ExitWriteLock();
                        }
                    }
                }
            }
        }

        private String BufferMsgToString(ArraySegment<byte> buffer) {
            string result = System.Text.Encoding.UTF8.GetString(buffer.Array);
            return result.Substring(0, result.IndexOf('\0'));
        }

        private void AddReceivedMsgToChatRoom(string str) {
            int delimInd = str.IndexOf(':');
            int msgShift = 2;
            string userName = str.Substring(0, delimInd);
            string message = str.Substring(delimInd + msgShift);

            ChatUser user = ChatRoom.ChatUsers.Find(chUsr => chUsr.Name == userName);
            ChatRoom.ChatMessages.Add(new ChatMessage(user, message));
        }
    }
}