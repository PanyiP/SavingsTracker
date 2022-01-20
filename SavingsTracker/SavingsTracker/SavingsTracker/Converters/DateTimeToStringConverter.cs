using System;
using System.Globalization;
using Xamarin.Forms;

namespace SavingsTracker.Converters
{
   /// <summary>
   /// Class to converts DateTime to formatted string based on current Culture
   /// </summary>
   public class DateTimeToStringConverter : IValueConverter
   {
      /// <summary>
      /// Converts a DateTime to string based on the currently set Culture in the App
      /// </summary>
      /// <param name="value">The DateTime object to be converted</param>
      /// <param name="targetType">Not used</param>
      /// <param name="parameter">Not used</param>
      /// <param name="culture">Not used</param>
      /// <returns>Returns the Date part of the DateTime object as string in short format</returns>
      public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
      {
         return ((DateTime)value).ToString("d");
      }

      /// <summary>
      /// Converts a string to DateTime
      /// </summary>
      /// <param name="value">The string to be converted</param>
      /// <param name="targetType">Not used</param>
      /// <param name="parameter">Not used</param>
      /// <param name="culture">Not used</param>
      /// <returns>Returns a DateTime object based on the string if successful, returns the current date if not successful</returns>
      public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
      {
         DateTime result;
         if (DateTime.TryParse((string)value, out result))
         {
            return result;
         }
         else
         {
            result = DateTime.Now;
            return result;
         }
      }
   }
}
