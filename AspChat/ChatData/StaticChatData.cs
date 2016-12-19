using System;
using System.Collections.Generic;
using System.IO;

using Newtonsoft.Json;

using AspChat.Models;
using AspChat.ViewModels;

namespace AspChat.ChatData {
    public class StaticChatData : IChatData {
        private static string dbFileName = AppDomain.CurrentDomain.BaseDirectory + "App_Data\\ChatFileDB.txt";

        static StaticChatData() {
            if(!File.Exists(dbFileName)) {
                using (File.Create(dbFileName));
            } else {
                DeserializeFromFileIntoInMemoryChatRepository();
            }
            AppDomain.CurrentDomain.DomainUnload += SerializeInMemoryChatRepositoryIntoFile;
        }

        static void SerializeInMemoryChatRepositoryIntoFile(object sender, EventArgs e) {
            var obj = new {
                ChatUsers = InMemoryChatRepository.ChatUsers,
                ChatMessages = InMemoryChatRepository.ChatMessages
            };
            string jsonOutput = JsonConvert.SerializeObject(obj);
            File.WriteAllText(dbFileName, jsonOutput);
        }

        static void DeserializeFromFileIntoInMemoryChatRepository() {
            if (!File.Exists(dbFileName)) {
                return;
            }
            var jsonInput = File.ReadAllText(dbFileName);
            var definition = new { 
                ChatUsers = new List<ChatUser>(),
                ChatMessages = new List<ChatMessage>()
            };
            var result = JsonConvert.DeserializeAnonymousType(jsonInput, definition);
            InMemoryChatRepository.ChatUsers = result.ChatUsers;
            InMemoryChatRepository.ChatMessages = result.ChatMessages;
        }

        public void AddChatMessage(ChatMessage chatMessage) {
            InMemoryChatRepository.ChatMessages.Add(chatMessage);
        }

        public void AddChatUser(ChatUser chatUser) {
            InMemoryChatRepository.ChatUsers.Add(chatUser);
        }

        public List<ChatMessage> ChatMessages {
            get {
                return InMemoryChatRepository.ChatMessages;
            }
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
