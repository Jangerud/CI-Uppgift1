using System;
using System.IO;
using System.Text;
using System.Text.Json;
using System.Collections.Generic;

namespace CI_Uppgift1
{
    public class Logic
    {

        string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

        public void CreateUser(User userinfo)
        {
            try
            {
                // Create the file, or overwrite if the file exists.
                using (FileStream fs = File.Create($"{filePath}/{userinfo.Username}"))
                {
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

        public void GetUser(string user)
        {
            try
            {
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

        public void SerializeData()
        {
            var user = new User("test", 12, "user1", "123", false);
            string fileName = "test.json";
            string jsonString = JsonSerializer.Serialize(user);

            File.WriteAllText(fileName, jsonString);
            // System.Console.WriteLine(jsonString);
        }

        public User DeserializeData(string user)
        {
            User userInfo = new();
            string fileName = $"{user.ToLower()}";
            string jsonString = File.ReadAllText($"{fileName}.json");
            if (jsonString != "")
            {
                userInfo = JsonSerializer.Deserialize<User>(jsonString);
            }
            else
            {
                Console.WriteLine("User does not exist! Please try again.");
                Console.ReadKey();
            }

            Console.WriteLine($"{userInfo.Salary}, {userInfo.Username}");
            Console.ReadKey();

            return userInfo;
        }
    }
}