using SQLite;
using System;

namespace SavingsTracker.Models
{
   /// <summary>
   /// Class to represent a Saving Account
   /// </summary>
   public class SavingAccount : BaseModel
   {
      #region Properties
      private string _accountId;
      /// <summary>
      /// The unique ID of the Saving Account. Used as primary key in the SQLite database.
      /// </summary>
      [PrimaryKey]
      public string AccountId
      { 
         get { return _accountId; } 
         set { SetProperty(ref _accountId, value); }  
      }

      private string _name;
      /// <summary>
      /// The name of the Saving Account
      /// </summary>
      public string Name
      {
         get { return _name; }
         set { SetProperty(ref _name, value); }
      }

      private string _currency;
      /// <summary>
      /// The currency of the Saving Account
      /// </summary>
      public string Currency
      {
         get { return _currency; }
         set { SetProperty(ref _currency, value); }
      }

      private Balance _currentBalance;
      /// <summary>
      /// The latest balance of the Saving Account
      /// </summary>
      [Ignore]
      public Balance CurrentBalance
      {
         get { return _currentBalance; }
         set { SetProperty(ref _currentBalance, value); }
      }
      #endregion

      /// <summary>
      /// Default constructor
      /// </summary>
      public SavingAccount()
      {
         _accountId = Guid.NewGuid().ToString();
         _name = "";
         _currency = "";
      }
   }

   /// <summary>
   /// Class to represent a Balance for a Saving Account
   /// </summary>
   public class Balance : BaseModel
   {
      #region Properties
      private string _balanceId;
      /// <summary>
      /// The unique ID of the Balance. Used as primary key in the SQLite database.
      /// </summary>
      [PrimaryKey]
      public string BalanceId
      {
         get { return _balanceId; }
         set { SetProperty(ref _balanceId, value); }
      }

      private string _accountId;
      /// <summary>
      /// The parent Saving Account Id. Used to associate the Balance with a Saving Account.
      /// </summary>
      [Indexed]
      public string AccountId
      {
         get { return _accountId; }
         set { SetProperty(ref _accountId, value); }
      }

      private DateTime dateTime;
      /// <summary>
      /// To store the Date portion in SQLite db and to represent the whole Date and Time
      /// </summary>
      public DateTime DateTime
      {
         get { return dateTime; }
         set { SetProperty(ref dateTime, value); }
      }

      private long timeOfDayInTicks;
      /// <summary>
      /// To have TimeOfDay stored in ticks, so it can be saved into SQLite db. TimeSpan cannot be saved into SQLite.
      /// </summary>
      public long TimeOfDayInTicks
      {
         get { return timeOfDayInTicks; }
         set { timeOfDayInTicks = value; }
      }

      private Double _value;
      /// <summary>
      /// The actual value of the Balance at a given Date
      /// </summary>
      public Double Value
      {
         get { return _value; }
         set { SetProperty(ref _value, value); }
      }
      #endregion

      /// <summary>
      /// Default Constructor
      /// </summary>
      public Balance()
      {
         BalanceId = Guid.NewGuid().ToString();
      }
   }
}
