using System;
using System.Collections.Generic;
using System.Text;
using Xamarin.Forms;

namespace BankApp.ViewModels
{
    internal class LoginVM:BaseViewModel
    {

        public LoginVM()
        {

        }
        #region
        public Command LoginCommand { get; set; }
        public Command RegNewCardCommand { get; set; }
        #endregion
        #region
        private string _CardNumber;
        public string CardNumber
        {
            get { return _CardNumber; }
            set { _CardNumber = value; }
        }
        #endregion

    }
}
