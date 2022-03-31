using BankApp.Models;
using BankApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BankApp.ViewModels
{
    internal class RegistrationVM : BaseViewModel
    {



        public RegistrationVM()
        {
            BanksList = new ObservableCollection<BanksModel>();
            isChecked = true;
            GetBanks();
            EndRegistrationCommand = new Command(async () => await EndRegistrationAsync());
        }

        #region(Commands)

        public Command EndRegistrationCommand { get; set; }

        #endregion

        #region(Props)

        private string _FirstName;
        public string FirstName
        {
            get { return _FirstName; }
            set { _FirstName = value; OnPropertyChanged(); }
        }

        private string _LastName;
        public string LastName 
        { 
            get { return _LastName; } 
            set { _LastName = value; OnPropertyChanged(); } 
        }

        private string _ThirdName;
        public string ThirdName 
        { 
            get { return _ThirdName; } 
            set { _ThirdName = value; OnPropertyChanged(); } 
        }

        private int _Password;
        public int Password 
        {
            get { return _Password; }
            set { _Password = value; OnPropertyChanged(); }
        }

        private bool _isChecked;
        public bool isChecked
        {
            get { return _isChecked; } 
            set { _isChecked = value; OnPropertyChanged(); }    
        }
        
        public ObservableCollection<BanksModel> BanksList {get;set;}
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
      

        private async Task EndRegistrationAsync()
        {
            if (isChecked)
            {
                var reg =  new ClientService();
                if (!(Password <=999)) {

                     if(!(string.IsNullOrEmpty(LastName)|| string.IsNullOrEmpty(FirstName)|| string.IsNullOrEmpty(ThirdName) ||SelectedBank==null))
                    {
                        string fullname = $"{LastName} {FirstName} {ThirdName}";

                        string cardnum = "";
                        Random random = new Random();
                        for (int i = 0; i < 4; i++)
                        {
                            int quad = random.Next(1000, 9999);
                            cardnum += quad.ToString();
                        }
                        var client = new ClientsModel()
                        {
                            Id = new Random().Next(1, 1000000),
                            FullName = fullname,
                            Password = Password
                        };
                        var card = new ClientsCardsModel()
                        {

                            ClientId = client.Id,
                            CardNumber = cardnum,
                            Amount = 0,
                            BankId = SelectedBank.Id,
                            Id = new Random().Next(100000),
                        };

                        
                        
                        bool Result = await reg.RegisterUser(client);
                        if (Result)
                        {
                            await new ClientsCardServices().RegisterCard(card);
                            await Shell.Current.DisplayAlert("Успех", $"Пользователь успешно создан\nВаш номер карты {card.CardNumber}", "ОК");
                        }
                        else
                        {
                            await Shell.Current.DisplayAlert("Ошибка", "Пользователь уже существует", "OK");
                        }
                    }
                    else
                    {
                        await Shell.Current.DisplayAlert("Внимание", "Данные некорректны", "ОК");
                    }

                }
                else
                {
                    await Shell.Current.DisplayAlert("Внимание", "Пароль должен содержать 4 цифры", "ОК");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Внимание", "Для регистрации аккаунта вы должны подтвердить свое совершеннолетие", "ОК");
            }
        }

        #endregion
    }
}
