using BankApp.Models;
using BankApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Essentials;

namespace BankApp.ViewModels
{
    internal class MainVM:BaseViewModel
    {
        
        public MainVM()
        {
            Cards = new ObservableCollection<ClientsCardsModel>();
            Client = new ClientsModel();
            GetClientData();
            GetCards();
        }
        #region(Commands)

        #endregion
        #region(Props)

        public ObservableCollection<ClientsCardsModel> Cards { get; set; }

        private ClientsModel _Client;
        public ClientsModel Client
        {
            get { return _Client; }
            set { _Client = value; OnPropertyChanged(); }
        }
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
        #endregion
    }
}
