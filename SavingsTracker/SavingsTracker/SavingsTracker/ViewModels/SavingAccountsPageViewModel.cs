using System.Collections.ObjectModel;
using System.Windows.Input;
using SavingsTracker.Models;
using SavingsTracker.Resources;
using SavingsTracker.Services;
using SavingsTracker.Views;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   /// <summary>
   /// View model for the SavingAccountsPage
   /// </summary>
   internal class SavingAccountsPageViewModel : BaseViewModel
   {
      public LocalizedString NewAccount { get; } = new LocalizedString(() => AppResources.NewAccount);
      public LocalizedString SavingAccountsHeader { get; } = new LocalizedString(() => AppResources.SavingAccounts);

      private ObservableCollection<SavingAccount> savingAccounts;
      /// <summary>
      /// Saving Accounts to be shown on the page
      /// </summary>
      public ObservableCollection<SavingAccount> SavingAccounts
      {
         get { return savingAccounts; }
         set { SetProperty(ref savingAccounts, value); }
      }

      private bool isRefreshBusy;
      /// <summary>
      /// Busy indicator for refreshing the page
      /// </summary>
      public bool IsRefreshBusy
      {
         get { return isRefreshBusy; }
         set
         {
            SetProperty(ref isRefreshBusy, value);
            (RefreshViewCommand as Command).ChangeCanExecute();
         }
      }

      /// <summary>
      /// Load Saving Account Details page with the tapped Saving Account data
      /// </summary>
      public ICommand SavingAccountTappedCommand { get; }
      /// <summary>
      /// Refresh the page
      /// </summary>
      public ICommand RefreshViewCommand { get; }
      /// <summary>
      /// Load create new Saving Account page
      /// </summary>
      public ICommand NewSavingAccountCommand { get; }
      /// <summary>
      /// Delete Saving Account
      /// </summary>
      public ICommand DeleteSavingAccountCommand { get; }
      /// <summary>
      /// Load edit Saving Account page
      /// </summary>
      public ICommand EditSavingAccountCommand { get; }

      /// <summary>
      /// Constructor
      /// </summary>
      public SavingAccountsPageViewModel()
      {
         SavingAccountTappedCommand = new Command<object>(async (obj) =>
         {
            string accountId = (obj as SavingAccount).AccountId;
            await Shell.Current.GoToAsync($"{nameof(SavingAccountDetailsPage)}?SavingAccountId={accountId}", true);
         });

         RefreshViewCommand = new Command(
            execute: async () =>
            {
               IsRefreshBusy = true;

               // Clear the SavingAccounts ObservableCollection
               SavingAccounts?.Clear();
               // Get all SavingAccounts from the database
               SavingAccounts = new ObservableCollection<SavingAccount>(await SavingAccountDBService.GetSavingAccountsAsync());
               // Get the CurrentBalance of all SavingAccounts
               foreach (var account in SavingAccounts)
               {
                  account.CurrentBalance = await SavingAccountDBService.GetLatestBalanceAsync(account);
               }

               IsRefreshBusy = false;
            },
            canExecute: () =>
            {
               return !IsRefreshBusy;
            }
         );

         NewSavingAccountCommand = new Command(async () =>
         {
            await Shell.Current.GoToAsync($"{nameof(NewSavingAccountPage)}?NewSavingAccount=true", true);
         });

         DeleteSavingAccountCommand = new Command<SavingAccount>(async (account) =>
         {
            string result = await Shell.Current.DisplayActionSheet(AppResources.DeleteQuestion, AppResources.No, AppResources.Yes);
            if (result == AppResources.Yes)
            {
               await SavingAccountDBService.DeleteSavingAccountAsync(account);

               (RefreshViewCommand as Command).Execute(null);
            }
         });

         EditSavingAccountCommand = new Command<SavingAccount>(async (account) =>
         {
            await Shell.Current.GoToAsync($"{nameof(NewSavingAccountPage)}?NewSavingAccount=false&SavingAccountId={account.AccountId}", true);
         });
      }
   }
}
