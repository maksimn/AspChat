using NUnit.Framework;

using AspChat.ChatData;
using AspChat.Models;
using AspChat.ViewModels;

namespace AspChat.Tests.ChatData {
    [TestFixture]
    class StaticChatDataTests {
        private IChatData chatData = new StaticChatData();
        [Test]
        public void IsUserWithGivenNameExist_NoUsersInRepository_ReturnFalse() {
            bool res = chatData.IsUserWithGivenNameExist("Patrick");

            Assert.IsFalse(res);
        }
        [Test]
        public void IsUserWithGivenNameExist_OneUserExistsInRepositoryCheckHisOrHerExistence_ReturnTrue() {
            chatData.AddChatUser(new ChatUser(0, "Patrick", "1234"));

            bool res = chatData.IsUserWithGivenNameExist("Patrick");

            Assert.IsTrue(res);
        }
        [Test]
        public void AuthenticateUser_NoUsersInRepository_ReturnFalse() {
            Assert.IsFalse(chatData.AuthenticateUser("Patrick", "1234"));
        }
        [Test]
        public void AuthenticateUser_OneUserInRepository_ReturnTrue() {
            chatData.AddChatUser(new ChatUser(0, "Abc", "xxx"));

            Assert.IsTrue(chatData.AuthenticateUser("Abc", "xxx"));
        }
        [Test]
        public void GetChatUserViewModelByName_NoUsers_ReturnNull() {
            var viewModel = chatData.GetChatUserViewModelByName("Abc");

            Assert.IsNull(viewModel);
        }
        [Test]
        public void GetChatUserViewModelByName_TwoUsersInRepository_ReturnSecondAddedUserViewModel() {
            chatData.AddChatUser(new ChatUser(0, "Abc", "1234"));
            chatData.AddChatUser(new ChatUser(1, "Xyz", "4321"));

            var viewModel = chatData.GetChatUserViewModelByName("Xyz");

            var expected = new ChatUserViewModel(1, "Xyz");
            Assert.AreEqual(expected, viewModel);
        }
        [TearDown]
        public void ClearRepository() {
            chatData.ClearAllData();
        }
    }
}
