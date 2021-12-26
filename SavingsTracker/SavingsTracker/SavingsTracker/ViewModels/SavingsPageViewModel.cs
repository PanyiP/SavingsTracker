using System.Collections.ObjectModel;
using System.Windows.Input;
using SavingsTracker.Models;
using SavingsTracker.Services;
using SavingsTracker.Views;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   public class SavingsPageViewModel : BaseViewModel
   {
      private ObservableCollection<SavingAccount> savingAccounts;
      public ObservableCollection<SavingAccount> SavingAccounts
      {
         get { return savingAccounts; }
         set { SetProperty(ref savingAccounts, value); }
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

      public ICommand SavingAccountTappedCommand { get; }
      public ICommand RefreshViewCommand { get; }
      public ICommand NewSavingAccountCommand { get; }
      public ICommand DeleteSavingAccountCommand { get; }
      public ICommand EditSavingAccountCommand { get; }

      public SavingsPageViewModel()
      {
         SavingAccountTappedCommand = new Command<object>( (obj) =>
         {
            //TODO: Implement SavingAccountTappedCommand Command

            (RefreshViewCommand as Command).Execute(null);
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
            await Shell.Current.GoToAsync(nameof(NewSavingAccountPage), true);
         });

         DeleteSavingAccountCommand = new Command<SavingAccount>(async (account) =>
         {
            await SavingAccountDBService.DeleteSavingAccountAsync(account);

            (RefreshViewCommand as Command).Execute(null);
         });

         EditSavingAccountCommand = new Command<SavingAccount>(async (account) =>
         {
            //TODO: Implement EditSavingAccountCommand Command

            (RefreshViewCommand as Command).Execute(null);
         });

         //(RefreshViewCommand as Command).Execute(null);
      }
   }
}
