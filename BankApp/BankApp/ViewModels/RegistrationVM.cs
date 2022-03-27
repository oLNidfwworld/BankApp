using System;
using System.Collections.Generic;
using System.Text;

namespace BankApp.ViewModels
{
    internal class RegistrationVM : BaseViewModel
    {



        public RegistrationVM()
        {

        }

        #region(Commands)

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

        #endregion

        #region(Functions)
        #endregion
    }
}
