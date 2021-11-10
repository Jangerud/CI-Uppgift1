using System;

namespace CI_Uppgift1
{
    class Program
    {
        static Logic logicClass = new();

        static void Main(string[] args)
        {
            Admin admin = new Admin("Admin", 30000, "admin1", "admin1234", true);
            User jangen = new User("Peon", 20000, "Johan", "Johan123", false);
            //logicClass.CreateUser(jangen);
            //logicClass.CreateUser(admin);
            //logicClass.GetUser(jangen.Username);
        }
    }
}
