using SavingsTracker.Models;
using System;
using System.Collections.ObjectModel;
using System.Threading.Tasks;

namespace SavingsTracker.Services
{
   public static class SavingAccountDBService
   {
      public static async Task<ObservableCollection<SavingAccount>> SetupMockDataAsync()
      {
         ObservableCollection<SavingAccount> collection = new ObservableCollection<SavingAccount>();

         collection.Add(new SavingAccount("Folyószámla", "HUF"));
         await collection[0].AddNewBalanceAsync(new DateTime(2021, 10, 5, 20, 0, 0), 100000.0);
         await collection[0].AddNewBalanceAsync(new DateTime(2021, 11, 5, 20, 0, 0), 200000.0);
         await collection[0].AddNewBalanceAsync(new DateTime(2021, 12, 5, 20, 0, 0), 300000.0);

         collection.Add(new SavingAccount("TBSZ 2020", "HUF"));
         await collection[1].AddNewBalanceAsync(new DateTime(2021, 10, 5, 20, 0, 0), 500000.0);
         await collection[1].AddNewBalanceAsync(new DateTime(2021, 11, 5, 20, 0, 0), 550000.0);
         await collection[1].AddNewBalanceAsync(new DateTime(2021, 12, 5, 20, 0, 0), 600000.0);

         collection.Add(new SavingAccount("TBSZ 2021", "HUF"));
         await collection[2].AddNewBalanceAsync(new DateTime(2021, 10, 5, 20, 0, 0), 1000000.0);
         await collection[2].AddNewBalanceAsync(new DateTime(2021, 11, 5, 20, 0, 0), 2000000.0);
         await collection[2].AddNewBalanceAsync(new DateTime(2021, 12, 5, 20, 0, 0), 3000000.0);

         return collection;
      }

      public static async Task<SavingAccount> RefreshSavingAccount(SavingAccount account)
      {
         await account.AddNewBalanceAsync(DateTime.Now, account.CurrentBalance.Value + 200000.0);

         return account;
      }
   }
}
