using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicketsClient.Models
{
    public class User
    {
        public string Username { get; set; }
        public string Password { get; set; }
        public int Age { get; set; }
        public string? Gender { get; set; }


        public User(Models.User user)
        {
            this.Username = Username;
            this.Password = Password;
            this.Age = Age;
            this.Gender = Gender;
        }
    }
}
