using System;

namespace AspChat.Models {
    public class ChatMessage {
        private ChatUser _chatUser;
        private string _text;

        public ChatMessage(ChatUser chatUser, String text) {
            _chatUser = chatUser;
            _text = text;
        }

        public ChatUser ChatUser {
            get { return _chatUser; }
        }

        public string Text {
            get { return _text; }
        }
    }
}