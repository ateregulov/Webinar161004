using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace WebApplication1.Models
{
    public class Message
    {
        public int MessageId { get; set; }

        public string SenderId { get; set; }
        public ApplicationUser Sender { get; set; }

        public string RecipientId { get; set; }
        public ApplicationUser Recipient { get; set; }

        public string Text { get; set; }
        public DateTime Datetime { get; set; }

    }
}
