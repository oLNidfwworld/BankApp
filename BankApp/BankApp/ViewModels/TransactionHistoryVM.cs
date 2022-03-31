using BankApp.Models;
using BankApp.Services;
using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;
using Xamarin.Forms;

namespace BankApp.ViewModels
{
    internal class TransactionHistoryVM:BaseViewModel
    {
        public TransactionHistoryVM()
        {
            Transactions = new ObservableCollection<TransactionsModel>();
            GetTransactions();
            RereshingCommand = new Command(() => RefreshingFunc());
        }

       
        #region(Commands)
        public Command RereshingCommand { get; set; } 
        #endregion
        #region(Props)
        public ObservableCollection<TransactionsModel> Transactions { get; set; }
        #endregion
        #region(Functions)

        public async void GetTransactions()
        {
            var transactions = await new TransactionsService().GetTransactionByCurrent();
            Transactions.Clear();
            foreach(var item in transactions)
            {
                Transactions.Add(item);
            }
        }

        private void RefreshingFunc()
        {
            Transactions.Clear();
            GetTransactions();
        }
        #endregion
    }
}
