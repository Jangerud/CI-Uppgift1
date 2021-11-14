using Microsoft.VisualStudio.TestTools.UnitTesting;
using CI_Uppgift1;
using System.Collections.Generic;

namespace CI_Uppgift1Test
{
    [TestClass]
    public class LoginTest
    {
        [TestMethod]
        public void Login_UserDoesNotExist_ReturnsFalse(){
            Assert.IsFalse(new Logic().Login("abc", "123"));
        }

        [TestMethod]
        public void Login_UserDoesExist_ReturnTrue(){
            Assert.IsTrue(new Logic().Login("user1", "123"));
        }

        [TestMethod]
        public void Login_WrongPassword_ReturnFalse(){
            Assert.IsFalse(new Logic().Login("user1", "abc"));
        }

        [TestMethod]
        public void Login_CorrectPassword_ReturnTrue(){
            Assert.IsTrue(new Logic().Login("user1", "123"));
        }
    }
}