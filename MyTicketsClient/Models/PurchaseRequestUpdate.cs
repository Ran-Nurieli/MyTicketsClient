using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicketsClient.Models
{
    public class PurchaseRequestUpdate
    {
        public int TicketId { get; set; }
        public bool IsAccepted { get; set; } = false;
    }
}
