using Microsoft.VisualStudio.TestTools.UnitTesting;
using CI_Uppgift1;
using System.Collections.Generic;
<<<<<<< HEAD
=======
using System;
using System.IO;
>>>>>>> login

namespace CI_Uppgift1Test
{
    [TestClass]
    public class LoginTest
    {
<<<<<<< HEAD
        [TestMethod]
        public void Login_UserDoesNotExist_ReturnsFalse(){
            Assert.IsFalse(new Logic().Login("abc", "123"));
=======
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
        public void Login_UserDoesNotExist_ReturnsFalse(){
            Assert.IsFalse(logic.Login("abc", "123"));
>>>>>>> login
        }

        [TestMethod]
        public void Login_UserDoesExist_ReturnTrue(){
<<<<<<< HEAD
            Assert.IsTrue(new Logic().Login("user1", "123"));
=======
            Assert.IsTrue(logic.Login("user1", "123"));
>>>>>>> login
        }

        [TestMethod]
        public void Login_WrongPassword_ReturnFalse(){
<<<<<<< HEAD
            Assert.IsFalse(new Logic().Login("user1", "abc"));
=======
            Assert.IsFalse(logic.Login("user1", "abc"));
>>>>>>> login
        }

        [TestMethod]
        public void Login_CorrectPassword_ReturnTrue(){
<<<<<<< HEAD
            Assert.IsTrue(new Logic().Login("user1", "123"));
=======
            Assert.IsTrue(logic.Login("user1", "123"));
>>>>>>> login
        }
    }
}