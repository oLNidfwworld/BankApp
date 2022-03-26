using BankApp.Models;
using BankApp.ViewModels;
using Firebase.Database;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace BankApp.Views
{
    [XamlCompilation(XamlCompilationOptions.Compile)]
    public partial class LoginLastStepView : ContentPage
    {
        LoginLastStepVM vm;
        public LoginLastStepView(FirebaseObject<ClientsModel> obj)
        {
            InitializeComponent();
            vm = new LoginLastStepVM();
            this.BindingContext = vm;
            vm.PasswordO = obj.Object.Password;
            vm.FullNameO = obj.Object.FullName;
        }

        public LoginLastStepView()
        {
            InitializeComponent();
        }
    }
}