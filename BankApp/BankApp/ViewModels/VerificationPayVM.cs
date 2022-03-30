using BankApp.Models;
using BankApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BankApp.ViewModels
{
    internal class VerificationPayVM:BaseViewModel
    {
        public VerificationPayVM(ClientsCardsModel card)
        {
            Card = new ClientsCardsModel()
            {
                Amount = card.Amount,
                Id = card.Id,
                BankId = card.BankId,
                CardNumber = card.CardNumber,
                ClientId = card.ClientId,
            };
            GetClient();
        }
        #region(Commands)
        #endregion
        #region(Props)
        private ClientsCardsModel _Card;
        public ClientsCardsModel Card { get { return _Card; } set { _Card = value; OnPropertyChanged(); } }
        private ClientsModel _Client;
        public ClientsModel Client { get { return _Client; } set { _Client = value; OnPropertyChanged(); } }


        private int _SendingAmount;
        public int SendingAmount { get { return _SendingAmount; } set { _SendingAmount = value; OnPropertyChanged(); } }
        #endregion
        #region(Functions)
        private async void GetClient()
        {
            var client = await new ClientService().GetUser(Card.CardNumber);
            Client = new ClientsModel()
            {
                FullName = client.Object.FullName,
                Id = client.Object.Id,
                Password = client.Object.Password
            };
            await Shell.Current.DisplayAlert("dasd", "dasd", "dasd");
        }
        #endregion
    }
}
