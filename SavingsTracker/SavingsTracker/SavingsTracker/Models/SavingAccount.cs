using SQLite;
using System;
using System.Collections.ObjectModel;
using System.Linq;
using System.Threading.Tasks;

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

      private readonly ObservableCollection<Balance> _balanceCollection;
      public ObservableCollection<Balance> BalanceCollection { get { return _balanceCollection; } }

      private string _currency;
      public string Currency
      {
         get { return _currency; }
         set { SetProperty(ref _currency, value); }
      }
      #endregion

      #region Auto Calculated Properties
      public Balance CurrentBalance
      {
         get { return BalanceCollection.LastOrDefault(); }
      }
      #endregion

      #region Constructor
      public SavingAccount()
      {
         _accountId = Guid.NewGuid().ToString();
         _name = "";
         _currency = "";
         _balanceCollection = new ObservableCollection<Balance>();
      }

      public SavingAccount(string name, string currency)
      {
         _accountId = Guid.NewGuid().ToString();
         _name = name;
         _currency = currency;
         _balanceCollection = new ObservableCollection<Balance>();
      }
      #endregion

      #region Methods
      public async Task<bool> AddNewBalanceAsync(DateTime datetime, Double value)
      {
         Balance newBalance = new Balance(datetime, value);

         BalanceCollection.Add(newBalance);
         BalanceCollection.OrderBy(balance => balance.DateTime);

         OnPropertyChanged("CurrentBalance");

         return await Task.FromResult(true);
      }

      public async Task<bool> DeleteBalanceAsync(string balanceId)
      {
         Balance toRemove = BalanceCollection.Where(
               (Balance balance) => balance.Id == balanceId).FirstOrDefault();

         BalanceCollection.Remove(toRemove);

         OnPropertyChanged("CurrentBalance");

         return await Task.FromResult(true);
      }

      public async Task<bool> UpdateBalanceAsync(Balance newBalance)
      {
         await DeleteBalanceAsync(newBalance.Id);

         BalanceCollection.Add(newBalance);
         BalanceCollection.OrderBy(balance => balance.DateTime);

         OnPropertyChanged("CurrentBalance");

         return await Task.FromResult(true);
      }
      #endregion
   }

   public class Balance : BaseModel
   {
      [PrimaryKey]
      public string Id { get; }

      private DateTime dateTime;
      public DateTime DateTime
      {
         get { return dateTime; }
         set { SetProperty(ref dateTime, value); }
      }

      private Double _value;
      public Double Value
      {
         get { return _value; }
         set { SetProperty(ref _value, value); }
      }

      public Balance(DateTime datetime, Double value)
      {
         Id = Guid.NewGuid().ToString();
         DateTime = datetime;
         Value = value;
      }

      public Balance(string id, DateTime datetime, Double value)
      {
         Id = id;
         DateTime = datetime;
         Value = value;
      }
   }
}
