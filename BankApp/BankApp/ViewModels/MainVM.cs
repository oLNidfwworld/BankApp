using BankApp.Models;
using BankApp.Services;
using BankApp.Views;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BankApp.ViewModels
{
    internal class MainVM:BaseViewModel
    {
        
        public MainVM()
        {
            Cards = new ObservableCollection<ClientsCardsModel>();
            GetClientData();
            GetCards();
            GoToPayCommand = new Command(async () => await  GoToPayAssync());
            GoToCreateCardCommand = new Command(async () => await  GoToCreateCardAsync());
            RefreshingCommand = new Command( () => RefreshingAsync());
            
        }

        
        #region(Commands)

        public Command GoToPayCommand {get; set; }
        public Command GoToCreateCardCommand { get; set; }
        public Command RefreshingCommand { get; set; }
        #endregion
        #region(Props)

        public ObservableCollection<ClientsCardsModel> Cards { get; set; }

        private ClientsModel _Client;
        public ClientsModel Client
        {
            get { return _Client; }
            set { _Client = value; OnPropertyChanged(); }
        }
        private string _Fullname;
        public  string Fullname { get { return _Fullname;} set { _Fullname = value; OnPropertyChanged(); } }
        #endregion
        #region(Functions)
        private async void GetClientData()
        {
            int id = Preferences.Get("Id", 0);
            var data = (await new ClientService().GetUserById(id));
            Client = new ClientsModel()
            {
                Id = data.Object.Id,
                FullName = data.Object.FullName,
                Password = data.Object.Password
            };
            
        }

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

        private void RefreshingAsync()
        {
            Cards.Clear();
            GetCards();
            GetClientData();
        }

        private async Task GoToPayAssync()
        {
            await Shell.Current.Navigation.PushModalAsync(new PayView());
        }
        private async Task GoToCreateCardAsync()
        {
            await Shell.Current.Navigation.PushModalAsync(new RegisterNewCardView());
        }

        #endregion
    }
}
