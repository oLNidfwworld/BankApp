using BankApp.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace BankApp.Services
{
    public class TransactionsService
    {
        FirebaseClient client;
        public TransactionsService()
        {
            client = new FirebaseClient("https://banksapp-48b9a-default-rtdb.firebaseio.com/");
        }

        public async Task<List<TransactionsModel>> GetTransactionAsync()
        {
            var products = (await client.Child("Transactions").OnceAsync<TransactionsModel>()).Select(f => new TransactionsModel
            {
                Id = f.Object.Id,
                Amount = f.Object.Amount,
                CardFrom = f.Object.CardFrom,
                CardTo = f.Object.CardTo,
                ClientFromId = f.Object.ClientFromId,
                HasComission = f.Object.HasComission,
                Time = f.Object.Time
            }).ToList();

            return products;
        }

        public async Task<ObservableCollection<TransactionsModel>> GetTransactionByCurrent()
        {
            var transactions = new ObservableCollection<TransactionsModel>();
            var list = (await GetTransactionAsync()).Where(p => p.ClientFromId == Preferences.Get("Id",0)).ToList();
            foreach (var item in list)
            {
                transactions.Add(item);
            }
            return transactions;
        }
    }
}
