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

        /////filter-price range//gate options

        //swipe right if you want to buy ticket from the user


        //use students collection filter page

        private MyTicketServerClientApi service;


        private List<Ticket> _ticketList;
        private Ticket selectedTicket;
        public Ticket SelectedTicket { get=>selectedTicket; set { selectedTicket = value; OnPropertyChanged();((Command)ShowTicketsCommand).ChangeCanExecute(); } }//שם כרטיס להוספה
        public ObservableCollection<Ticket> TicketList;//אוסף כרטיסים
        public ICommand ClearTicketsCommand { get; private set; }  //ריקון הרשימה
        public ICommand LoadTicketsCommand { get; private set; }//טעינה
        public ICommand ShowTicketsCommand { get; private set; }//הוספת כרטיס

        public ICommand DeleteTicketCommand { get; private set; }//מחיקת כרטיס

        private bool isRefreshing;//רענון מסך
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }

        public BuyTicketViewModel(MyTicketServerClientApi s)
        {
            this.service = s;
            _ticketList = new List<Ticket>();
            TicketList = new ObservableCollection<Ticket>();


        }


    }
}
