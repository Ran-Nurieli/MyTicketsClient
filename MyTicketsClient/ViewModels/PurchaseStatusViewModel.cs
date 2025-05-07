using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MyTicketsClient.Models;
using MyTicketsClient.Services;
using MyTicketsClient.Views;

namespace MyTicketsClient.ViewModels
{
    public class PurchaseStatusViewModel:ViewModelBase
    {
        private MyTicketServerClientApi proxy;
        public Command AcceptedCommand { get; }
        public Command RejectedCommand { get; }
        
        public ObservableCollection<MyTicketDisp> tickets { get; set; }
        public PurchaseStatusViewModel(MyTicketServerClientApi proxy)
        {
            this.proxy = proxy;
            AcceptedCommand = new Command<MyTicketDisp>(OnAccepted);
            RejectedCommand = new Command<MyTicketDisp>(OnRejected);
            this.tickets = new ObservableCollection<MyTicketDisp>();
            Task.Run(async () => await UpdateTickets());
        }

        public async void OnAccepted(MyTicketDisp ticket)
        {
            await proxy.SetPurchaseStatus(ticket.TicketId,true);
            await UpdateTickets();
        }
        public async void OnRejected(MyTicketDisp ticket)
        {
            await proxy.SetPurchaseStatus(ticket.TicketId, false);
            await UpdateTickets();
        }
        public async Task UpdateTickets()
        {
            var tickets = await proxy.GetMyTickets();
            this.tickets = new ObservableCollection<MyTicketDisp>();
            foreach(var ticket in tickets)
            {
                this.tickets.Add(new MyTicketDisp(ticket.TicketId, ticket.Price, ticket.Gate, (int)ticket.Seats, ticket.BuyerUsername, ticket.BuyerPhone, ticket.IsAccepted, ticket.HomeTeam, ticket.AwayTeam));
            }
            OnPropertyChanged(nameof(tickets));
        }


       
    }
    public class MyTicketDisp
    {
        public int TicketId { get; set; }
        public int Price { get; set; }
        public int Gate { get; set; }
        public int Seats { get; set; }
        public string? HomeTeam { get; set; } = null!;
        public string? AwayTeam { get; set; } = null!;   
        public string Description { get => $"Gate: {Gate},Seat: {Seats} Home Team: {HomeTeam} Away Team: {AwayTeam}"; }
        public string PriceDescription { get => $"Price: {Price}"; }
        public bool IsSold { get; set; }
        public bool IsPurchasePending { get; set; }
        public string BuyerUesrname { get; set; }
        public string BuyerPhone { get; set; }
        public string BuyerDescription { get => $"Buyer: {BuyerUesrname}, Phone: {BuyerPhone}"; }

        public MyTicketDisp(int ticketId, int price, int Gate, int seats, string buyerUsername, string buyerPhone, bool isSold,string homeTeam,string awayTeam)
        {
            this.TicketId = ticketId;
            this.Price = price;
            this.Gate = Gate;
            this.Seats = seats;
            this.BuyerPhone = buyerPhone == null? "": buyerPhone;
            this.BuyerUesrname = buyerUsername == null? "": buyerUsername;
            this.IsSold = isSold;
            this.IsPurchasePending = !isSold && BuyerUesrname != "";
            this.HomeTeam = homeTeam;
            this.AwayTeam = awayTeam;
        }
    }
}
