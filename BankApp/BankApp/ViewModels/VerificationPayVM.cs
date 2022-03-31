using BankApp.Models;
using BankApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BankApp.ViewModels
{
    internal class VerificationPayVM:BaseViewModel
    {
        public VerificationPayVM(ClientsCardsModel card,ClientsModel client)
        {
            Card = new ClientsCardsModel()
            {
                Amount = card.Amount,
                Id = card.Id,
                BankId = card.BankId,
                CardNumber = card.CardNumber,
                ClientId = card.ClientId,
            };
            Client = new ClientsModel()
            {
                FullName = client.FullName,
                Id = client.Id,
                Password = client.Password,
            };
            Cards = new ObservableCollection<ClientsCardsModel>();
            GetCards();
            SendMoneyCommand = new Command(async () => await SendMoneyAsync());
        }

        #region(Commands)
        public Command SendMoneyCommand { get; set; }
        #endregion
        #region(Props)
        private ClientsCardsModel _Card;
        public ClientsCardsModel Card { 
            get { return _Card; } 
            set { _Card = value; OnPropertyChanged(); } 
        }
        private ClientsModel _Client;
        public ClientsModel Client { 
            get { return _Client; } 
            set { _Client = value; OnPropertyChanged(); } 
        }


        private int _SendingAmount;
        public int SendingAmount { get { return _SendingAmount; } set { _SendingAmount = value; OnPropertyChanged(); } }

        public ObservableCollection<ClientsCardsModel> Cards { get; set; }
        private ClientsCardsModel _SelectedCard;
        public ClientsCardsModel SelectedCard
        {
            get { return _SelectedCard; }
            set { _SelectedCard = value; OnPropertyChanged(); }
        }
        #endregion
        #region(Functions)
        private async void GetCards()
        {

            int id = Preferences.Get("Id", 0);
            var data = (await new ClientsCardServices().GetCardByIdAsyncs(id));
            Cards.Clear();
            foreach (var item in data)
            {
                Cards.Add(item);
            }
        }

        private async Task SendMoneyAsync()
        {
            bool Result = await new ClientsCardServices().TransactionTo(SelectedCard.Id, Card.Id, SendingAmount,Preferences.Get("Id",0));
            if (Result)
            {
                await Shell.Current.DisplayAlert("Успешно", "Средства успешно отправлены", "Ok");
            }
            else
            {

                await Shell.Current.DisplayAlert("Ошибка", "Недостаточно средств", "Ok");
            }
        }
        #endregion
    }
}
