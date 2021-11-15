using System;
using System.IO;
using CI_Uppgift1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CI_Uppgift1Test
{
    [TestClass]
    public class CreateUserTests
    {

        private Logic logic;
        private Admin admin;
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
        public void CreateUser_UserAlreadyExists_ReturnTrue()
        {
            Assert.IsTrue(admin.CreateUser("test", 12000, "user1", "123"));
        }

        [TestMethod]
        public void CreateUser_UserDoesntExist_ReturnFalse()
        {
            Assert.IsFalse(admin.CreateUser("test", 12000, "user2", "123"));
        }
    }
}