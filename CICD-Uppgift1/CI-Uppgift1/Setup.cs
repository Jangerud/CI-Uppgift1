using System;
using System.Collections.Generic;
using System.IO;
using CI_Uppgift1.Interfaces;

namespace CI_Uppgift1
{
    public class Setup
    {
        private IAccount _user;
        public void Start()
        {
            FirstTimeRun();
            LoginScreen();
            IsAdmin();
        }

        private void FirstTimeRun()
        {
            Logic logic = new();
            if (!File.Exists(logic.filePath + "/users.json"))
            {
                logic.SerializeData(
                    logic.CreateDummyData(), logic.filePath + "/users.json");
            }
        }

        private void LoginScreen()
        {
            Logic logic = new();
            List<User> tmp = logic.DeserializeData(logic.filePath + "/users.json");
            List<IAccount> users = new Logic().CreateEmployeeList(tmp);
            string username;
            string password;
            bool successfullLogin;

            do
            {
                Console.Clear();
                Console.WriteLine("Please login:");
                Console.Write("Username: ");
                username = Console.ReadLine();
                Console.Write("Password: ");
                password = Console.ReadLine();

                successfullLogin = new Logic().Login(username, password);

                if (!successfullLogin)
                {
                    Console.WriteLine("The username or password is not correct" +
                        ", please try again.");
                    Console.ReadKey();
                }
                else
                {
                    void GetUser()
                    {
                        for (int i = 0; i < users.Count; i++)
                        {
                            if (users[i].Username == username)
                            {
                                _user = users[i];
                                return;
                            }
                        }
                    }
                    GetUser();
                }
                System.Console.WriteLine(_user.Username);
            } while (!successfullLogin);
        }

        private void IsAdmin()
        {
            if (_user.IsAdmin)
            {
                AdminMenu();
            }
        }

        private void AdminMenu()
        {
            Admin admin = new();
            Logic logic = new();
            List<User> tmp = logic.DeserializeData(logic.filePath + "/users.json");
            List<IAccount> users = new Logic().CreateEmployeeList(tmp);
            bool active = true;
            int salary = 0;
            string title = "";
            for (int i = 0; i < users.Count; i++)
            {
                if (users[i].Username == _user.Username)
                {
                    salary = users[i].Salary;
                    title = users[i].Title;
                }
            }
            List<string> options = new List<string> { "Check Salary", "Check Role", "Check Active Users", "Create New User", "Remove Account", "Logout" };
            do
            {
                Menu menu = new(options);
                menu.CreateMenu();

                switch (menu.Choice)
                {
                    case 1:
                        Console.WriteLine($"Your current salary is: {salary}");
                        break;
                    case 2:
                        Console.WriteLine($"Your title in the company: {title}");
                        break;
                    case 3:
                        foreach (var item in users)
                        {
                            Console.WriteLine($"Username: {item.Username}");
                            Console.WriteLine($"Password: {item.Password}");
                            Console.WriteLine(" ");
                        }
                        break;
                    case 4:
                        Console.WriteLine("Type in the following:");
                        Console.WriteLine("Username: ");
                        string newUsername = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        string newPassword = Console.ReadLine();
                        Console.WriteLine("Title: ");
                        string newTitle = Console.ReadLine();
                        Console.WriteLine("Salary: ");
                        int.TryParse(Console.ReadLine(), out int newSalary);
                        admin.CreateUser(newTitle, newSalary, newUsername, newPassword);
                        break;
                    case 5:
                        Console.WriteLine("Type in the username and password of the account you want to remove:");
                        Console.WriteLine("Username: ");
                        string user = Console.ReadLine();
                        Console.WriteLine("Password: ");
                        string pass = Console.ReadLine();
                        if (logic.RemoveAccount(user, pass))
                        {
                            Console.WriteLine("User has been removed!");
                        }
                        Console.WriteLine("Please check if the username or password was correct!");
                        break;
                    case 6:
                        Console.WriteLine("You've been logged out!");
                        active = false;
                        break;
                }
            } while (active);
        }
    }
}