using SavingsTracker.Models;
using SavingsTracker.Resources;
using SavingsTracker.Services;
using SavingsTracker.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Linq;
using System.Web;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   internal class SavingAccountDetailsPageViewModel : BaseViewModel, IQueryAttributable
   {//TODO: Instead of header menu new item, add bottom pluss button to add new Balance
      public LocalizedString NewBalance { get; } = new LocalizedString(() => AppResources.NewBalancePageTitle);

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
      public ICommand NewBalanceCommand { get; }

      public SavingAccountDetailsPageViewModel()
      {
         RefreshViewCommand = new Command(
            execute: async () =>
            {
               IsRefreshBusy = true;

               // Get the SavingAccount from the database
               SavingAccount = await SavingAccountDBService.GetSavingAccountAsync(savingAccountId);
               SavingAccount.CurrentBalance = await SavingAccountDBService.GetLatestBalanceAsync(SavingAccount);
               // Clear the Balances ObservableCollection
               Balances?.Clear();
               // Get the Balances of the current SavingAccount
               var temp = await SavingAccountDBService.GetBalancesAsync(SavingAccount);
               Balances = new ObservableCollection<Balance>(temp.OrderByDescending(item => item.DateTime));

               IsRefreshBusy = false;
            },
            canExecute: () =>
            {
               return !IsRefreshBusy;
            }
         );

         DeleteBalanceCommand = new Command<Balance>(async (balance) =>
         {
            string result = await Shell.Current.DisplayActionSheet(AppResources.DeleteQuestion, AppResources.No, AppResources.Yes);
            if (result == AppResources.Yes)
            {
               await SavingAccountDBService.DeleteBalanceAsync(balance);

               (RefreshViewCommand as Command).Execute(null);
            }
         });

         EditBalanceCommand = new Command<Balance>(async (balance) =>
         {
            await Shell.Current.GoToAsync($"{nameof(NewBalancePage)}?NewBalance=false&BalanceId={balance.BalanceId}", true);
         });

         NewBalanceCommand = new Command(async () =>
         {
            await Shell.Current.GoToAsync($"{nameof(NewBalancePage)}?NewBalance=true&AccountId={SavingAccount.AccountId}", true);
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
