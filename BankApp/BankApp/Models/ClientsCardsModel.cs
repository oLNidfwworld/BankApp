using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    internal class ClientsCardsModel
    {
        public int Amount { get; set; }
        public int BankId { get; set; }
        public int CardNumber { get; set; }
        public int ClientId { get; set; }
        public int Id { get; set; }
    }
}
