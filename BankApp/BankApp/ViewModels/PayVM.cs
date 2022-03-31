using BankApp.Services;
using BankApp.Views;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;

namespace BankApp.ViewModels
{
    internal class PayVM:BaseViewModel
    {
        public PayVM()
        {
            GoToVerificationPayCommand = new Command(async () => await GoToVerificationPayAsync());
        }

        

        #region(Commands)
        public Command GoToVerificationPayCommand { get; set; }
        #endregion
        #region(Props)
        private string _CardNumber;
        public string CardNumber
        {
            get { return _CardNumber; }
            set
            {
                _CardNumber = value;
                OnPropertyChanged();
            }
        }
        #endregion
        #region(Functions)

        private async Task GoToVerificationPayAsync()
        {

            
            if (CardNumber.Length == 16 )
            {
                bool Result = await new ClientsCardServices().IsCardExists(CardNumber);
                if (Result)
                {
                    var card = await new ClientsCardServices().GetCardByCardNum(CardNumber);
                    var client = await new ClientService().GetUser(CardNumber);
                    await Shell.Current.Navigation.PushModalAsync(new VerificationPayView(card,client.Object ));
                }
                else
                {
                    await Shell.Current.DisplayAlert("Ошибка", "Такой карты не существует", "OK");
                }
            }
            else
            {
                await Shell.Current.DisplayAlert("Ошибка", "Некорректно введены данные", "OK");
            }
        }
        #endregion
    }
}
