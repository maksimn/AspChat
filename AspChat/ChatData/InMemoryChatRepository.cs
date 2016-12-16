using System.Collections.Generic;
using AspChat.Models;

namespace AspChat.ChatData {
    public static class InMemoryChatRepository {
        static InMemoryChatRepository() {
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