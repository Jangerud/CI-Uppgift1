using System;
using System.IO;

namespace CI_Uppgift1
{
    public class Setup
    {
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
            } while (!successfullLogin);
        }
    }
}