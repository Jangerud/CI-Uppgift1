using System;
using System.Collections.Generic;

namespace CI_Uppgift1
{
    class Program
    {
        static Logic logicClass = new();

        static void Main(string[] args)
        {
            string filePath = Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments);

            List<User> temp = new Logic().CreateDummyData();
            new Logic().SerializeData(temp, filePath + "/test.json");
            List<User> secondTemp = new Logic().DeserializeData(filePath + "/test.json");
            new Logic().CreateEmployeeList(secondTemp);
        }
    }
}
