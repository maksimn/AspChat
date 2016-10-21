using System;
using System.Collections.Generic;

namespace AspChat.Models {
    public class ChatRoom {
        public ChatRoom() {
            ChatUsers = new List<ChatUser>();
            ChatMessages = new List<ChatMessage>();
        }

        public ChatUser ThisUser { get; set; }
        public List<ChatUser> ChatUsers { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }

        public Int32 NumChatUsers {
            get { return ChatUsers.Count; }
        }
    }
}