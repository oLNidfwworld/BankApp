using BankApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using BankApp.Views;
using Xamarin.Essentials;

namespace BankApp.ViewModels
{
    internal class LoginVM:BaseViewModel
    {

        public LoginVM()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
            RegNewCardCommand = new Command(async () => await RegNewCardAsync());
        }

        
        #region(Commands)
        public Command LoginCommand { get; set; }
        public Command RegNewCardCommand { get; set; }
        #endregion
        #region(Props)
        private string _CardNumber;
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; 
                OnPropertyChanged(); }
        }

        private bool _Isbusy;
        public bool Isbusy
        {
            get { return _Isbusy; }
            set { _Isbusy = value;
                OnPropertyChanged();
            }
        }

        private bool _Result;
        public bool Result
        {
            get { return _Result; }
            set { _Result = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region(Functions)

        private async Task RegNewCardAsync()
        {
            await Shell.Current.Navigation.PushModalAsync(new RegistrationView()); 
        }

        private async Task LoginCommandAsync()

        {
            if (Isbusy)
                return;
            try
            {
                Isbusy = true;
                var usersrvs = new ClientService();
                Result = await usersrvs.LoginByCard(CardNumber);
                if (Result)
                {
                    var user = await usersrvs.GetUser(CardNumber);
                    Preferences.Set("Password", user.Object.Password);
                    Preferences.Set("Id", user.Object.Id);

                    await Shell.Current.Navigation.PushModalAsync(new LoginLastStepView());
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Неправильный номер карты", "OK");
                }
            }
            catch (Exception e)
            {
                await Shell.Current.DisplayAlert("Error", e.Message, "Ok");
            }
            finally
            {
                Isbusy = false;
            }
        }
        #endregion 
    }
}
