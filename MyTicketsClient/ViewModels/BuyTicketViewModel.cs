using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTicketsClient.Views;
using MyTicketsClient.Services;
using System.Collections.ObjectModel;
using System.Windows.Input;
using MyTicketsClient.Models;

namespace MyTicketsClient.ViewModels
{
    public class BuyTicketViewModel:ViewModelBase
    {
        

        public ObservableCollection<Ticket> Gates;
        public object SelectedTicket;
        private int selectedIndex;
        public int SelectedIndex { get =>  selectedIndex; set { selectedIndex = value; OnPropertyChanged(); }  }

        public ICommand FilterCommand { get; private set; }


        private List<Ticket> ticketList;

        public ObservableCollection<Ticket> tickets;



    }
}
