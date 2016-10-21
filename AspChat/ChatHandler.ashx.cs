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
        private int MSG_BUFFER_SIZE = 1024;
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
                string result = System.Text.Encoding.UTF8.GetString(buffer.Array);
                result = result.Substring(0, result.IndexOf('\0'));
                // Строку нужно распарсить и добавить в ChatRepository.ChatMessages
                int delimInd = result.IndexOf(':');
                string userName = result.Substring(0, delimInd);
                string message = result.Substring(delimInd + 2);

                ChatUser user = ChatRepository.ChatUsers.Find((chUsr) => chUsr.Name == userName);
                ChatRepository.ChatMessages.Add(new ChatMessage(user, message));

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
    }
}