using System;
using System.Collections.Generic;
using System.IO;
using CI_Uppgift1.Interfaces;

namespace CI_Uppgift1
{
    public class Setup
    {
        private IAccount _user;
        private List<User> _users;
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
                _users = new Logic().DeserializeData(new Logic().filePath +
                    "/users.json");
                return;
            }
            _users = new Logic().DeserializeData(new Logic().filePath +
                "/users.json");
        }

        private void LoginScreen()
        {
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
                    Console.WriteLine("The username or password is not correct"
                        + ", please try again.");
                    Console.ReadKey();
                }
                else
                {
                    void GetUser()
                    {
                        for (int i = 0; i < _users.Count; i++)
                        {
                            if (_users[i].Username == username)
                            {
                                _user = _users[i];
                                Console.ReadKey();
                                return;
                            }
                        }
                    }
                    GetUser();
                }
            } while (!successfullLogin);
        }

        private void UserMenu()
        {
            List<string> menuOptions = new()
            {
                "See current salary.",
                "See role in the company",
                "Remove account"
            };

            Console.Clear();
            Menu menu = new(menuOptions);
            menu.CreateMenu();

            switch (menu.Choice)
            {
                case 1:
                    Console.WriteLine($"Current salary: {_user.Salary}");
                    Console.ReadKey();
                    UserMenu();
                    break;
                case 2:
                    Console.WriteLine($"Role: {_user.Title}");
                    Console.ReadKey();
                    UserMenu();
                    break;
                case 3:
                    RemoveUserMenu();
                    break;
                default:
                    break;
            }
        }

        private void RemoveUserMenu()
        {
            List<string> menuOptions = new() { "yes", "no" };
            Console.Clear();
            Console.WriteLine("Do you really want to remove this account?");
            Menu menu = new(menuOptions);
            menu.CreateMenu();

            switch (menu.Choice)
            {
                case 1:
                    RemoveUser(_user);
                    Start();
                    break;
                case 2:
                    UserMenu();
                    break;
                default:
                    break;
            }
        }

        private void RemoveUser(IAccount user)
        {
            bool isRemoveable = new Logic().RemoveAccount(
                user.Username, user.Password);
            if (isRemoveable)
            {
                _users.Remove((User)user);
                new Logic().SerializeData(
                    _users, new Logic().filePath + "/users.json");
            }
            Console.WriteLine("Account successfully removed.");
            Console.ReadKey();
        }
        private void IsAdmin()
        {
            if (_user.IsAdmin)
            {
                AdminMenu();
            }
            else
            {
                UserMenu();
            }
        }

        private void AdminMenu()
        {
            Admin admin = new();
            bool active = true;
            int salary = 0;
            string title = "";
            for (int i = 0; i < _users.Count; i++)
            {
                if (_users[i].Username == _user.Username)
                {
                    salary = _users[i].Salary;
                    title = _users[i].Title;
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
                        foreach (var item in _users)
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
                        if (new Logic().RemoveAccount(user, pass))
                        {
                            IAccount userToRemove;
                            for (int i = 0; i < _users.Count; i++)
                            {
                                if (_users[i].Username == user)
                                {
                                    userToRemove = _users[i];
                                    Console.ReadKey();
                                    RemoveUser(userToRemove);
                                    return;
                                }
                            }
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