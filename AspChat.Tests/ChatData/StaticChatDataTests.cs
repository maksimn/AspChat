using NUnit.Framework;

using AspChat.ChatData;
using AspChat.Models;

namespace AspChat.Tests.ChatData {
    [TestFixture]
    class StaticChatDataTests {
        [Test]
        public void IsUserWithGivenNameExist_NoUsersInRepository_Correctness() {
            IChatData chatData = new StaticChatData();

            bool res = chatData.IsUserWithGivenNameExist("Patrick");

            Assert.IsFalse(res);
        }
        [Test]
        public void IsUserWithGivenNameExist_OneUserExistsInRepository_CheckHisOrHerExistence() {
            IChatData chatData = new StaticChatData();
            chatData.AddChatUser(new ChatUser(0, "Patrick", "1234"));

            bool res = chatData.IsUserWithGivenNameExist("Patrick");

            Assert.IsTrue(res);
        }
    }
}
