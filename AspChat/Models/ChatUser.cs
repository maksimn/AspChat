using System;

namespace AspChat.Models {
    public class ChatUser {
        private readonly int _id;
        private String _name;

        public ChatUser(int id, String name) {
            _id = id;
            _name = name;
        }

        public int Id {
            get { return _id; }
        }

        public String Name {
            get { return _name; }
        }
    }
}