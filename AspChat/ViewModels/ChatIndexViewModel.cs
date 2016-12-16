using System.Collections.Generic;
using AspChat.ViewModels;
using Newtonsoft.Json;

namespace AspChat.ViewModels {
    public class ChatIndexViewModel {
        public ChatIndexViewModel(ChatUserViewModel user, List<ChatMessage> messages) {
            ThisUser = user;
            ChatMessages = messages;
        }
        public ChatUserViewModel ThisUser { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
        public string JsonChatMessages {
            get {
                return JsonConvert.SerializeObject(ChatMessages);
            }
        }
    }
}