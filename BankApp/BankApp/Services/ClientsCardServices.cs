using BankApp.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services
{
    internal class ClientsCardServices
    {
        FirebaseClient client;
        public ClientsCardServices()
        {
            client = new FirebaseClient("https://banksapp-48b9a-default-rtdb.firebaseio.com/");
        }


        public async Task<bool> RegisterCard(ClientsCardsModel m)
        {
            await client.Child("ClientsCards").PostAsync(m);
                return true;
        }


        public async Task<ObservableCollection<ClientsCardsModel>> GetCardByIdAsyncs(int Id)
        {
            var cards = new ObservableCollection<ClientsCardsModel>();
            var items = (await client.Child("ClientsCards").OnceAsync<ClientsCardsModel>()).Where(p => p.Object.ClientId == Id);
            foreach (var item in items)
            {
                cards.Add(new ClientsCardsModel()
                {
                     Amount = item.Object.Amount,
                      BankId = item.Object.BankId,
                       ClientId = item.Object.ClientId,
                          CardNumber = item.Object.CardNumber,
                           Id =  item.Object.Id
                });
            }
            return cards;
        }
    }
}
