using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyTicketsClient.Models
{
    public class FeedBack
    {
        public int FeedBackId { get; set; }

        public int FeedBackType { get; set; }

        public string Info { get; set; }

        public FeedBack() { }
    }
}
