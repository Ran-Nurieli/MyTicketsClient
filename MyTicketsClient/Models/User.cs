//using Android.Text;
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
        public string Phone { get; set; }   
        public string Email { get; set; }   
        public int Age { get; set; }
        public string? Gender { get; set; }

        public bool IsAdmin { get; set; }

        public int? FavoriteTeamId { get; set; } = null!;


        public User() { }

        public User(Models.User user)
        {
            this.Username = Username;
            this.Password = Password;
            this.Phone = Phone;
            this.Email = Email;
            this.Age = Age;
            this.Gender = Gender;
            this.FavoriteTeamId = FavoriteTeamId;
            IsAdmin = false;
        }
    }
}
