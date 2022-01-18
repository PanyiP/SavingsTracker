using SavingsTracker.Models;
using SavingsTracker.Services;
using System.Collections.Generic;
using System.Web;
using System.Windows.Input;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   /// <summary>
   /// View model for the NewSavingAccountPage
   /// </summary>
   internal class NewSavingAccountPageViewModel : BaseViewModel, IQueryAttributable
   {
      private string title;
      /// <summary>
      /// The Title of the Page
      /// </summary>
      public string Title
      {
         get { return title; }
         set { SetProperty(ref title, value); }
      }

      private bool isNewSavingAccount;
      /// <summary>
      /// Property to know if the Page is to edit a Saving Account or create a new one
      /// </summary>
      public bool IsNewSavingAccount
      {
         get { return isNewSavingAccount; }
         set { SetProperty(ref isNewSavingAccount, value); }
      }

      private SavingAccount savingAccount;
      /// <summary>
      /// The Saving Account to be edited or the new Saving Account to be created
      /// </summary>
      public SavingAccount SavingAccount
      {
         get { return savingAccount; }
         set { SetProperty(ref savingAccount, value); }
      }

      /// <summary>
      /// Save the modified Saving Account or new Saving Account
      /// </summary>
      public ICommand SaveCommand { get; }

      /// <summary>
      /// Constructor
      /// </summary>
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
