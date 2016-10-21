namespace AspChat.Models {
    public class ChatMessage {
        private readonly ChatUser _chatUser;
        private readonly string _text;

        public ChatMessage(ChatUser chatUser, string text) {
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