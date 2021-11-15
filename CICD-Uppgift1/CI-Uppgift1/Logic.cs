using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;
using CI_Uppgift1.Interfaces;

namespace CI_Uppgift1
{
    public class Logic
    {

        /// <summary>
        /// String variable that is made to keep the filepath for the employees that are created.
        /// </summary>
        /// <returns></returns>
        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        /// <summary>
        /// This method will create a json file with the users.
        /// </summary>
        /// <param name="userinfo">This contains the information about the user needed.</param>
        public void CreateUser(User userinfo)
        {
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create($"{filePath}/{userinfo.Username}"))
                {
                    // Declaring 'info' with all the user information from the variable 'userinfo'.
                    byte[] info = new UTF8Encoding(true).GetBytes($"{userinfo.Title}, {userinfo.Salary}, {userinfo.Username}, {userinfo.Password}, {userinfo.IsAdmin}");
                    // Add some information to the file.
                    fs.Write(info, 0, info.Length);

                    Console.WriteLine("User created successfully!");
                    Console.ReadKey();
                }
            }

            catch (Exception ex)
            {
                Console.WriteLine(ex.ToString());
            }

        }

        /// <summary>
        /// This method will get the information about the specific user.
        /// </summary>
        /// <param name="user">The string username of the user.</param>
        public void GetUser(string user)
        {
            try
            {
                // Checking which document to open, by putting together the filepath with the username.
                using (StreamReader sr = File.OpenText($"{filePath}/{user}"))
                {
                    string s = "";
                    while ((s = sr.ReadLine()) != null)
                    {
                        Console.WriteLine(s);
                    }
                    Console.ReadKey();
                }
            }
            catch (Exception e)
            {
                Console.WriteLine(e.ToString());
                Console.ReadKey();
            }

        }

        /// <summary>
        /// This method creates a json file with the users information.
        /// </summary>
        /// <param name="list">List of employees.</param>
        /// <param name="filepath">Filepath to the file.</param>
        public void SerializeData(List<User> list, string filepath)
        {
            var options = new JsonSerializerOptions { WriteIndented = true };
            string jsonString = JsonSerializer.Serialize(list, options);

            File.WriteAllText(filepath, jsonString);
        }

        /// <summary>
        /// DeserializeData(string user) makes it possible to read a json file of a user and returns the appropiate user information as an object.
        /// </summary>
        /// <param name="filepath">The filepath.</param>
        /// <returns>A List of employees.</returns>
        public List<User> DeserializeData(string filepath)
        {
            string jsonString = File.ReadAllText(filepath);
            var list = JsonSerializer.Deserialize<List<User>>(jsonString);

            return list;
        }


        /// <summary>
        /// Method that will create a list of employees.
        /// </summary>
        /// <param name="userslist">The list of users that needs to be added.</param>
        /// <returns>A list of employees.</returns>
        public List<IAccount> CreateEmployeeList(List<User> userslist)
        {
            List<IAccount> employeeList = new();
            foreach (var item in userslist)
            {
                if (item.IsAdmin)
                {
                    employeeList.Add(new Admin(item.Title, item.Salary, item.Username, item.Password, item.IsAdmin));
                }
                else
                {
                    employeeList.Add(new User(item.Title, item.Salary, item.Username, item.Password, item.IsAdmin));
                }

            }

            return employeeList;
        }


        /// <summary>
        /// Method that creates dummy data.
        /// </summary>
        /// <returns>Returns the list of dummy data.</returns>
        public List<User> CreateDummyData()
        {
            List<User> users = new() { new User("test", 12000, "user1", "123", false), new User("Admin", 22000, "admin", "admin123", true), new User("Admin", 22000, "administrator", "admin123", true) };

            return users;
        }

        /// <summary>
        /// Checks whether the user can log in or not.
        /// </summary>
        /// <param name="username"><c>username</c> is the username that the user
        /// entered.</param>
        /// <param name="password"><c>password</c> is the password that the user
        /// entered.</param>
        /// <returns>True if the username and password is correct. False if the
        /// username doesn't exist or the password is incorrect.</returns>
        public bool Login(string username, string password){
            List<User> tmp = DeserializeData(filePath + "/test.json");
            List<IAccount> users = CreateEmployeeList(tmp);
            IAccount user = null;

            bool UsernameExists(){
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Username == username){
                        user = users[i];
                        return true;
                    }
                }
                return false;
            }

            if (!UsernameExists()) return false;

            bool CheckPassword(){
                return user.Password == password;
            }

            if (!CheckPassword()) return false;
            return true;
        }

        /// <summary>
        /// Removes an account if it's not an admin account and the user exists
        /// and the password is correct-
        /// </summary>
        /// <param name="username"><c>username</c> is the username of the
        /// account that should be removed.</param>
        /// <param name="password"><c>password</c> is the password for the
        /// account that should be removed.</param>
        /// <returns>True if the account can be removed. False if the account is
        /// an admin account, the user does not exist or the password is wrong.
        /// </returns>
        public bool RemoveAccount(string username, string password){
            List<User> tmp = DeserializeData(filePath + "/test.json");
            List<IAccount> users = CreateEmployeeList(tmp);
            IAccount user = null;

            bool CheckUser(){
                for (int i = 0; i < users.Count; i++)
                {
                    if (users[i].Username == username &&
                        users[i].GetType() != typeof(Admin)){
                        user = users[i];
                        return true;
                    }
                }
                return false;
            }

            if(!CheckUser()) return false;

            if(user.Password != password) return false;
            return true;
        }
    }
}