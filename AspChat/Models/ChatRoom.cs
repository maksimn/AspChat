using System.Collections.Generic;

namespace AspChat.Models {
    public static class ChatRoom {
        static ChatRoom() {
            ChatUsers = new List<ChatUser>();
            ChatMessages = new List<ChatMessage>();
        }

        public static List<ChatUser> ChatUsers { get; set; }
        public static List<ChatMessage> ChatMessages { get; set; }

        public static int NumChatUsers {
            get { return ChatUsers.Count; }
        }
    }
}