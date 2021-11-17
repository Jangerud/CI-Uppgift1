using System;
using System.IO;
using CI_Uppgift1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CI_Uppgift1Test
{
    [TestClass]
    public class IntegrationTests
    {
        private Logic logic;
        private Admin admin;
        private CreateUserTests ct;
        private string filePath;

        [TestInitialize]
        public void TestInitialize()
        {
            this.admin = new Admin();
            this.logic = new Logic();
            this.filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            logic.SerializeData(logic.CreateDummyData(), filePath + "/users.json");
        }

        [TestCleanup]
        public void TestCleanup()
        {
            this.logic = null;
            File.Delete(filePath + "/users.json");
        }

        [TestMethod]
        public void Integration_AdminCreateUser_ReturnsFalse()
        {
            string username = "admin", password = "admin123";
            bool loginSuccessful = logic.Login(username, password);
            Assert.IsFalse(admin.CreateUser("test", 12000, "user2", "123"));
        }
    }
}