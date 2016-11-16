using System.Collections.Generic;
using AspChat.Models;

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
            return InMemoryChatRepository.ChatUsers[id];
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