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
    internal class ReplenishVM:BaseViewModel
    {

        public ReplenishVM()
        {
            Cards = new ObservableCollection<ClientsCardsModel>();
            ReplenishCommand = new Command(async () => await ReplenishAsync());
            GetCards();
        }

        #region(Commands)
        public Command ReplenishCommand { get; set; }  
        #endregion
        #region(Props)
        public ObservableCollection<ClientsCardsModel> Cards { get; set; }

        private int _ReplenishAmount;
        public int ReplenishAmount
        {
            get { return _ReplenishAmount; }
            set { _ReplenishAmount = value; OnPropertyChanged(); }
        }

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


        private async Task ReplenishAsync()
        {
            bool Result = await new ClientsCardServices().ReplenishAsync(SelectedCard.Id, ReplenishAmount);
            if (Result)
            {
                await Shell.Current.DisplayAlert("Успешно", "Средства успешно пополнены", "Ok");
            }
        }
        #endregion
    }
}
