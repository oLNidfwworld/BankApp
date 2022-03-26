using BankApp.Services;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

using BankApp.Views;

namespace BankApp.ViewModels
{
    internal class LoginVM:BaseViewModel
    {

        public LoginVM()
        {
            LoginCommand = new Command(async () => await LoginCommandAsync());
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
                    //Preferences.Set("Login", Login);
                    var user = await usersrvs.GetUser(CardNumber);
                    //Preferences.Set("FullName", user.Object.Fullname);

                    await Shell.Current.Navigation.PushModalAsync(new LoginLastStepView(user));
                }
                else
                {
                    await Shell.Current.DisplayAlert("Error", "Invalid Login or Password", "OK");
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
