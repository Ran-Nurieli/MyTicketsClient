using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicketsClient.Models
{
    public class Team
    {
        public int TeamId { get; set; }

        public int Capacity { get; set; }

        public string TeamName { get; set; }

        public string TeamCity { get; set; }

        public int PriceForTicket { get; set; }

        public Team() { }

        public override string ToString()
        {
            return $"{TeamName} {TeamCity}";
        }
    }
}
