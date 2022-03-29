using BankApp.Models;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BankApp.Services
{
    internal class BankServices
    {
        FirebaseClient client;
        public BankServices()
        {
            client = new FirebaseClient("https://banksapp-48b9a-default-rtdb.firebaseio.com/");
        }

        public async Task<List<BanksModel>> GetBanksAsync()
        {
            var list = (await client.Child("Banks").OnceAsync<BanksModel>()).Select(f => new BanksModel 
            {
                Id = f.Object.Id,
                Name = f.Object.Name

            }).ToList();

            return list;
        }

    }
}
