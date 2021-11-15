using System;
using System.Collections.Generic;
using System.IO;
using CI_Uppgift1.Interfaces;

namespace CI_Uppgift1
{
    public class Setup
    {
        private IAccount _user;
        public void Start(){
            FirstTimeRun();
            LoginScreen();
        }

        private void FirstTimeRun(){
            Logic logic = new();
            if(!File.Exists(logic.filePath + "/users.json")){
                logic.SerializeData(
                    logic.CreateDummyData(), logic.filePath + "/users.json");
            }
        }

        private void LoginScreen(){
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
                    Console.WriteLine("The username or password is not correct"+
                        ", please try again.");
                    Console.ReadKey();
                }
                else
                {
                    void GetUser(){
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
    }
}