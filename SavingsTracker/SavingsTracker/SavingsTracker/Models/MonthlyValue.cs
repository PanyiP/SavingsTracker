using System;
using System.Collections.Generic;
using System.Text;

namespace SavingsTracker.Models
{
   /// <summary>
   /// Class to store the monthly income and expense
   /// </summary>
   public class MonthlyIncomeExpense
   {
      /// <summary>
      /// The year and month part of the data
      /// </summary>
      public DateTime YearAndMonth
      {
         get; 
      }

      /// <summary>
      /// The monthly expense
      /// </summary>
      public double Expense { get; set; } = 0.0;

      /// <summary>
      /// The monthly income
      /// </summary>
      public double Income { get; set; } = 0.0;

      public MonthlyIncomeExpense(DateTime date)
      {
         YearAndMonth = date;
      }
   }
}
