using SavingsTracker.Models;
using SavingsTracker.Services;
using System;
using System.Collections.Generic;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   internal class NewBalancePageViewModel : BaseViewModel, IQueryAttributable
   {//TODO: Implement binding of TimePicker. It should be bound to a TimeSpan object, but TimeSpan object cannot be saved into SQLite db.
      //TODO: Theme: TimePicker and DatePicker underlining should change color according to the theme.
      //TODO: Theme: TimePicker and DatePicker should change color according to the theme.
      private string title;
      public string Title
      {
         get { return title; }
         set { SetProperty(ref title, value); }
      }

      private bool isNewBalance;
      public bool IsNewBalance
      {
         get { return isNewBalance; }
         set { SetProperty(ref isNewBalance, value); }
      }

      private Balance balance;
      public Balance Balance
      {
         get { return balance; }
         set { SetProperty(ref balance, value); }
      }

      public ICommand SaveCommand { get; }

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

      public void ApplyQueryAttributes(IDictionary<string, string> query)
      {
         ProcessPageParameters(query);
      }

      private async void ProcessPageParameters(IDictionary<string, string> query)
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
