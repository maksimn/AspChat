using System.Collections.Generic;
using AspChat.ViewModels;

namespace AspChat.ChatData {
    public static class InMemoryChatRepository {
        static InMemoryChatRepository() {
            ChatUsers = new List<ChatUserViewModel>();
            ChatMessages = new List<ChatMessage>();
        }

        public static List<ChatUserViewModel> ChatUsers { get; set; }
        public static List<ChatMessage> ChatMessages { get; set; }

        public static int NumChatUsers {
            get { return ChatUsers.Count; }
        }
    }
}