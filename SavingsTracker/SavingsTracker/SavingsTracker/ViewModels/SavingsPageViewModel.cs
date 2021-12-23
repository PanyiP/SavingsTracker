using System.Collections.ObjectModel;
using System.Windows.Input;
using SavingsTracker.Models;
using SavingsTracker.Services;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   public class SavingsPageViewModel : BaseViewModel
   {
      public ObservableCollection<SavingAccount> SavingAccounts { get; set; }

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

      public ICommand SavingAccountTapped { get; }
      public ICommand RefreshViewCommand { get; }

      public SavingsPageViewModel()
      {
         SetupSavingAccountsAsync();

         SavingAccountTapped = new Command<object>( (obj) =>
         {
            //Navigate to SavingAccountDetailsPage
            //string value = obj.ToString();
         });

         RefreshViewCommand = new Command(
            execute: async () =>
            {
               IsRefreshBusy = true;

               foreach (var account in SavingAccounts)
               {
                  await SavingAccountDBService.RefreshSavingAccount(account);
               }

               IsRefreshBusy = false;
            },
            canExecute: () =>
            {
               return !IsRefreshBusy;
            }
         );
      }

      private async void SetupSavingAccountsAsync()
      {
         SavingAccounts = await SavingAccountDBService.SetupMockDataAsync();
      }
   }
}
