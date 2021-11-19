namespace CI_Uppgift1
{
    /// <summary>
    /// Constructor of the User class.
    /// </summary>
    public class User : Interfaces.IAccount
    {
        public string Title { get; set; }
        public int Salary { get; set; }
        public string Username { get; set; }
        public string Password { get; set; }

        public bool IsAdmin { get; set; }

        public User()
        {

        }

        public User(string title, int salary, string username, string password,
            bool isadmin = false)
        {
            this.Title = title;
            this.Salary = salary;
            this.Username = username;
            this.Password = password;
            this.IsAdmin = isadmin;
        }
    }
}