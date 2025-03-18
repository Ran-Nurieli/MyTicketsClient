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
using System.Linq.Expressions;

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

        private int selectedIndex {  get; set; }//מיקום הכרטיס ברשימה
        public int SelectedIndex { get => selectedIndex; set { selectedIndex = value; OnPropertyChanged(); } }

        public ObservableCollection<int> Place { get; set; } //מקומות

        public ObservableCollection<Ticket> TicketList;//אוסף כרטיסים
        public ICommand ClearTicketsCommand { get; private set; }  //ריקון הרשימה
        public ICommand LoadTicketsCommand { get; private set; }//טעינה
        public ICommand ShowTicketsCommand { get; private set; }//הוספת כרטיס

        public ICommand DeleteTicketCommand { get; private set; }//מחיקת כרטיס
        public ICommand FilterCommand { get; private set; }//סינון
        public ICommand ClearFilterCommand { get; private set; }    //ניקוי סינון

        public ICommand BuyTicketCommand { get; private set; }//קניית כרטיס



        private List<Ticket> fullist; // רשימת הכרטיסים

        public ObservableCollection<Ticket> Tickets { get; private set; }


        private bool isRefreshing;//רענון מסך
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }

        public BuyTicketViewModel(MyTicketServerClientApi s)
        {
            this.service = s;
            _ticketList = new List<Ticket>();
            TicketList = new ObservableCollection<Ticket>();
            Place = new ObservableCollection<int>();
            errorMessage = "not valid ticket";
            BuyTicketCommand = new Command(async () => await BuyTicket(), () => SelectedTicket != null);
            LoadTicketsCommand = new Command(async () => await LoadTickets());
            ClearTicketsCommand = new Command(ClearTickets, () => Tickets.Count > 0);

            FilterCommand = new Command(async () =>
            {
                try
                {
                    var isSelectedPlace = fullist.Where(x => x.Place == selectedTicket.Place).ToList();
                    Tickets.Clear();
                    foreach (var ticket in isSelectedPlace)
                    {
                        Tickets.Add(ticket);
                    }
                }
                catch (Exception ex)
                {
                    //binding error message
                    errorMessage = "not valid Gate";
                }
            }
                );
        }

        private async Task BuyTicket()
        {
            

        }

        private string errorMessage;
        public string ErrorMessage
        {
            get { return errorMessage; }
            set { errorMessage = value; OnPropertyChanged(); }
        }

        private bool showErrorMessage;
        public bool ShowErrorMessage
        {
            get => showErrorMessage; set { showErrorMessage = value; OnPropertyChanged(); }
        }



        private async Task LoadTickets()
        {
            fullist = await service.GetTickets();
            Tickets.Clear();
            foreach (var ticket in fullist)
            {
                Tickets.Add(ticket);
            }

            UpdatePlace();
            ((Command)ClearTicketsCommand).ChangeCanExecute();
            ((Command)LoadTicketsCommand).ChangeCanExecute();
            ((Command)ClearFilterCommand).ChangeCanExecute();

        }

        private void UpdatePlace()
        {
            Place.Clear();
            var m = fullist.Select(x => x.Seats).Distinct().OrderBy(x => x);
            foreach(var x in m)
            {
                Place.Add(x);
            }
            selectedIndex = -1;
        }
        private void ClearTickets()
        {
            Tickets.Clear();
            Place.Clear();
            fullist.Clear();

            ((Command)ClearTicketsCommand).ChangeCanExecute();
            ((Command)LoadTicketsCommand).ChangeCanExecute();
            ((Command)ClearFilterCommand).ChangeCanExecute();
        }


    }
}
