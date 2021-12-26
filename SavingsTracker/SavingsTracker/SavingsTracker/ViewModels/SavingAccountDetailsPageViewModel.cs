using SavingsTracker.Models;
using SavingsTracker.Services;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   internal class SavingAccountDetailsPageViewModel : BaseViewModel, IQueryAttributable
   {
      private string savingAccountId;

      private SavingAccount savingAccount;
      public SavingAccount SavingAccount
      {
         get { return savingAccount; }
         set { SetProperty(ref savingAccount, value); }
      }

      private ObservableCollection<Balance> balances;
      public ObservableCollection<Balance> Balances
      {
         get { return balances; }
         set { SetProperty(ref balances, value); }
      }

      private bool isRefreshBusy;
      public bool IsRefreshBusy
      {
         get { return isRefreshBusy; }
         set
         {
            SetProperty(ref isRefreshBusy, value);
            (RefreshViewCommand as Command).ChangeCanExecute();
         }
      }

      public ICommand RefreshViewCommand { get; }
      public ICommand DeleteBalanceCommand { get; }
      public ICommand EditBalanceCommand { get; }

      public SavingAccountDetailsPageViewModel()
      {
         RefreshViewCommand = new Command(
            execute: async () =>
            {
               IsRefreshBusy = true;

               // Get the SavingAccount from the database
               SavingAccount = await SavingAccountDBService.GetSavingAccountAsync(savingAccountId);
               // Clear the Balances ObservableCollection
               Balances?.Clear();
               // Get the Balances of the current SavingAccount
               Balances = new ObservableCollection<Balance>(await SavingAccountDBService.GetBalancesAsync(SavingAccount));

               IsRefreshBusy = false;
            },
            canExecute: () =>
            {
               return !IsRefreshBusy;
            }
         );

         DeleteBalanceCommand = new Command(() =>
         {
            //TODO: Implement DeleteBalanceCommand
            (RefreshViewCommand as Command).Execute(null);
         });

         EditBalanceCommand = new Command(() =>
         {
            //TODO: Implement EditBalanceCommand
            (RefreshViewCommand as Command).Execute(null);
         });
      }

      public void ApplyQueryAttributes(IDictionary<string, string> query)
      {
         // The query parameter requires URL decoding.
         savingAccountId = HttpUtility.UrlDecode(query["SavingAccountId"]);

         // Load data into the view
         (RefreshViewCommand as Command).Execute(null);
      }
   }
}
