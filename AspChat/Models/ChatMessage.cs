namespace AspChat.Models {
    public class ChatMessage {
        private readonly string _chatUserName;
        private readonly string _text;

        public ChatMessage(string chatUser, string text) {
            _chatUserName = chatUser;
            _text = text;
        }

        public string ChatUserName {
            get { return _chatUserName; }
        }

        public string Text {
            get { return _text; }
        }
    }
}