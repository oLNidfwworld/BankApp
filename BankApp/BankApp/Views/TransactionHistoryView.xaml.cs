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
    public partial class TransactionHistoryView : ContentPage
    {
        public TransactionHistoryView()
        {
            InitializeComponent();
        }

        private async void RefreshingOrb_Refreshing(object sender, EventArgs e)
        {
            await Task.Delay(2000);
            RefreshingOrb.IsRefreshing = false;
        }
    }
}