using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.Models
{
    public class TransactionsModel
    {
        public int Id { get; set; }
        public int ClientFromId { get; set; }   
        public string CardFrom { get; set; }
        public string CardTo { get; set; }
        public int Amount { get; set; }
        public string Time { get; set; }    
        public bool HasComission { get; set; }
    }
}
