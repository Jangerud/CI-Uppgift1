using System;
using System.Collections.Generic;
using System.IO;
using CI_Uppgift1.Interfaces;

namespace CI_Uppgift1
{
    public class Setup
    {
        private IAccount _user;
        private readonly List<User> _users = new Logic().DeserializeData(
            new Logic().filePath + "/users.json");
        public void Start()
        {
            FirstTimeRun();
            LoginScreen();
            UserMenu();
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
                    RemoveUser();
                    Start();
                    break;
                case 2:
                    UserMenu();
                    break;
                default:
                    break;
            }
        }

        private void RemoveUser()
        {
            bool isRemoveable = new Logic().RemoveAccount(
                _user.Username, _user.Password);
            if (isRemoveable)
            {
                _users.Remove((User)_user);
                new Logic().SerializeData(
                    _users, new Logic().filePath + "/users.json");
            }
            System.Console.WriteLine(_user.Username, _user.Password);
            Console.WriteLine("Account successfully removed.");
            Console.ReadKey();
        }
    }
}