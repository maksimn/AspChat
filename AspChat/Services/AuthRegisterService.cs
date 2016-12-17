using System;
using AspChat.ChatData;
using AspChat.Models;

namespace AspChat.Services {
    internal sealed class AuthRegisterService {
        public class RegisterUserResult {
            public string RedirectUrl { get; set; }
            public string ServiceMessage { get; set; }
        }

        public RegisterUserResult RegisterChatUser(string chatUserName, string password) {
            IChatData chatData = new StaticChatData();

            var result = new RegisterUserResult();

            if (chatData.IsUserWithGivenNameExist(chatUserName)) {
                result.ServiceMessage =  "Пользователь с таким именем уже существует.\\nПопробуйте другое имя.";
                result.RedirectUrl = "#/register";
                return result;
            }

            try {
                chatData.AddChatUser(
                    new ChatUser(
                        chatData.GetIdForNewUser(),
                        chatUserName,
                        password
                    )
                );
            } catch (Exception) {
                if(!chatData.IsUserWithGivenNameExist(chatUserName)) {
                    result.ServiceMessage =  "При регистрации пользователя произошла ошибка.\\nПопробуйте ещё раз.";
                    result.RedirectUrl = "#/register";
                    return result;
                }
            }

            result.ServiceMessage =  "Пользователь успешно зарегистрирован.\\n"
                                     + "Введите указанные данные для входа в чат.";
            result.RedirectUrl = "#/login";
            return result;
        }
    }
}