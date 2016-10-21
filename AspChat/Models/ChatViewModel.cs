using System.Collections.Generic;

namespace AspChat.Models {
    public class ChatViewModel {
        public ChatViewModel(ChatUser user, List<ChatMessage> messages) {
            ThisUser = user;
            ChatMessages = messages;
        }
        public ChatUser ThisUser { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
    }
}