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
      static SQLiteAsyncConnection db;

      #region MockData
      public static async Task SetupMockDataAsync()
      {
         SavingAccount account1 = new SavingAccount("Folyószámla", "HUF");
         await account1.AddNewBalanceAsync(new DateTime(2021, 10, 5, 20, 0, 0), 100000.0);
         await account1.AddNewBalanceAsync(new DateTime(2021, 11, 5, 20, 0, 0), 200000.0);
         await account1.AddNewBalanceAsync(new DateTime(2021, 12, 5, 20, 0, 0), 300000.0);
         await AddSavingAccountAsync(account1);

         SavingAccount account2 = new SavingAccount("TBSZ 2020", "HUF");
         await account2.AddNewBalanceAsync(new DateTime(2021, 10, 5, 20, 0, 0), 500000.0);
         await account2.AddNewBalanceAsync(new DateTime(2021, 11, 5, 20, 0, 0), 550000.0);
         await account2.AddNewBalanceAsync(new DateTime(2021, 12, 5, 20, 0, 0), 600000.0);
         await AddSavingAccountAsync(account2);

         SavingAccount account3 = new SavingAccount("TBSZ 2021", "HUF");
         await account3.AddNewBalanceAsync(new DateTime(2021, 10, 5, 20, 0, 0), 1000000.0);
         await account3.AddNewBalanceAsync(new DateTime(2021, 11, 5, 20, 0, 0), 2000000.0);
         await account3.AddNewBalanceAsync(new DateTime(2021, 12, 5, 20, 0, 0), 3000000.0);
         await AddSavingAccountAsync(account3);
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

         await db.CreateTableAsync<SavingAccount>();

         // TODO: Remove Mock data
         if (await db.Table<SavingAccount>().CountAsync() == 0)
         {
            await SetupMockDataAsync();
         }
      }

      public static async Task<bool> AddSavingAccountAsync(SavingAccount account)
      { //TODO: SavingAccount.BalanceCollection is not saved into SQLite database
         await Init();
         if (await db.InsertAsync(account) > 0)
         {
            return true;
         }

         return false;
      }

      public static async Task<bool> DeleteSavingAccountAsync(SavingAccount account)
      {//TODO: Delete the SavingAccount.BalanceCollection as well from its table
         await Init();

         if (await db.DeleteAsync<SavingAccount>(account.AccountId) > 0)
         {
            return true;
         }

         return false;
      }

      public static async Task<IEnumerable<SavingAccount>> GetSavingAccountsAsync()
      {//TODO: Get the SavingAccount.BalanceCollection as well from its table
         await Init();

         var teszt = await db.Table<SavingAccount>().ToListAsync();

         return teszt;
      }

      public static async Task<bool> UpdateSavingAccountAsync(SavingAccount account)
      {//TODO: Implement UpdateSavingAccountAsync function
         await Init();

         throw new NotImplementedException();

         return true;
      }
   }
}
