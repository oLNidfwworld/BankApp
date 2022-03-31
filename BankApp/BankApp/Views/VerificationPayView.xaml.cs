using BankApp.Models;
using BankApp.ViewModels;
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
    public partial class VerificationPayView : ContentPage
    {
        VerificationPayVM vm;
        public VerificationPayView(ClientsCardsModel card,ClientsModel client)
        {
            InitializeComponent();
            vm = new VerificationPayVM(card,client);
            
            this.BindingContext = vm;

        }
        public VerificationPayView()
        {
            InitializeComponent();
        }
    }
}