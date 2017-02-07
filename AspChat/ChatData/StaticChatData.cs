using System;
using System.Collections.Generic;
using System.IO;
using System.Threading;

using Newtonsoft.Json;

using AspChat.Models;
using AspChat.ViewModels;

namespace AspChat.ChatData {
    public class StaticChatData : IChatData {
        private static string dbFileName = "D:\\PrevRabStol\\MyProgramming\\ASP.NET MVC\\AspChat\\AspChat\\App_Data\\ChatFileDB.txt";

        private readonly object _lock = new object();

        static StaticChatData() {
            if(!File.Exists(dbFileName)) {
                using (File.Create(dbFileName)) {};
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
            Monitor.Enter(_lock);
            InMemoryChatRepository.ChatMessages.Add(chatMessage);
            Monitor.Exit(_lock);
        }

        public void AddChatUser(ChatUser chatUser) {
            Monitor.Enter(_lock);
            InMemoryChatRepository.ChatUsers.Add(chatUser);
            Monitor.Exit(_lock);
        }

        public List<ChatMessage> ChatMessages {
            get {
                Monitor.Enter(_lock);
                var chatMessages = InMemoryChatRepository.ChatMessages;
                Monitor.Exit(_lock);
                return chatMessages;
            }
        }

        public int GetIdForNewUser() {
            int newId = 0;
            Monitor.Enter(_lock);
            while (InMemoryChatRepository.ChatUsers.Exists(chatUser => chatUser.Id == newId)) {
                newId++;
            }
            Monitor.Exit(_lock);
            return newId;
        }


        public bool IsUserWithGivenNameExist(string userName) {
            Monitor.Enter(_lock);
            var res = InMemoryChatRepository.ChatUsers.Exists(chatUser => chatUser.Name == userName);
            Monitor.Exit(_lock);
            return res;
        }


        public bool AuthenticateUser(string username, string password) {
            Monitor.Enter(_lock);
            bool isAuth = InMemoryChatRepository.ChatUsers.Exists(
                chatUser => chatUser.Name == username && chatUser.Password == password
            );
            Monitor.Exit(_lock);
            return isAuth;
        }

        public void ClearAllData() {
            InMemoryChatRepository.ChatUsers = new List<ChatUser>();
            InMemoryChatRepository.ChatMessages = new List<ChatMessage>();
        }

        private static class InMemoryChatRepository {
            static InMemoryChatRepository() {
                ChatUsers = new List<ChatUser>();
                ChatMessages = new List<ChatMessage>();
            }

            public static List<ChatUser> ChatUsers { get; set; }
            public static List<ChatMessage> ChatMessages { get; set; }
        }
    }
}
