using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTicketsClient.Services;
using MyTicketsClient.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;
//using AVFoundation;


namespace MyTicketsClient.ViewModels
{
    public class AdminPageViewModel:ViewModelBase
    {

        private MyTicketServerClientApi proxy;

        private List<User> fullist;
        
        private UserDisp selectedUser;
        public UserDisp SelectedUser { get => selectedUser; set {  selectedUser = value; OnPropertyChanged(); } }

        public ObservableCollection<UserDisp> Users { get; set; }

        public ICommand DeleteUserCommand { get; private set; }
        public ICommand LoadUsersCommand { get; private set; }
       


        private bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }



        public AdminPageViewModel(MyTicketServerClientApi proxy)
        {
            this.proxy = proxy;
            fullist = new List<User>();
            Users = new ObservableCollection<UserDisp>();

            //DeleteUserCommand = new Command();
            LoadUsersCommand = new Command(LoadStudents);

        
        }


        private async void LoadStudents()
        {
            IsRefreshing = true;

            fullist = await proxy.GetUsers();
            Users.Clear();
            foreach (User user in fullist)
            {
                UserDisp u = new UserDisp();
                u.Username = user.Username;
                u.Email = user.Email;
                Users.Add(u);
            }

        }





    }


    public class UserDisp
    {
        public string Username { get; set; }
        public string Email { get; set; }
    }
}
