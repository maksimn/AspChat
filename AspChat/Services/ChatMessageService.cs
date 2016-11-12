using System;
using System.Collections.Generic;
using System.Net.WebSockets;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Web.WebSockets;
using AspChat.ChatData;
using AspChat.Models;
using Newtonsoft.Json;

namespace AspChat.Services {
    public class ChatMessagesService : IChatMessageService {
        // Список всех сокетов-клиентов
        private static readonly List<WebSocket> WsClients = new List<WebSocket>();

        // Блокировка для обеспечения потокобезопасности
        private static readonly ReaderWriterLockSlim Lock = new ReaderWriterLockSlim();

        private readonly IChatData _chatStorage = new StaticChatData();

        private const int MsgBufferSize = 1000;

        public async Task WebSocketRequest(AspNetWebSocketContext context) {
            // Получаем сокет клиента из контекста запроса
            var socket = context.WebSocket;

            // Добавляем его в список клиентов
            Lock.EnterWriteLock();
            try {
                WsClients.Add(socket);
            } finally {
                Lock.ExitWriteLock();
            }

            // Слушаем его
            while (socket.State == WebSocketState.Open) {               
                var receiveBuffer = new ArraySegment<byte>(new byte[MsgBufferSize]);

                // Ожидаем данные от него
                WebSocketReceiveResult receiveResult = await socket.ReceiveAsync(receiveBuffer, CancellationToken.None);

                string stringResult = BufferMsgToString(receiveBuffer, receiveResult.Count);

                AddReceivedMsgToChatRoom(stringResult);

                //Передаём сообщение всем клиентам
                foreach (var client in WsClients) {
                    if (client.State == WebSocketState.Open) {
                        var outputBuffer = new ArraySegment<byte>(Encoding.UTF8.GetBytes(stringResult));
                        await client.SendAsync(outputBuffer, WebSocketMessageType.Text, true, CancellationToken.None);
                    }
                }
            }
        }

        private string BufferMsgToString(ArraySegment<byte> buffer, int count) {
            return Encoding.UTF8.GetString(buffer.Array, 0, count);
        }

        private void AddReceivedMsgToChatRoom(string str) {
            var chatMessage = JsonConvert.DeserializeObject<ChatMessage>(str);
            if (chatMessage != null) {
                Lock.EnterWriteLock();
                try {
                    _chatStorage.AddChatMessage(chatMessage);
                } finally {
                    Lock.ExitWriteLock();
                }      
            }
        }
    }
}