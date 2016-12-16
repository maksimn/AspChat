using System;

namespace AspChat.ViewModels {
    public class ChatUserViewModel {
        private readonly int _id;
        private String _name;

        public ChatUserViewModel(int id, String name) {
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