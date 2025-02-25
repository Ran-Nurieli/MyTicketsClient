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
        public object SelectedTicket { get=>selectedTicket; set { selectedTicket = value; OnPropertyChanged();((Command)ShowTicketsCommand).ChangeCanExecute(); } }//שם כרטיס להוספה
        private int selectedIndex {  get; set; }//מיקום הכרטיס ברשימה
        public ObservableCollection<int> Seats { get; set; } //מקומות

        public ObservableCollection<Ticket> TicketList;//אוסף כרטיסים
        public ICommand ClearTicketsCommand { get; private set; }  //ריקון הרשימה
        public ICommand LoadTicketsCommand { get; private set; }//טעינה
        public ICommand ShowTicketsCommand { get; private set; }//הוספת כרטיס

        public ICommand DeleteTicketCommand { get; private set; }//מחיקת כרטיס


        public ICommand FilterCommand { get; private set; }//סינון
        public ICommand ClearFilterCommand { get; private set; }    //ניקוי סינון


        private List<Ticket> fullist; // רשימת הכרטיסים

        public ObservableCollection<Ticket> Tickets { get; private set; }


        private bool isRefreshing;//רענון מסך
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }

        public BuyTicketViewModel(MyTicketServerClientApi s)
        {
            this.service = s;
            _ticketList = new List<Ticket>();
            TicketList = new ObservableCollection<Ticket>();
            Seats = new ObservableCollection<int>();

            //LoadTicketsCommand = new Command(async () => await LoadTickets());
            //ClearTicketsCommand = new Command();

        }

        private async Task LoadTickets()
        {
            fullist = await service.GetTickets();
            Tickets.Clear();
            foreach (var ticket in fullist)
            {
                Tickets.Add(ticket);
            }

            UpdateSeats();
            ((Command)ClearTicketsCommand).ChangeCanExecute();
            ((Command)LoadTicketsCommand).ChangeCanExecute();
            ((Command)ClearFilterCommand).ChangeCanExecute();

        }

        private void UpdateSeats()
        {
            Seats.Clear();
            var m = fullist.Select(x => x.Seats).Distinct().OrderBy(x => x);
            foreach(var x in m)
            {
                Seats.Add(x);
            }
            selectedIndex = -1;
        }


    }
}
