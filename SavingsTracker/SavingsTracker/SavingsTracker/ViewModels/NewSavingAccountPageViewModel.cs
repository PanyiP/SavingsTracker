using SavingsTracker.Models;
using SavingsTracker.Services;
using System.Collections.Generic;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   internal class NewSavingAccountPageViewModel : BaseViewModel, IQueryAttributable
   {//TODO: Theme: Entry color should be changed based on the Theme
      private string title;
      public string Title
      {
         get { return title; }
         set { SetProperty(ref title, value); }
      }

      private bool isNewSavingAccount;
      public bool IsNewSavingAccount
      {
         get { return isNewSavingAccount; }
         set { SetProperty(ref isNewSavingAccount, value); }
      }

      private SavingAccount savingAccount;
      public SavingAccount SavingAccount
      {
         get { return savingAccount; }
         set { SetProperty(ref savingAccount, value); }
      }

      public ICommand SaveCommand { get; }

      public NewSavingAccountPageViewModel()
      {
         savingAccount = new SavingAccount();

         SaveCommand = new Command(async () =>
         {
            if (IsNewSavingAccount)
            {
               await SavingAccountDBService.AddNewSavingAccountAsync(SavingAccount);
            }
            else
            {
               await SavingAccountDBService.UpdateSavingAccountAsync(SavingAccount);
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
         // Store if we are making a new saving account or editing an old one
         bool.TryParse(HttpUtility.UrlDecode(query["NewSavingAccount"]), out isNewSavingAccount);

         if (IsNewSavingAccount)
         {
            Title = Resources.AppResources.NewSavingAccountPageTitle;
         }
         else
         {
            Title = Resources.AppResources.EditNewSavingAccountPageTitle;

            SavingAccount = await SavingAccountDBService.GetSavingAccountAsync(HttpUtility.UrlDecode(query["SavingAccountId"]));
         }
      }
   }
}
