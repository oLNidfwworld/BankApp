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


        public async Task<bool> IsUserExists(string fullname)
        {
            var user = (await client.Child("Clients").OnceAsync<ClientsModel>()).Where(u => u.Object.FullName == fullname).FirstOrDefault();
            return (user != null);
        }
        public async Task<bool> IsUserExists(int id)
        {
            var user = (await client.Child("Clients").OnceAsync<ClientsModel>()).Where(u => u.Object.Id == id).FirstOrDefault();
            return (user != null);
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

        public async Task<bool> RegisterUser(ClientsModel m)
        {
            if (await IsUserExists(m.FullName) == false)
            {
                await client.Child("Clients").PostAsync(m);
                return true;

            }
            else
            {
                return false;
            }
        }
        public async Task<FirebaseObject<ClientsModel>> GetUserByFullname(string fullname)
        {
            if (await IsUserExists(fullname) != false)
            {
                var model = (await client.Child("Clients").OnceAsync<ClientsModel>()).Where(u => u.Object.FullName == fullname).FirstOrDefault();
                return model;
            }
            else
            {
                return null;
            }
        }
        public async Task<FirebaseObject<ClientsModel>> GetUserById(int  id)
        {
            if (await IsUserExists(id) != false)
            {
                var model = (await client.Child("Clients").OnceAsync<ClientsModel>()).Where(u => u.Object.Id == id).FirstOrDefault();
                return model;
            }
            else
            {
                return null;
            }
        }

    }
}
