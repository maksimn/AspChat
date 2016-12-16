using NUnit.Framework;

using AspChat.ChatData;

namespace AspChat.Tests.ChatData {
    [TestFixture]
    class StaticChatDataTests {
        [Test]
        public void IsUserWithGivenNameExist_NoUsersInRepository_Correctness() {
            IChatData chatData = new StaticChatData();

            bool res = chatData.IsUserWithGivenNameExist("Patrick");

            Assert.IsFalse(res);
        }
    }
}
