using System.Collections.Generic;
using AspChat.Models;
using AspChat.ViewModels;

namespace AspChat.ChatData {
    public interface IChatData {
        void AddChatMessage(ChatMessage chatMessage);

        void AddChatUser(ChatUser chatUser);

        List<ChatMessage> ChatMessages { get; }

        ChatUserViewModel GetChatUserViewModelByName(string username);

        int GetIdForNewUser();

        bool IsUserWithGivenNameExist(string userName);

        bool AuthenticateUser(string username, string password);

        void ClearAllData();
    }
}
