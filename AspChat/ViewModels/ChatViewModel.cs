using System.Collections.Generic;
using AspChat.Models;
using Newtonsoft.Json;

namespace AspChat.ViewModels {
    public class ChatViewModel {
        public ChatViewModel(ChatUser user, List<ChatMessage> messages) {
            ThisUser = user;
            ChatMessages = messages;
        }
        public ChatUser ThisUser { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
        public string JsonChatMessages {
            get {
                return JsonConvert.SerializeObject(ChatMessages);
            }
        }
    }
}