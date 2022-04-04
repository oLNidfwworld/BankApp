using BankApp.Models;
using Firebase.Database;
using Firebase.Database.Query;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BankApp.Services
{
    internal class ClientsCardServices
    {
        FirebaseClient client;
        public ClientsCardServices()
        {
            client = new FirebaseClient("https://banksapp-48b9a-default-rtdb.firebaseio.com/");
        }

        public async Task<bool> IsCardExists(string cardnum)
        {
            var user = (await client.Child("ClientsCards").OnceAsync<ClientsCardsModel>()).Where(u => u.Object.CardNumber == cardnum).FirstOrDefault();
            return (user != null);
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

        public async Task<ClientsCardsModel> GetCardByCardNum(string cardnum)
        {
            var card = (await client.Child("ClientsCards").OnceAsync<ClientsCardsModel>()).Where(p => p.Object.CardNumber == cardnum).FirstOrDefault().Object;
           
            return card;
        }

        public async Task<FirebaseObject<ClientsCardsModel>> GetCardById(int id)
        {
            var card = (await client.Child("ClientsCards").OnceAsync<ClientsCardsModel>()).Where(p => p.Object.Id == id).FirstOrDefault();

            return card;
        }

        public async Task<bool> TransactionTo(int idFrom,int idTo,int amount,int clientFromId)
        {
            var cardFrom = await GetCardById(idFrom);
            var cardTo = await GetCardById(idTo);
            bool hasComission = false;
            bool action = false;
            if (cardFrom.Object.BankId != cardTo.Object.BankId)
            {
                amount = amount + Convert.ToInt32(amount * 0.13);
                hasComission = true;
            }
            if (hasComission)
            {
                action = await Shell.Current.DisplayAlert("Внимание", $"Данная операция будет взиматься с комиссией. Сумма к оплате {amount}. Продолжить?", "Да", "Нет");
            }
            if (action)
            {
                if (cardFrom.Object.Amount >= amount)
                {

                    cardFrom.Object.Amount = cardFrom.Object.Amount - amount;
                    cardTo.Object.Amount = cardTo.Object.Amount + amount;

                    await client.Child("ClientsCards").Child(cardFrom.Key).PutAsync(cardFrom.Object);
                    await client.Child("ClientsCards").Child(cardTo.Key).PutAsync(cardTo.Object);

                    await client.Child("Transactions").PostAsync(new TransactionsModel()
                    {
                        Id = new Random().Next(0, int.MaxValue),
                        Amount = amount,
                        HasComission = hasComission,
                        CardFrom = cardFrom.Object.CardNumber,
                        CardTo = cardTo.Object.CardNumber,
                        ClientFromId = clientFromId,
                        Time = DateTime.Now.ToString()
                    });

                    return true;
                }
                else
                {
                    return false;
                }
            }
            else
            {
                return false;
            }
        }

        public async Task<bool> ReplenishAsync(int idCard,int amount)
        {
            var card = await GetCardById(idCard);
            card.Object.Amount = card.Object.Amount + amount;
            await client.Child("ClientsCards").Child(card.Key).PutAsync(card.Object);
            return true;
        }

    }
}
