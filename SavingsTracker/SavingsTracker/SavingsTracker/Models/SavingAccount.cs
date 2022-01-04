using SQLite;
using System;

namespace SavingsTracker.Models
{
   public class SavingAccount : BaseModel
   {
      #region Properties
      private string _accountId;
      [PrimaryKey]
      public string AccountId
      { 
         get { return _accountId; } 
         set { SetProperty(ref _accountId, value); }  
      }

      private string _name;
      public string Name
      {
         get { return _name; }
         set { SetProperty(ref _name, value); }
      }

      private string _currency;
      public string Currency
      {
         get { return _currency; }
         set { SetProperty(ref _currency, value); }
      }

      private Balance _currentBalance;
      [Ignore]
      public Balance CurrentBalance
      {
         get { return _currentBalance; }
         set { SetProperty(ref _currentBalance, value); }
      }
      #endregion

      #region Constructor
      public SavingAccount()
      {
         _accountId = Guid.NewGuid().ToString();
         _name = "";
         _currency = "";
      }

      public SavingAccount(string name, string currency)
      {
         _accountId = Guid.NewGuid().ToString();
         _name = name;
         _currency = currency;
      }
      #endregion
   }

   public class Balance : BaseModel
   {
      #region Properties
      private string _balanceId;
      [PrimaryKey]
      public string BalanceId
      {
         get { return _balanceId; }
         set { SetProperty(ref _balanceId, value); }
      }

      private string _accountId;
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
      public Double Value
      {
         get { return _value; }
         set { SetProperty(ref _value, value); }
      }
      #endregion

      #region Constructor
      public Balance()
      {
         BalanceId = Guid.NewGuid().ToString();
      }

      public Balance(string accountID)
      {
         BalanceId = Guid.NewGuid().ToString();
         AccountId = accountID;
      }

      public Balance(string accountID, DateTime datetime, Double value)
      {
         BalanceId = Guid.NewGuid().ToString();
         AccountId = accountID;
         DateTime = datetime;
         Value = value;
      }

      public Balance(string id, string accountID, DateTime datetime, Double value)
      {
         BalanceId = id;
         AccountId = accountID;
         DateTime = datetime;
         Value = value;
      }
      #endregion
   }
}
