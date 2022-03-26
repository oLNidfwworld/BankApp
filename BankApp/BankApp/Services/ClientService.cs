using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Firebase.Database;
using System.Linq;
using Firebase.Database.Query;
using BankApp.Models;


namespace BankApp.Services
{
  
    internal class ClientService
    {
        FirebaseClient client;
        public ClientService()
        {
            client = new FirebaseClient("https://banksapp-48b9a-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> LoginByCard(string cardnum)
        {
            var user = (await client.Child("ClientsCards").OnceAsync<ClientsCardsModel>()).Where(u => u.Object.CardNumber ==cardnum).FirstOrDefault();
            return (user != null);
            
        }

        public async Task<FirebaseObject<ClientsModel>> GetUser(string cardnum)
        {
            
            var cardm = (await client.Child("ClientsCards").OnceAsync<ClientsCardsModel>()).Where(u => string.Equals(u.Object.CardNumber, cardnum)).FirstOrDefault();
            var userm = (await client.Child("Clients").OnceAsync<ClientsModel>()).Where(u => u.Object.Id == cardm.Object.ClientId).FirstOrDefault();
            return userm;
        }
    }
}
