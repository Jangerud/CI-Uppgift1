using System;
using System.Collections.Generic;
using System.IO;
using CI_Uppgift1.Interfaces;

namespace CI_Uppgift1
{
    public class Setup
    {
        /// <summary>
        /// Property to store a specific user.
        /// </summary>
        private IAccount _user;
        /// <summary>
        /// Property to store a list of users.
        /// </summary>
        private List<User> _users;

        /// <summary>
        /// Method that handles the program, it will first run FirstTimeRun to
        /// fill data and create the file. Then run the LoginScreen.
        /// Then depending on the users priviledges, it will start the
        /// corresponding menu.
        /// </summary>
        public void Start()
        {
            FirstTimeRun();
            LoginScreen();
            IsAdmin();
        }

        /// <summary>
        /// Creates dummy data and stores it in a new json file. If it is not
        /// the first time the users in the file will be added to the
        /// <paramref name="_users"/> list.
        /// </summary>
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

        /// <summary>
        /// Prints the login screen and lets the user login.
        /// </summary>
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
                                return;
                            }
                        }
                    }
                    GetUser();
                }
            } while (!successfullLogin);
        }

        /// <summary>
        /// If the account is a regular user, then this menu will be displayed.
        /// </summary>
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

        /// <summary>
        /// Prints the remove user menu to check if the user really wants to
        /// remove it's account.
        /// </summary>
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

        /// <summary>
        /// Removes the user.
        /// </summary>
        /// <param name="user"><c>user</c> is the user that should be removed.
        /// </param>
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

        /// <summary>
        /// Checks if the user is an admin and displays the propper menu.
        /// </summary>
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

        /// <summary>
        /// Displays the admin menu.
        /// </summary>
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
            List<string> options = new List<string> {
                "Check Salary", "Check Role", "Check Active Users",
                "Create New User", "Remove Account", "Logout" };
            do
            {
                Console.Clear();
                Menu menu = new(options);
                menu.CreateMenu();

                switch (menu.Choice)
                {
                    case 1:
                        Console.WriteLine($"Your current salary is: {salary}");
                        break;
                    case 2:
                        Console.WriteLine(
                            $"Your title in the company: {title}");
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
                        admin.CreateUser(
                            newTitle, newSalary, newUsername, newPassword);
                        break;
                    case 5:
                        Console.WriteLine("Type in the username and password " +
                            "of the account you want to remove:");
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
                        Console.WriteLine("Please check if the username " +
                            "or password was correct!");
                        break;
                    case 6:
                        Console.WriteLine("You've been logged out!");
                        active = false;
                        break;
                }
                Console.ReadKey();
            } while (active);
        }
    }
}