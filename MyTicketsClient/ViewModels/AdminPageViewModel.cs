using MyTicketsClient.Models;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicketsClient.ViewModels
{
    public class AdminPageViewModel
    {

        public ObservableCollection<User> users;
        public object SelectedUser { get; set; }





    }
}
