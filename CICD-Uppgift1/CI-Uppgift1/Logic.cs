using System;
using System.IO;
using System.Text;

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
        /// This method will create a new user(text document) with the associated employees username. 
        /// </summary>
        /// <param name="userinfo">userinfo is all the relevant variables that the User class needs to be created.</param>
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
        /// This method is used to get the information for a specific user that is declared by the variable 'user'.
        /// </summary>
        /// <param name="user">String value that determines which users info should be returned.</param>
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
    }
}