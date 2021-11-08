namespace CI_Uppgift1
{
    public class Admin : Interfaces.IAccount
    {
        public string Title { get; set; }
        public int Salary { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public Admin(string title, int salary, string username, string password, bool isadmin)
        {
            this.Title = title;
            this.Salary = salary;
            this.Username = username;
            this.Password = password;
            this.IsAdmin = isadmin;
        }
    }
}