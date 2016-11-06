using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Threading;
using System.Threading.Tasks;
using System.Web.WebSockets;
using AspChat.ChatData;
using AspChat.Models;

namespace AspChat.Services {
    public class ChatMessagesService : IChatMessageService {
        private const int MsgBufferSize = 1000;
        // Список всех клиентов
        // WebSocket -- класс, позволяющий отправлять и получать данные по сети
        private static readonly List<WebSocket> Clients = new List<WebSocket>();

        // Блокировка для обеспечения потокобезопасности
        private static readonly ReaderWriterLockSlim Locker = new ReaderWriterLockSlim();

        private readonly IChatData chatStorage = new StaticChatData();

        public async Task WebSocketRequest(AspNetWebSocketContext context) {
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
                var buffer = new ArraySegment<byte>(new byte[MsgBufferSize]);

                // Ожидаем данные от него
                await socket.ReceiveAsync(buffer, CancellationToken.None);
                
                Locker.EnterWriteLock();
                try {
                    // Перекодируем результат в строку
                    string result = BufferMsgToString(buffer);

                    AddReceivedMsgToChatRoom(result);
                } finally {
                    Locker.ExitWriteLock();
                }

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
            chatStorage.AddChatMessage(new ChatMessage(userName, message));
        }
    }
}