using SavingsTracker.Models;
using SavingsTracker.Services;
using System.Windows.Input;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   internal class NewSavingAccountPageViewModel : BaseViewModel
   {
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
            await SavingAccountDBService.AddNewSavingAccountAsync(SavingAccount);

            await Shell.Current.GoToAsync("..", true);
         });
      }
   }
}
