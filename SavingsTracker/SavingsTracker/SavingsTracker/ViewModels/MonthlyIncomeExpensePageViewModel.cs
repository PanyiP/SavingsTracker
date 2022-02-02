using SavingsTracker.Models;
using SavingsTracker.Resources;
using SavingsTracker.Services;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   internal class MonthlyIncomeExpensePageViewModel : BaseViewModel
   {
      public LocalizedString Title { get; } = new LocalizedString(() => AppResources.MonthlyIETitle);
      public LocalizedString Month { get; } = new LocalizedString(() => AppResources.Month + ": ");
      public LocalizedString Income { get; } = new LocalizedString(() => AppResources.Income + ": ");
      public LocalizedString Expense { get; } = new LocalizedString(() => AppResources.Expense + ": ");
      public LocalizedString Balance { get; } = new LocalizedString(() => AppResources.Balance + ": ");

      private Dictionary<DateTime, MonthlyIncomeExpense> incomeAndExpenseByMonth;
      public Dictionary<DateTime, MonthlyIncomeExpense> IncomeAndExpenseByMonth
      {
         get => incomeAndExpenseByMonth;
         set
         {
            SetProperty(ref incomeAndExpenseByMonth, value);
         }
      }

      private readonly SMSParserService smsParserService;

      private string currentBalance;
      public string CurrentBalance
      {
         get => currentBalance;
         set
         {
            SetProperty(ref currentBalance, value);
         }
      }

      public ICommand RefreshCommand { get; }

      public MonthlyIncomeExpensePageViewModel()
      {
         smsParserService = new SMSParserService(Banks.RaiffeisenBank, new List<string> { "+36209000848", "+36303444217" });

         RefreshCommand = new Command(
            execute: () =>
            {
               Task.Run(() =>
                 {
                    IsBusy = true;

                    IncomeAndExpenseByMonth = smsParserService.GetAllIncomeAndExpenseByMonth();

                    CurrentBalance = smsParserService.GetCurrentBalance();

                    IsBusy = false;
                 });
            },
            canExecute: () =>
            {
               return !IsBusy;
            }
            );
      }
   }
}
