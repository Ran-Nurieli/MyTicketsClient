﻿using System;
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
        private int? selectedGate;
        public int? SelectedGate { get => selectedGate; set { selectedGate = value; OnPropertyChanged(); } }

        private TicketDisp selectedTicket;
        public TicketDisp SelectedTicket { get => selectedTicket; set { selectedTicket = value; OnPropertyChanged();  } }//שם כרטיס להוספה


        public ObservableCollection<int> Gates { get; set; } //מקומות
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
            Gates = new ObservableCollection<int>();
            errorMessage = "not valid ticket";
            BuyTicketCommand = new Command(async () => await BuyTicket());
            LoadTicketsCommand = new Command(async () => await LoadTickets());
            ClearFilterCommand = new Command(ClearFilter);

            FilterCommand = new Command(async () =>
            {
                try
                {
                    if (selectedGate == null)
                    {
                        ClearFilter();
                        return;
                    }
                    var isSelectedGate = _ticketList.Where(x => x.Gate == selectedGate).ToList();
                    ticketsToDisp.Clear();
                    foreach (var ticket in isSelectedGate)
                    {
                        ticketsToDisp.Add(new TicketDisp(ticket.TicketId,ticket.Price, ticket.Gate, ticket.Seats, ticket.HomeTeam, ticket.AwayTeam));

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

            UpdateGate();
            ClearFilter();
            selectedGate = null;

        }

        private void UpdateGate()
        {
            Gates.Clear();
            var m = _ticketList.Select(x => x.Gate).Distinct().OrderBy(x => x);
            foreach (var x in m)
            {
                Gates.Add(x);
            }
            selectedGate = null;
        }
        private void ClearFilter()
        {
            ticketsToDisp.Clear();
            foreach (var ticket in _ticketList)
            {
                ticketsToDisp.Add(new TicketDisp(ticket.TicketId,ticket.Price, ticket.Gate, ticket.Seats, ticket.HomeTeam, ticket.AwayTeam));
            }
            OnPropertyChanged();
        }


    }

    public class TicketDisp
    {
        public int TicketId { get; set; }
        public int Price { get; set; }
        public int Gate { get; set; }
        public int Seats { get; set; }
        public string? HomeTeam { get; set; } = null!;
        public string? AwayTeam { get; set; } = null!;
        public string Description { get => $"Gate: {Gate},Seat: {Seats} Home Team: {HomeTeam} Away Team: {AwayTeam}"; }
        public string PriceDescription { get => $"Price: {Price}"; }
        public TicketDisp(int ticketId,int price, int Gate, int seats,string homeTeam,string awayTeam)
        {
            this.TicketId = ticketId;
            this.Price = price;
            this.Gate = Gate;
            this.Seats = seats;
            this.AwayTeam = awayTeam;
            this.HomeTeam = homeTeam;
        }
    }
}
