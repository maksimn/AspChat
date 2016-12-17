using System.Collections.Generic;
using AspChat.Models;
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

        public void AddChatUser(ChatUser chatUser) {
            InMemoryChatRepository.ChatUsers.Add(chatUser);
        }

        public List<ChatMessage> ChatMessages {
            get {
                return InMemoryChatRepository.ChatMessages;
            }
        }

        public ChatUser GetChatUserById(int id) {
            return InMemoryChatRepository.ChatUsers.Find(chatuser => chatuser.Id == id);
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


        public bool IsUserWithGivenNameExist(string userName) {
            return InMemoryChatRepository.ChatUsers.Exists(chatUser => chatUser.Name == userName);
        }


        public bool AuthenticateUser(string username, string password) {
            return InMemoryChatRepository.ChatUsers.Exists(
                chatUser => chatUser.Name == username && chatUser.Password == password
            );
        }

        public void ClearAllData() {
            InMemoryChatRepository.ChatUsers = new List<ChatUser>();
            InMemoryChatRepository.ChatMessages = new List<ChatMessage>();
        }

        public ChatUserViewModel GetChatUserViewModelByName(string username) {
            var chatUser = InMemoryChatRepository.ChatUsers.Find(u => u.Name == username);
            if (chatUser != null) {
                return new ChatUserViewModel(chatUser.Id, chatUser.Name);
            }
            return null;
        }
    }
}