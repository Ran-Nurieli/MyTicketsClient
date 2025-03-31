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
    public class BuyTicketViewModel : ViewModelBase
    {

        /////filter-price range//gate options

        //swipe right if you want to buy ticket from the user


        //use students collection filter page

        private MyTicketServerClientApi service;

        private List<Ticket> _ticketList;
        private ObservableCollection<TicketDisp> ticketsToDisp;
        public ObservableCollection<TicketDisp> TicketsToDisp { get => ticketsToDisp; }
        private string selectedPlace;
        public string SelectedPlace { get => selectedPlace; set { selectedPlace = value; OnPropertyChanged(); } }

        private TicketDisp selectedTicket;
        public TicketDisp SelectedTicket { get => selectedTicket; set { selectedTicket = value; OnPropertyChanged();  } }//שם כרטיס להוספה


        public ObservableCollection<string> Places { get; set; } //מקומות
        public ICommand ClearTicketsCommand { get; private set; }  //ריקון הרשימה
        public ICommand LoadTicketsCommand { get; private set; }//טעינה
        

        public ICommand FilterCommand { get; private set; }//סינון
        public ICommand ClearFilterCommand { get; private set; }    //ניקוי סינון

        public ICommand BuyTicketCommand { get; private set; }//קניית כרטיס





        private bool isRefreshing;//רענון מסך
        public bool IsRefreshing { get => isRefreshing; set { isRefreshing = value; OnPropertyChanged(); } }

        public BuyTicketViewModel(MyTicketServerClientApi s)
        {

            this.service = s;
            ticketsToDisp = new ObservableCollection<TicketDisp>();
            _ticketList = new List<Ticket>();
            Places = new ObservableCollection<string>();
            errorMessage = "not valid ticket";
            BuyTicketCommand = new Command(async () => await BuyTicket());
            LoadTicketsCommand = new Command(async () => await LoadTickets());
            ClearFilterCommand = new Command(ClearFilter);

            FilterCommand = new Command(async () =>
            {
                try
                {
                    if (selectedPlace == null)
                    {
                        ClearFilter();
                        return;
                    }
                    var isSelectedPlace = _ticketList.Where(x => x.Place == selectedPlace).ToList();
                    ticketsToDisp.Clear();
                    foreach (var ticket in isSelectedPlace)
                    {
                        ticketsToDisp.Add(new TicketDisp(ticket.TicketId,ticket.Price, ticket.Place, ticket.Seats));

                    }
                    OnPropertyChanged();
                }
                catch (Exception ex)
                {
                    //binding error message
                    errorMessage = "not valid Gate";
                }
            }
                );
            Task.Run(async () => await LoadTickets());
        }

        private async Task BuyTicket()
        {
            //implement buy ticket
            int ticketId = SelectedTicket.TicketId;
            var result = await service.BuyTicket(ticketId);
            if (result != null)
            {
                await App.Current.MainPage.DisplayAlert("Success", $"to confirm purchase contact- {result}", "OK");
                await LoadTickets();
            }
            else
            {
                await App.Current.MainPage.DisplayAlert("Error", "Failed to purchase ticket", "OK");
            }

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
            _ticketList = await service.GetTickets();
            ticketsToDisp.Clear();
            if(_ticketList == null)
            {
                _ticketList = new List<Ticket>();
            }

            UpdatePlace();
            ClearFilter();
            selectedPlace = null;

        }

        private void UpdatePlace()
        {
            Places.Clear();
            var m = _ticketList.Select(x => x.Place).Distinct().OrderBy(x => x);
            foreach (var x in m)
            {
                Places.Add(x);
            }
            selectedPlace = null;
        }
        private void ClearFilter()
        {
            ticketsToDisp.Clear();
            foreach (var ticket in _ticketList)
            {
                ticketsToDisp.Add(new TicketDisp(ticket.TicketId,ticket.Price, ticket.Place, ticket.Seats));
            }
            OnPropertyChanged();
        }


    }

    public class TicketDisp
    {
        public int TicketId { get; set; }
        public int Price { get; set; }
        public string Place { get; set; }
        public int Seats { get; set; }
        public string Description { get => $"Gate: {Place},Seat: {Seats}"; }
        public string PriceDescription { get => $"Price: {Price}"; }
        public TicketDisp(int ticketId,int price, string place, int seats)
        {
            this.TicketId = ticketId;
            this.Price = price;
            this.Place = place;
            this.Seats = seats;
        }
    }
}
