﻿using System;
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
        private readonly IChatData _chatStorage = new StaticChatData();
        private const int MsgBufferSize = 1000;

        public async Task WebSocketRequest(AspNetWebSocketContext context) {
            // Получаем сокет клиента из контекста запроса
            var socket = context.WebSocket;

            var userId = GetUserIdFromCookie(context);
            var chatUser = _chatStorage.GetChatUserById(userId);

            WsConnectionManager.AddWsChatEntity(new WsChatEntity(socket, chatUser));

            // Слушаем его
            while (socket.State == WebSocketState.Open) {               
                var receiveBuffer = new ArraySegment<byte>(new byte[MsgBufferSize]);

                // Ожидаем данные от него
                var receiveResult = await socket.ReceiveAsync(receiveBuffer, CancellationToken.None);

                var stringResult = BufferMsgToString(receiveBuffer, receiveResult.Count);

                AddReceivedMsgToChatRoom(stringResult);

                //Передаём сообщение всем клиентам
                await WsConnectionManager.BroadcastMessage(stringResult);
            }
        }

        private string BufferMsgToString(ArraySegment<byte> buffer, int count) {
            return Encoding.UTF8.GetString(buffer.Array, 0, count);
        }

        private void AddReceivedMsgToChatRoom(string str) {
            var chatMessage = JsonConvert.DeserializeObject<ChatMessage>(str);
            if (chatMessage != null) {
                _chatStorage.AddChatMessage(chatMessage);    
            }
        }

        private int GetUserIdFromCookie(AspNetWebSocketContext context) {
            var requestCookie = context.Cookies["id"];
            if (requestCookie != null)
                return Convert.ToInt32(requestCookie.Value);
            return -1;
        }
    }
}