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

        public string Place { get; set; }

        public int Row { get; set; }

        public int Seats { get; set; }

        public int TeamId { get; set; }

        public Ticket() { }


    }
}
