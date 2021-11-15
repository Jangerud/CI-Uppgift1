using Microsoft.VisualStudio.TestTools.UnitTesting;
using CI_Uppgift1;
using System.Collections.Generic;
using System;
using System.IO;

namespace CI_Uppgift1Test
{
    [TestClass]
    public class LoginTest
    {
        private Logic logic;
        private string filePath;

        [TestInitialize]
        public void TestInitialize(){
            this.logic = new Logic();
            this.filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);
            logic.SerializeData(logic.CreateDummyData(), filePath + "/users.json");
        }

        [TestCleanup]
        public void TestCleanup(){
            this.logic = null;
            File.Delete(filePath + "/users.json");
        }

        [TestMethod]
        public void Login_UserDoesNotExist_ReturnsFalse(){
            Assert.IsFalse(logic.Login("abc", "123"));
        }

        [TestMethod]
        public void Login_UserDoesExist_ReturnTrue(){
            Assert.IsTrue(logic.Login("user1", "123"));
        }

        [TestMethod]
        public void Login_WrongPassword_ReturnFalse(){
            Assert.IsFalse(logic.Login("user1", "abc"));
        }

        [TestMethod]
        public void Login_CorrectPassword_ReturnTrue(){
            Assert.IsTrue(logic.Login("user1", "123"));
        }
    }
}