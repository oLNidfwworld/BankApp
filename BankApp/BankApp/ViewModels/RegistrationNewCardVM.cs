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
    internal class RegistrationNewCardVM:BaseViewModel
    {
        public RegistrationNewCardVM()
        {
            RegisterNewCardCommand = new Command(async () => await RegisterNewCardAsync());
            BanksList = new ObservableCollection<BanksModel>();
            GetBanks();
        }

        #region(Commands)
        public Command RegisterNewCardCommand { get; set; }
        #endregion
        #region(Props)
        public ObservableCollection<BanksModel> BanksList { get; set; }
        private BanksModel _SelectedBank;
        public BanksModel SelectedBank
        {
            get { return _SelectedBank; }
            set { _SelectedBank = value; OnPropertyChanged(); }
        }
        #endregion
        #region(Functions)
        private async void GetBanks()
        {
            var data = (await new BankServices().GetBanksAsync());
            BanksList.Clear();
            foreach (var item in data)
            {
                BanksList.Add(item);
            }
        }


        private async Task RegisterNewCardAsync()
        {

            string cardnum = "";
            Random random = new Random();
            for (int i = 0; i < 4; i++)
            {
                int quad = random.Next(1000, 9999);
                cardnum += quad.ToString();
            }

            ClientsCardsModel card = new ClientsCardsModel()
            {
                Amount = 0,
                BankId = SelectedBank.Id,
                Id = new Random().Next(0, int.MaxValue),
                CardNumber = cardnum,
                ClientId = Preferences.Get("Id", 0)
            };
            await new ClientsCardServices().RegisterCard(card);
            await Shell.Current.DisplayAlert("OK", $"Карта банка {SelectedBank.Name} с номером {cardnum} успешно создана", "Ok");
        }

        #endregion
    }
}
