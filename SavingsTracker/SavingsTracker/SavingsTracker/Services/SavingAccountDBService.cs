using SavingsTracker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SavingsTracker.Services
{
   public static class SavingAccountDBService
   {
      private static SQLiteAsyncConnection db;

      #region MockData
      public static async Task SetupMockDataAsync()
      {
         SavingAccount account1 = new SavingAccount("Folyószámla", "HUF");
         await AddNewSavingAccountAsync(account1);
         await AddNewBalanceAsync(new Balance(account1.AccountId, new DateTime(2021, 10, 5, 20, 0, 0), 100000.0));
         await AddNewBalanceAsync(new Balance(account1.AccountId, new DateTime(2021, 11, 5, 20, 0, 0), 200000.0));
         await AddNewBalanceAsync(new Balance(account1.AccountId, new DateTime(2021, 12, 5, 20, 0, 0), 300000.0));

         SavingAccount account2 = new SavingAccount("TBSZ 2020", "HUF");
         await AddNewSavingAccountAsync(account2);
         await AddNewBalanceAsync(new Balance(account2.AccountId, new DateTime(2021, 10, 5, 20, 0, 0), 500000.0));
         await AddNewBalanceAsync(new Balance(account2.AccountId, new DateTime(2021, 11, 5, 20, 0, 0), 550000.0));
         await AddNewBalanceAsync(new Balance(account2.AccountId, new DateTime(2021, 12, 5, 20, 0, 0), 600000.0));

         SavingAccount account3 = new SavingAccount("TBSZ 2021", "HUF");
         await AddNewSavingAccountAsync(account3);
         await AddNewBalanceAsync(new Balance(account3.AccountId, new DateTime(2021, 10, 5, 20, 0, 0), 1000000.0));
         await AddNewBalanceAsync(new Balance(account3.AccountId, new DateTime(2021, 11, 5, 20, 0, 0), 2000000.0));
         await AddNewBalanceAsync(new Balance(account3.AccountId, new DateTime(2021, 12, 5, 20, 0, 0), 3000000.0));
      }
      #endregion

      static async Task Init()
      {
         if (db != null)
         {
            return;
         }

         var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

         db = new SQLiteAsyncConnection(databasePath);
         // TODO: Remove Mock data: Remove DropTableAsync()
         //await db.DropTableAsync<SavingAccount>();
         //await db.DropTableAsync<Balance>();
         await db.CreateTableAsync<SavingAccount>();
         await db.CreateTableAsync<Balance>();

         // TODO: Remove Mock data: Remove SetupMockDataAsync()
         if (await db.Table<SavingAccount>().CountAsync() == 0)
         {
            await SetupMockDataAsync();
         }
      }

      #region SavingAccount table functions
      public static async Task<bool> AddNewSavingAccountAsync(SavingAccount account)
      {
         await Init();
         if (await db.InsertAsync(account, typeof(SavingAccount)) > 0)
         {
            return true;
         }

         return false;
      }

      public static async Task<bool> DeleteSavingAccountAsync(SavingAccount account)
      {
         await Init();

         if (await db.DeleteAsync<SavingAccount>(account.AccountId) > 0)
         {
            return true;
         }

         return false;
      }

      public static async Task<IEnumerable<SavingAccount>> GetSavingAccountsAsync()
      {
         await Init();

         return await db.Table<SavingAccount>().ToListAsync();
      }

      public static async Task<bool> UpdateSavingAccountAsync(SavingAccount account)
      {
         await Init();

         if (await db.UpdateAsync(account, typeof(SavingAccount)) > 0)
         {
            return true;
         }

         return false;
      }
      #endregion

      #region Balance table functions
      public static async Task<bool> AddNewBalanceAsync(Balance balance)
      {
         await Init();
         if (await db.InsertAsync(balance, typeof(Balance)) > 0)
         {
            return true;
         }

         return false;
      }

      public static async Task<bool> DeleteBalanceAsync(Balance balance)
      {
         await Init();

         if (await db.DeleteAsync<Balance>(balance.BalanceId) > 0)
         {
            return true;
         }

         return false;
      }

      public static async Task<IEnumerable<Balance>> GetBalancesAsync(SavingAccount account)
      {
         await Init();

         return await db.Table<Balance>().Where(balance => balance.AccountId == account.AccountId).ToListAsync();
      }

      public static async Task<Balance> GetLatestBalanceAsync(SavingAccount account)
      {
         await Init();

         // Get all Balances for a given Account
         List<Balance> balances = await db.Table<Balance>().Where(balance => balance.AccountId == account.AccountId).ToListAsync();

         // Get the latest Balance
         Balance return_value = new Balance();
         if (balances.Count > 0)
         {
            return_value.DateTime = new DateTime(1900, 1, 1);
            foreach (var balance in balances)
            {
               if (balance.DateTime > return_value.DateTime)
               {
                  return_value = balance;
               }
            }
         }
         else
         {
            return_value.DateTime = DateTime.Now;
            return_value.Value = 0.0;
         }


         return return_value;
      }

      public static async Task<bool> UpdateBalanceAsync(Balance balance)
      {
         await Init();

         if (await db.UpdateAsync(balance, typeof(Balance)) > 0)
         {
            return true;
         }

         return false;
      }
      #endregion
   }
}
