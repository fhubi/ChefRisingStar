using System;
using System.Diagnostics;

namespace ChefRisingStar.Models
{
    [DebuggerDisplay("{GetDebuggerDisplay}")]
    public class User
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Username { get; set; }
        public string EmailAddress { get; set; }
        public string SocialMediaAccount { get; set; }
        public string SocialMediaProvider { get; set; }
        public int SchoolId { get; set; }
        public int TeamID { get; set; }
        public DateTime JoinDate { get; set; }
        public DateTime LastLoginDate { get; set; }

        public string PreferredLanguage { get; set; }

        public bool IsAdmin { get; set; }
        public bool IsLocked { get; set; }

        public string DisplayName
        {
            get { return $"{Username} - {FirstName} {LastName}"; }
        }

        public User()
        {

        }

        public User(int id, string firstName, string lastName, string userName, string emailAddress)
        {
            Id = id;
            FirstName = firstName;
            LastName = lastName;
            Username = userName;
            EmailAddress = emailAddress;
            LastLoginDate = DateTime.Now;

            JoinDate = DateTime.Now.AddMonths(-4);

            SocialMediaAccount = string.Empty;
            SocialMediaProvider = string.Empty;
        }

        public override string ToString()
        {
            if (string.IsNullOrEmpty(EmailAddress))
                return Username;

            return $"{Username} : {EmailAddress}";
        }

        private string GetDebuggerDisplay()
        {
            return $"{Id} - {Username}";
        }
    }
}