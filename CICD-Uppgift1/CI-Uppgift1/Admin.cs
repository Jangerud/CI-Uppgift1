using System;
using System.Collections.Generic;
using CI_Uppgift1.Interfaces;

namespace CI_Uppgift1
{
    /// <summary>
    /// Constructor of the Admin class.
    /// </summary>
    public class Admin : Interfaces.IAccount
    {
        new Logic logic = new();

        string filePath = Environment.GetFolderPath(
            Environment.SpecialFolder.MyDocuments);

        public string Title { get; set; }
        public int Salary { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsAdmin { get; set; }


        public Admin()
        {

        }

        public Admin(string title, int salary, string username, string password,
            bool isadmin = true)
        {
            this.Title = title;
            this.Salary = salary;
            this.Username = username;
            this.Password = password;
            this.IsAdmin = isadmin;
        }


        /// <summary>
        /// Method used to create a new user and add it to the users file.
        /// </summary>
        /// <param name="title">Title of the user</param>
        /// <param name="salary">Salary of the user</param>
        /// <param name="username">Username of the user</param>
        /// <param name="password">Password of the user</param>
        /// <returns>True if the user already exists otherwise it will return
        /// false.</returns>
        public bool CreateUser(string title, int salary, string username,
            string password)
        {
            List<User> tmp = logic.DeserializeData(filePath + "/users.json");

            for (int i = 0; i < tmp.Count; i++)
            {
                if (tmp[i].Username == username)
                {
                    return true;
                }
            }
            tmp.Add(new User(title, salary, username, password));
            logic.SerializeData(tmp, filePath + "/users.json");
            return false;
        }
    }
}