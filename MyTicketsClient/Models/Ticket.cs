using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicketsClient.Models
{
    public class Ticket
    {
        public int TicketId { get; set; }

        public int Price { get; set; }

        public int Gate { get; set; }

        public int Row { get; set; }

        public int Seats { get; set; }

        public int TeamId { get; set; }

        public Ticket() { }

        public Ticket(Models.Ticket ticket)
        {
            this.TicketId ++;
            this.Price = ticket.Price;
            this.Gate = ticket.Gate;
            this.Row = ticket.Row;
            this.Seats = ticket.Seats;
            this.TeamId = ticket.TeamId;
        }   
        public Ticket(int price,int Gate,int row,int seats,int teamId)
        {
            this.Price = price;
            this.Gate = Gate;
            this.Row = row;
            this.Seats = seats;
            this.TeamId = teamId;
        }
        public Models.Ticket ToModel()
        {
            return new Models.Ticket(this);
        }
    }
}
