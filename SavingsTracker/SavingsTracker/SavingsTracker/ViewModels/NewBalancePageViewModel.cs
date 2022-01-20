using SavingsTracker.Models;
using SavingsTracker.Services;
using System;
using System.Collections.Generic;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   /// <summary>
   /// View model for the NewBalancePage
   /// </summary>
   internal class NewBalancePageViewModel : BaseViewModel, IQueryAttributable
   {
      //TODO: Theme: DatePicker underlining should change color according to the theme. -> Accent color should be set based on the theme.
      //TODO: Theme: DatePicker should change color according to the theme. -> Accent color should be set based on the theme.
      private string title;
      /// <summary>
      /// The Title of the Page
      /// </summary>
      public string Title
      {
         get { return title; }
         set { SetProperty(ref title, value); }
      }

      private bool isNewBalance;
      /// <summary>
      /// Property to know if the Page is to edit a Balance or create a new one
      /// </summary>
      public bool IsNewBalance
      {
         get { return isNewBalance; }
         set { SetProperty(ref isNewBalance, value); }
      }

      private Balance balance;
      /// <summary>
      /// The Balance to be edited or the new Balance to be created
      /// </summary>
      public Balance Balance
      {
         get { return balance; }
         set { SetProperty(ref balance, value); }
      }

      /// <summary>
      /// Save the modified Balance or new Balance
      /// </summary>
      public ICommand SaveCommand { get; }

      /// <summary>
      /// Constructor
      /// </summary>
      public NewBalancePageViewModel()
      {
         balance = new Balance();

         SaveCommand = new Command(async () =>
         {
            if (IsNewBalance)
            {
               await SavingAccountDBService.AddNewBalanceAsync(Balance);
            }
            else
            {
               await SavingAccountDBService.UpdateBalanceAsync(Balance);
            }

            await Shell.Current.GoToAsync("..", true);
         });
      }

      /// <summary>
      /// Process the navigation attributes of the Page
      /// </summary>
      /// <param name="query"></param>
      public void ApplyQueryAttributes(IDictionary<string, string> query)
      {
         ProcessPageParametersAsync(query);
      }

      private async void ProcessPageParametersAsync(IDictionary<string, string> query)
      {
         // The query parameter requires URL decoding.
         // Store if we are making a new Balance or editing an old one
         bool.TryParse(HttpUtility.UrlDecode(query["NewBalance"]), out isNewBalance);

         if (IsNewBalance)
         {
            Title = Resources.AppResources.NewBalancePageTitle;

            Balance.AccountId = HttpUtility.UrlDecode(query["AccountId"]);
            Balance.DateTime = DateTime.Now;
            Balance.Value = 0.0;
         }
         else
         {
            Title = Resources.AppResources.EditBalancePageTitle;

            Balance = await SavingAccountDBService.GetBalanceAsync(HttpUtility.UrlDecode(query["BalanceId"]));
         }
      }
   }
}
