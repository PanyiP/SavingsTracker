using SavingsTracker.DependencyServices;
using SavingsTracker.Models;
using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Text.RegularExpressions;
using Xamarin.Forms;

namespace SavingsTracker.Services
{
   /// <summary>
   /// List of Supported Banks
   /// </summary>
   public enum Banks
   {
      RaiffeisenBank = 0
   }

   /// <summary>
   /// Class to parse SMSs of a bank
   /// </summary>
   public class SMSParserService
   {
      /// <summary>
      /// The bank of which SMSs to parse
      /// </summary>
      private readonly Banks bank;
      /// <summary>
      /// The prone numbers of the bank
      /// </summary>
      private readonly List<string> phoneNumbers;
      /// <summary>
      /// RegEx to get the ammounts from the Raiffeisen SMSs
      /// </summary>
      private readonly string raiffeisenMessageRegExWithCurrency =    @" (\d*|\.|,)* [A-Z]{3}";
      /// <summary>
      /// RegEx to get the ammounts from the Raiffeisen SMSs
      /// </summary>
      private readonly string raiffeisenMessageRegExWithOutCurrency = @" (\d*|\.|,)* ";

      /// <summary>
      /// List of all SMSs
      /// </summary>
      public List<SMS> AllSMSs
      {
         private set;
         get;
      }
      /// <summary>
      /// List of all SMSs from a bank
      /// </summary>
      public List<SMS> AllSMSsFromBank
      {
         private set;
         get;
      }

      /// <summary>
      /// Initializes the object
      /// </summary>
      /// <param name="bankName">Bank name</param>
      /// <param name="phoneNumbers">List of phone numbers belonging to the bank</param>
      public SMSParserService(Banks bank, List<string> phoneNumbers)
      {
         this.bank = bank;
         this.phoneNumbers = phoneNumbers;
         AllSMSs = new List<SMS>();
         AllSMSsFromBank = new List<SMS>();

         GetAllSMSs();
      }

      /// <summary>
      /// Gets all SMSs
      /// </summary>
      public void GetAllSMSs()
      {
         var SMSParser = DependencyService.Get<ISMSReader>();
         SMSParser.Init();
         AllSMSs = SMSParser.ParseAllSMS();

         foreach (var phoneNumber in phoneNumbers)
         {
            AllSMSsFromBank.AddRange(from sms in AllSMSs
                                     where sms.Address.Contains(phoneNumber)
                                     select sms);
         }

         AllSMSsFromBank = new List<SMS>(AllSMSsFromBank.OrderByDescending(sms => sms.Date));
      }

      /// <summary>
      /// Parses an SMS to get the income or expense from it
      /// </summary>
      /// <param name="sms">The SMS to be parsed</param>
      /// <returns></returns>
      private double GetIncomeExpenseValueFromSMS(SMS sms)
      {
         switch (bank)
         {
            case Banks.RaiffeisenBank:
               MatchCollection resultString = Regex.Matches(sms.Body, raiffeisenMessageRegExWithOutCurrency);
               string value = "";

               if (CultureInfo.DefaultThreadCurrentCulture.TwoLetterISOLanguageName == Settings.SupportedCultures.En.ToString().ToLower())
               {
                  value = resultString[0].ToString().Replace(".", "").Replace(",", ".");
               }
               if (CultureInfo.DefaultThreadCurrentCulture.TwoLetterISOLanguageName == Settings.SupportedCultures.Hu.ToString().ToLower())
               {
                  value = resultString[0].ToString().Replace(".", "");
               }

               if (double.TryParse(value, out double result))
               {
                  return result;
               }
               else
               {
                  return 0.0;
               }
            default:
               return 0.0;
         }
      }

      /// <summary>
      /// Gets all SMSs which contains expenses
      /// </summary>
      /// <returns></returns>
      private List<SMS> GetAllExpenseSMS()
      {
         List<SMS> AllExpenses = new List<SMS>();

         switch (bank)
         {
            case Banks.RaiffeisenBank:
               foreach (var phoneNumber in phoneNumbers)
               {
                  AllExpenses.AddRange(from sms in AllSMSsFromBank
                                       where sms.Address.Contains(phoneNumber) &&
                                             (sms.Body.Contains("Sikeres vàsàrlàs") || sms.Body.Contains("Terhelés"))
                                       select sms);
               }
               break;
            default:
               break;
         }

         return AllExpenses;
      }

      /// <summary>
      /// Gets all SMSs which contains incomes
      /// </summary>
      /// <returns></returns>
      private List<SMS> GetAllIncomeSMS()
      {
         List<SMS> AllExpenses = new List<SMS>();

         switch (bank)
         {
            case Banks.RaiffeisenBank:
               foreach (var phoneNumber in phoneNumbers)
               {
                  AllExpenses.AddRange(from sms in AllSMSsFromBank
                                       where sms.Address.Contains(phoneNumber) && sms.Body.Contains("Jòvàìràs")
                                       select sms);
               }
               break;
            default:
               break;
         }

         return AllExpenses;
      }

      /// <summary>
      /// Gets the current balance from the latest SMS
      /// </summary>
      /// <returns></returns>
      public string GetCurrentBalance()
      {
         SMS NewestSMS = AllSMSsFromBank.First();
         switch (bank)
         {
            case Banks.RaiffeisenBank:
               MatchCollection resultString = Regex.Matches(NewestSMS.Body, raiffeisenMessageRegExWithCurrency);
               return resultString[1].ToString();
            default:
               return "0.0";
         }
      }

      /// <summary>
      /// Gets all Income and Expenses groupped by month
      /// </summary>
      /// <returns></returns>
      public List<MonthlyIncomeExpense> GetAllIncomeAndExpenseByMonth()
      {
         List<MonthlyIncomeExpense> AllIncomeAndExpense = new List<MonthlyIncomeExpense>();

         foreach (var sms in GetAllExpenseSMS())
         {
            if (!AllIncomeAndExpense.Any(p => p.YearAndMonth.Year == sms.Date.Year && p.YearAndMonth.Month == sms.Date.Month))
            {
               AllIncomeAndExpense.Add(new MonthlyIncomeExpense(new DateTime(sms.Date.Year, sms.Date.Month, 1)));
            }

            int index = AllIncomeAndExpense.FindIndex(p => p.YearAndMonth.Year == sms.Date.Year && p.YearAndMonth.Month == sms.Date.Month);
            AllIncomeAndExpense[index].Expense += GetIncomeExpenseValueFromSMS(sms);
         }

         foreach (var sms in GetAllIncomeSMS())
         {
            if (!AllIncomeAndExpense.Any(p => p.YearAndMonth.Year == sms.Date.Year && p.YearAndMonth.Month == sms.Date.Month))
            {
               AllIncomeAndExpense.Add(new MonthlyIncomeExpense(new DateTime(sms.Date.Year, sms.Date.Month, 1)));
            }

            int index = AllIncomeAndExpense.FindIndex(p => p.YearAndMonth.Year == sms.Date.Year && p.YearAndMonth.Month == sms.Date.Month);
            AllIncomeAndExpense[index].Income += GetIncomeExpenseValueFromSMS(sms);
         }

         return AllIncomeAndExpense;
      }
   }
}
