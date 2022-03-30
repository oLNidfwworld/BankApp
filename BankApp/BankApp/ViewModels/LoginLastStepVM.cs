using BankApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace BankApp.ViewModels
{
    internal class LoginLastStepVM:BaseViewModel
    {

        public LoginLastStepVM()
        {
            if (Preferences.ContainsKey("Password"))
            {
                PasswordO = Preferences.Get("Password",0); 
            }
            GoToAppCommand = new Command(async () => await GoToAppAsync());
        }
        #region(Commands)
        public Command GoToAppCommand { get; set; }
        #endregion
        #region(Props)
        private int _PasswordO;
        public int PasswordO
        {
            get { return _PasswordO; }
            set {
                _PasswordO = value;
                OnPropertyChanged();
            }
        }

        private string _FullNameO;
        public string FullNameO
        {
            get { return _FullNameO; }
            set
            {
                _FullNameO = value;
                OnPropertyChanged();
            }
        }

        private int _Password;

        public int Password
        {
            get { return _Password; }
            set { _Password = value; }
        }

        #endregion
        #region(Functions)
        private async Task GoToAppAsync()
        {
            if(Password == PasswordO)
            {
                await Shell.Current.GoToAsync($"//{nameof(MainView)}");
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка", "Неправильный пароль", "ОК");
            }
        }

        #endregion
    }
}
