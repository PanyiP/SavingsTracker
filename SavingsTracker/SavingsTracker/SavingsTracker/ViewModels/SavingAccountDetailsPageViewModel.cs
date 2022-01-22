using Microcharts;
using SavingsTracker.Models;
using SavingsTracker.Resources;
using SavingsTracker.Services;
using SavingsTracker.Views;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Globalization;
using System.Linq;
using System.Web;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   /// <summary>
   /// View model for the SavingAccountDetailsPage
   /// </summary>
   internal class SavingAccountDetailsPageViewModel : BaseViewModel, IQueryAttributable
   {
      public LocalizedString NewBalance { get; } = new LocalizedString(() => AppResources.NewBalancePageTitle);

      /// <summary>
      /// The Saving Account ID to be loaded
      /// </summary>
      private string savingAccountId;

      private SavingAccount savingAccount;
      /// <summary>
      /// The Saving Account to be shown on the page
      /// </summary>
      public SavingAccount SavingAccount
      {
         get { return savingAccount; }
         set { SetProperty(ref savingAccount, value); }
      }

      private ObservableCollection<Balance> balances;
      /// <summary>
      /// The Balances to be shown on the page
      /// </summary>
      public ObservableCollection<Balance> Balances
      {
         get { return balances; }
         set { SetProperty(ref balances, value); }
      }

      /// <summary>
      /// The Balances to be shown in the Chart
      /// </summary>
      public ObservableCollection<ChartEntry> ChartEntries
      {
         get;
         private set;
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
      /// Refresh the page
      /// </summary>
      public ICommand RefreshViewCommand { get; }
      /// <summary>
      /// Delete Balance
      /// </summary>
      public ICommand DeleteBalanceCommand { get; }
      /// <summary>
      /// Edit Balance
      /// </summary>
      public ICommand EditBalanceCommand { get; }
      /// <summary>
      /// Create new Balance
      /// </summary>
      public ICommand NewBalanceCommand { get; }

      /// <summary>
      /// Constructor
      /// </summary>
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

               // Update container for the ChartView as well
               ChartEntries?.Clear();
               ChartEntries = new ObservableCollection<ChartEntry>();
               foreach (var balance in temp)
               {
                  ChartEntries.Add(new ChartEntry((float)balance.Value)
                  {
                     Label = balance.DateTime.ToString("d", CultureInfo.DefaultThreadCurrentCulture),
                     ValueLabel = balance.Value.ToString("N0") + " " + SavingAccount.Currency
                  });
               }

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

      /// <summary>
      /// Process the navigation attributes of the Page
      /// </summary>
      /// <param name="query"></param>
      public void ApplyQueryAttributes(IDictionary<string, string> query)
      {
         // The query parameter requires URL decoding.
         savingAccountId = HttpUtility.UrlDecode(query["SavingAccountId"]);

         // Load data into the view
         (RefreshViewCommand as Command).Execute(null);
      }
   }
}
