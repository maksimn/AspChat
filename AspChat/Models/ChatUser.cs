namespace AspChat.Models {
    public class ChatUser {
        private readonly int _id;
        private readonly string _name;
        private readonly string _password;

        public ChatUser(int id, string name, string password) {
            _id = id;
            _name = name;
            _password = password;
        }

        public int Id {
            get { return _id; }
        }

        public string Name {
            get { return _name; }
        }

        public string Password {
            get { return _password; }
        }
    }
}