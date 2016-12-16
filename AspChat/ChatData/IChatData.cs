using System.Collections.Generic;
using AspChat.ViewModels;

namespace AspChat.ChatData {
    public interface IChatData {
        void AddChatMessage(ChatMessage chatMessage);

        int NumChatUsers { get; }

        void AddChatUser(ChatUserViewModel chatUser);

        List<ChatMessage> ChatMessages { get; }

        ChatUserViewModel GetChatUserById(int id);

        void DeleteUser(int userId);

        int GetIdForNewUser();
    }
}
