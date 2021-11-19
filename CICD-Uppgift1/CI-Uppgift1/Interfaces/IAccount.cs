namespace CI_Uppgift1.Interfaces
{
    public interface IAccount
    {
        /// <summary>
        /// Property for the salary of the user.
        /// </summary>
        /// <value>Can be set aswell as gotten when called upon.</value>
        public int Salary { get; set; }

        /// <summary>
        /// Property for the title of the user.
        /// </summary>
        /// <value>Can be set aswell as gotten when called upon.</value>
        public string Title { get; set; }

        /// <summary>
        /// Property for the username of the user.
        /// </summary>
        /// <value>Can be set aswell as gotten when called upon.</value>
        public string Username { get; set; }

        /// <summary>
        /// Property for the password of the user.
        /// </summary>
        /// <value>Can be set aswell as gotten when called upon.</value>
        public string Password { get; set; }

        /// <summary>
        /// Property for the user regarding if they are an admin or not.
        /// </summary>
        /// <value>Can be set aswell as gotten when called upon.</value>
        public bool IsAdmin { get; set; }

    }
}