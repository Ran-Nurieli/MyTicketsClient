using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTicketsClient.Services;
using MyTicketsClient.Models;
using System.Collections.ObjectModel;
using System.Windows.Input;


namespace MyTicketsClient.ViewModels
{
    public class AdminPageViewModel:ViewModelBase
    {

        private MyTicketServerClientApi proxy;

        private List<User> fullist;
        
        private User selectedUser;
        public User SelectedUser { get => selectedUser; set {  selectedUser = value; OnPropertyChanged(); } }

        public ObservableCollection<User> Users { get; set; }

        public ICommand DeleteUserCommand { get; private set; }
        public ICommand LoadUsersCommand { get; private set; }
       


        private bool isRefreshing;
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }



        public AdminPageViewModel(MyTicketServerClientApi proxy)
        {
            this.proxy = proxy;
            fullist = new List<User>();
            Users = new ObservableCollection<User>();

            DeleteUserCommand = new Command((object obj) => { User u = (User)obj; Users.Remove(u); fullist.Remove(u); OnPropertyChanged(); });
            LoadUsersCommand = new Command(async () => await LoadStudents());

        
        }



        private async Task LoadStudents()
        {
            IsRefreshing = true;

            fullist = await proxy.GetUsers();
            Users.Clear();
            foreach (User user in fullist)
            {
                Users.Add(user);
            }

        }






    }
}
