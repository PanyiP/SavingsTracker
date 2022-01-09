using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SavingsTracker.Services
{
   public static class Settings
   {
      public const string defaultLanguage = "DefaultLanguage";
      public const string english = "En";
      public const string hungarian = "Hu";

      public const string defaultTheme = "DefaultTheme";
      public const string light = "Light";
      public const string dark = "Dark";

      public static string Culture
      {
         get
         {
            return Preferences.Get("Culture", defaultLanguage);
         }
         set
         {
            Preferences.Set("Culture", value);
            UpdateAppCulture();
         }
      }

      public static string Theme
      {
         get
         {
            return Preferences.Get("Theme", defaultTheme);
         }
         set
         {
            Preferences.Set("Theme", value);
            UpdateAppTheme();
         }
      }

      public static void UpdateAppTheme()
      {
         if (Theme == defaultTheme)
         {
            App.Current.UserAppTheme = OSAppTheme.Unspecified;
         }
         else if (Theme == light)
         {
            App.Current.UserAppTheme = OSAppTheme.Light;
         }
         else if (Theme == dark)
         {
            App.Current.UserAppTheme = OSAppTheme.Dark;
         }
      }

      public static void UpdateAppCulture()
      {
         if (Culture == defaultLanguage)
         {
            LocalizationResourceManager.Current.CurrentCulture = CultureInfo.InstalledUICulture;
         }
         else if (Culture == english)
         {
            CultureInfo language = new CultureInfo(english);
            LocalizationResourceManager.Current.CurrentCulture = language;
         }
         else if (Culture == hungarian)
         {
            CultureInfo language = new CultureInfo(hungarian);
            LocalizationResourceManager.Current.CurrentCulture = language;
         }
      }
   }
}
