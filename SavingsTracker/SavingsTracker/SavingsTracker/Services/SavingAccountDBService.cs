using SavingsTracker.Models;
using SQLite;
using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using Xamarin.Essentials;

namespace SavingsTracker.Services
{
   /// <summary>
   /// Class to handle SQLite DB interactions
   /// </summary>
   public static class SavingAccountDBService
   {
      private static SQLiteAsyncConnection db;

      /// <summary>
      /// Initializes the database and connection
      /// </summary>
      /// <returns></returns>
      static async Task InitAsync()
      {
         if (db != null)
         {
            return;
         }

         var databasePath = Path.Combine(FileSystem.AppDataDirectory, "MyData.db");

         db = new SQLiteAsyncConnection(databasePath);
         await db.CreateTableAsync<SavingAccount>();
         await db.CreateTableAsync<Balance>();
      }

      /// <summary>
      /// Deletes all tables
      /// </summary>
      /// <returns></returns>
      public static async Task<bool> DeleteAllTablesAsync()
      {
         await InitAsync();

         bool ret_val = true;

         ret_val &= (await db.DropTableAsync<SavingAccount>() > 0);
         ret_val &= (await db.DropTableAsync<Balance>() > 0);

         return ret_val;
      }

      #region SavingAccount table functions
      /// <summary>
      /// Creates a new Saving Account
      /// </summary>
      /// <param name="account">The account to be put into the DB</param>
      /// <returns></returns>
      public static async Task<bool> AddNewSavingAccountAsync(SavingAccount account)
      {
         await InitAsync();
         if (await db.InsertAsync(account, typeof(SavingAccount)) > 0)
         {
            return true;
         }

         return false;
      }

      /// <summary>
      /// Deletes a Saving Account
      /// </summary>
      /// <param name="account">The account to be deleted from the DB</param>
      /// <returns></returns>
      public static async Task<bool> DeleteSavingAccountAsync(SavingAccount account)
      {
         await InitAsync();

         if (await db.DeleteAsync<SavingAccount>(account.AccountId) > 0)
         {
            return true;
         }

         return false;
      }

      /// <summary>
      /// Get all Saving Accounts from the DB
      /// </summary>
      /// <returns></returns>
      public static async Task<IEnumerable<SavingAccount>> GetSavingAccountsAsync()
      {
         await InitAsync();

         return await db.Table<SavingAccount>().ToListAsync();
      }

      /// <summary>
      /// Get one Saving Account from the DB based on the Account ID
      /// </summary>
      /// <param name="id">The ID of the Saving Account to get</param>
      /// <returns></returns>
      public static async Task<SavingAccount> GetSavingAccountAsync(string id)
      {
         await InitAsync();

         return await db.Table<SavingAccount>().Where(account => account.AccountId == id).FirstAsync();
      }

      /// <summary>
      /// Update a Saving Account in the DB
      /// </summary>
      /// <param name="account">The Saving Account data to be updated</param>
      /// <returns></returns>
      public static async Task<bool> UpdateSavingAccountAsync(SavingAccount account)
      {
         await InitAsync();

         if (await db.UpdateAsync(account, typeof(SavingAccount)) > 0)
         {
            return true;
         }

         return false;
      }
      #endregion

      #region Balance table functions
      /// <summary>
      /// Creates a new Balance
      /// </summary>
      /// <param name="balance">The balance to be put into the DB</param>
      /// <returns></returns>
      public static async Task<bool> AddNewBalanceAsync(Balance balance)
      {
         await InitAsync();
         if (await db.InsertAsync(balance, typeof(Balance)) > 0)
         {
            return true;
         }

         return false;
      }

      /// <summary>
      /// Deletes a Balance
      /// </summary>
      /// <param name="balance">The Balance to be deleted from the DB</param>
      /// <returns></returns>
      public static async Task<bool> DeleteBalanceAsync(Balance balance)
      {
         await InitAsync();

         if (await db.DeleteAsync<Balance>(balance.BalanceId) > 0)
         {
            return true;
         }

         return false;
      }

      /// <summary>
      /// Get all Balances of a Saving Account from the DB
      /// </summary>
      /// <param name="account">The Saving Account which Balance's should be returned</param>
      /// <returns></returns>
      public static async Task<IEnumerable<Balance>> GetBalancesAsync(SavingAccount account)
      {
         await InitAsync();

         return await db.Table<Balance>().Where(balance => balance.AccountId == account.AccountId).ToListAsync();
      }

      /// <summary>
      /// Gets the latest Balance of a Saving Account
      /// </summary>
      /// <param name="account">The Saving Account which latest Balance's should be returned</param>
      /// <returns></returns>
      public static async Task<Balance> GetLatestBalanceAsync(SavingAccount account)
      {
         await InitAsync();

         // Get all Balances for a given Account
         List<Balance> balances = await db.Table<Balance>().Where(balance => balance.AccountId == account.AccountId).ToListAsync();

         // Get the latest Balance
         Balance return_value = new Balance();
         if (balances.Count > 0)
         {
            return_value.DateTime = new DateTime(1, 1, 1);
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

      /// <summary>
      /// Gets a Balance from the DB
      /// </summary>
      /// <param name="id">The ID of the Balance to get from the DB</param>
      /// <returns></returns>
      public static async Task<Balance> GetBalanceAsync(string id)
      {
         await InitAsync();

         return await db.Table<Balance>().Where(balance => balance.BalanceId == id).FirstAsync();
      }

      /// <summary>
      /// Updates the Balance in the DB
      /// </summary>
      /// <param name="balance">The Balance data to be updated</param>
      /// <returns></returns>
      public static async Task<bool> UpdateBalanceAsync(Balance balance)
      {
         await InitAsync();

         if (await db.UpdateAsync(balance, typeof(Balance)) > 0)
         {
            return true;
         }

         return false;
      }
      #endregion
   }
}
