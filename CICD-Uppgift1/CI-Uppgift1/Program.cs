using System;
using System.Collections.Generic;

namespace CI_Uppgift1
{
    class Program
    {
        static Logic logicClass = new();

        static void Main(string[] args)
        {
            List<User> temp = new Logic().CreateDummyData();
            new Logic().SerializeData(temp, "testing.json");
            List<User> secondTemp = new Logic().DeserializeData("testing.json");
            new Logic().CreateEmployeeList(secondTemp);
        }
    }
}
