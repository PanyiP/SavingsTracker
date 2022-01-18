using SavingsTracker.DependencyServices;
using System;
using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SavingsTracker.Services
{
   /// <summary>
   /// Helper class to handle Settings
   /// </summary>
   public static class Settings
   {
      /// <summary>
      /// The list of Cultures that are currently supported
      /// </summary>
      public enum SupportedCultures
      {
         Default,
         En,
         Hu
      }

      /// <summary>
      /// The list of Themes that are currently supported
      /// </summary>
      public enum SupportedThemes
      {
         Default,
         Light,
         Dark
      }

      /// <summary>
      /// The currently used Culture
      /// </summary>
      public static SupportedCultures Culture
      {
         get
         {
            return (SupportedCultures)Enum.Parse(typeof(SupportedCultures), Preferences.Get("Culture", SupportedCultures.Default.ToString()));
         }
         set
         {
            Preferences.Set("Culture", value.ToString());
            UpdateAppCulture();
         }
      }

      /// <summary>
      /// The currently used Theme
      /// </summary>
      public static SupportedThemes Theme
      {
         get
         {
            return (SupportedThemes)Enum.Parse(typeof(SupportedThemes), Preferences.Get("Theme", SupportedThemes.Default.ToString()));
         }
         set
         {
            Preferences.Set("Theme", value.ToString());
            UpdateAppTheme();
         }
      }

      /// <summary>
      /// Updates the Theme and uses a Dependency service to update Status Bar Color
      /// </summary>
      public static void UpdateAppTheme()
      {
         // Set the Theme
         if (Theme == SupportedThemes.Default)
         {
            App.Current.UserAppTheme = OSAppTheme.Unspecified;
         }
         else if (Theme == SupportedThemes.Light)
         {
            App.Current.UserAppTheme = OSAppTheme.Light;
         }
         else if (Theme == SupportedThemes.Dark)
         {
            App.Current.UserAppTheme = OSAppTheme.Dark;
         }

         // Set Status Bar Color according to the Theme
         var Environment = DependencyService.Get<IEnvironment>();
         if (App.Current.RequestedTheme == OSAppTheme.Dark)
         {
            if (Application.Current.Resources.TryGetValue("DarkThemeSurface", out var backgroundColor))
            {
               System.Drawing.Color DarkThemeBackground =
                  Color.FromRgba(((Color)backgroundColor).R,
                                 ((Color)backgroundColor).G,
                                 ((Color)backgroundColor).B,
                                 ((Color)backgroundColor).A);
               Environment.SetStatusBarColor(DarkThemeBackground, false);
            }
         }
         else
         {
            if (Application.Current.Resources.TryGetValue("LightThemeSurface", out var backgroundColor))
            {
               System.Drawing.Color LightThemeBackground =
                  Color.FromRgba(((Color)backgroundColor).R,
                                 ((Color)backgroundColor).G,
                                 ((Color)backgroundColor).B,
                                 ((Color)backgroundColor).A);
               Environment.SetStatusBarColor(LightThemeBackground, true);
            }
         }
      }

      /// <summary>
      /// Updates the current Culture
      /// </summary>
      public static void UpdateAppCulture()
      {
         if (Culture == SupportedCultures.Default)
         {
            LocalizationResourceManager.Current.CurrentCulture = CultureInfo.InstalledUICulture;
         }
         else 
         {
            CultureInfo language = new CultureInfo(Culture.ToString());
            LocalizationResourceManager.Current.CurrentCulture = language;
         }
      }
   }
}
