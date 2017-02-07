using System.Collections.Generic;
using AspChat.Models;
using AspChat.ViewModels;
using Newtonsoft.Json;

namespace AspChat.ViewModels {
    public class ChatIndexViewModel {
        public ChatIndexViewModel(string userName, List<ChatMessage> messages) {
            ThisUserName = userName;
            ChatMessages = messages;
        }
        public string ThisUserName { get; set; }
        public List<ChatMessage> ChatMessages { get; set; }
        public string JsonChatMessages {
            get {
                return JsonConvert.SerializeObject(ChatMessages);
            }
        }
    }
}
