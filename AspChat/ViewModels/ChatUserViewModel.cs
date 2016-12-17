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

        public override bool Equals(object obj) {
            if (obj == null || obj.GetType() != typeof(ChatUserViewModel)) {
                return false;
            }
            var compared = (ChatUserViewModel)obj;
            return compared.Id == this.Id && compared.Name == this.Name;
        }
    }
}