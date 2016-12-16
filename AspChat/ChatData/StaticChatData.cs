using System.Collections.Generic;
using AspChat.ViewModels;

namespace AspChat.ChatData {
    public class StaticChatData : IChatData {
        public void AddChatMessage(ChatMessage chatMessage) {
            InMemoryChatRepository.ChatMessages.Add(chatMessage);
        }

        public int NumChatUsers {
            get {
                return InMemoryChatRepository.ChatUsers.Count;
            }
        }

        public void AddChatUser(ChatUserViewModel chatUser) {
            InMemoryChatRepository.ChatUsers.Add(chatUser);
        }

        public List<ChatMessage> ChatMessages {
            get {
                return InMemoryChatRepository.ChatMessages;
            }
        }

        public ChatUserViewModel GetChatUserById(int id) {
            if(InMemoryChatRepository.ChatUsers.Exists(chatUser => chatUser.Id == id)) { 
                return InMemoryChatRepository.ChatUsers.Find(chatuser => chatuser.Id == id);
            } else {
                var newUser = new ChatUserViewModel(id, "Гость" + (id + 1));
                AddChatUser(newUser);
                return newUser;
            }
        }

        public void DeleteUser(int userId) {
            var chatUser = InMemoryChatRepository.ChatUsers.Find(chatuser => chatuser.Id == userId);
            InMemoryChatRepository.ChatUsers.Remove(chatUser);
        }

        public int GetIdForNewUser() {
            int newId = 0;
            while (InMemoryChatRepository.ChatUsers.Exists(chatUser => chatUser.Id == newId)) {
                newId++;
            }
            return newId;
        }
    }
}