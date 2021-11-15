using System;
using System.IO;
using CI_Uppgift1;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CI_Uppgift1Test
{
    [TestClass]
    public class RemoveAccountTest
    {
        private Logic logic;
        private string filePath;

        [TestInitialize]
        public void TestInitialize(){
            this.logic = new Logic();
            this.filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            logic.SerializeData(logic.CreateDummyData(), filePath + "/test.json");
        }

        [TestCleanup]
        public void TestCleanup(){
            this.logic = null;
            File.Delete(filePath + "/test.json");
        }

        [TestMethod]
        public void RemoveAccount_AccountDoesNotExist_ReturnFalse(){
            Assert.IsFalse(logic.RemoveAccount("userDoesNotExist", "123"));
        }

        [TestMethod]
        public void RemoveAccount_AccountIsAdmin_ReturnFalse(){
            Assert.IsFalse(logic.RemoveAccount("admin", "123"));
        }

        [TestMethod]
        public void RemoveAccount_WrongPassword_ReturnFalse(){
            Assert.IsFalse(logic.RemoveAccount("user1", "wrongPassword"));
        }

        [TestMethod]
        public void RemoveAccount_CorrectUsernameAndPassword_ReturnTrue(){
            Assert.IsTrue(logic.RemoveAccount("user1", "123"));
        }

        [TestMethod]
        public void RemoveAccount_RemoveAdminAccount_ReturnFalse(){
            Assert.IsFalse(logic.RemoveAccount("admin", "123"));
        }
    }
}