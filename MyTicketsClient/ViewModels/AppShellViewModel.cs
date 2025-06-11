using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTicketsClient.Models;
using MyTicketsClient.Views;

namespace MyTicketsClient.ViewModels
{
    public class AppShellViewModel: ViewModelBase
    {
        private User? currentUser;
        private IServiceProvider serviceProvider;
        public AppShellViewModel(IServiceProvider serviceProvider)
        {
            this.serviceProvider = serviceProvider;
            this.currentUser = ((App)Application.Current).LoggedInUser;
        }

        public bool IsManager
        {
            get
            {
                if(this.currentUser == null)
                {
                    return false;
                }
                return this.currentUser.IsAdmin;
            }
        }

        public bool IsLoggedIn
        {
            get
            {
                return this.currentUser != null;
            }
        }
        public bool IsGuest
        {
            get
            {
                return this.currentUser == null;
            }
        }

        //this command will be used for logout menu item
        public Command LogoutCommand
        {
            get
            {
                return new Command(OnLogout);
            }
        }
        //this method will be trigger upon Logout button click
        public void OnLogout()
        {
            ((App)Application.Current).LoggedInUser = null;

            ((App)Application.Current).MainPage = new NavigationPage(serviceProvider.GetService<LoginPage>());
        }

        public void Refresh()
        {
            this.currentUser = ((App)Application.Current).LoggedInUser;
            OnPropertyChanged("IsLoggedIn");
            OnPropertyChanged("IsManager");
            OnPropertyChanged("IsGuest");
        }
    }
}
