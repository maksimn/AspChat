using System.Collections.Generic;
using AspChat.Models;
using AspChat.ViewModels;

namespace AspChat.ChatData {
    public interface IChatData {
        void AddChatMessage(ChatMessage chatMessage);

        int NumChatUsers { get; }

        void AddChatUser(ChatUser chatUser);

        List<ChatMessage> ChatMessages { get; }

        ChatUser GetChatUserById(int id);

        ChatUserViewModel GetChatUserViewModelByName(string username);

        void DeleteUser(int userId);

        int GetIdForNewUser();

        bool IsUserWithGivenNameExist(string userName);

        bool AuthenticateUser(string username, string password);

        void ClearAllData();
    }
}
