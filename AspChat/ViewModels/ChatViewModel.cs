using System.Collections.Generic;
using AspChat.Models;

namespace AspChat.ViewModels {
    public class ChatViewModel {
        public ChatViewModel(ChatUser user, List<ChatMessage> messages) {
            ThisUser = user;
            ChatMessages = messages;
        }
        public ChatUser ThisUser { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
    }
}