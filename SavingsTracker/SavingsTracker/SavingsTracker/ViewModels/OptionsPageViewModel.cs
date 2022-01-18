using SavingsTracker.Resources;
using SavingsTracker.Services;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   /// <summary>
   /// View model for the OptionsPage
   /// </summary>
   internal class OptionsPageViewModel : BaseViewModel
   {//TODO: Theme: Changing the theme is sometimes crashes the App.
      public LocalizedString Title { get; } = new LocalizedString(() => AppResources.Options);
      public LocalizedString LanguageOptionsTitle { get; } = new LocalizedString(() => AppResources.LanguageOptionsTitle);
      public LocalizedString Default { get; } = new LocalizedString(() => AppResources.Default);
      public LocalizedString EnglishLanguage { get; } = new LocalizedString(() => AppResources.EnglishLanguage);
      public LocalizedString HungarianLanguage { get; } = new LocalizedString(() => AppResources.HungarianLanguage);
      public LocalizedString ThemeOptionsTitle { get; } = new LocalizedString(() => AppResources.ThemeOptionsTitle);
      public LocalizedString LightTheme { get; } = new LocalizedString(() => AppResources.LightTheme);
      public LocalizedString DarkTheme { get; } = new LocalizedString(() => AppResources.DarkTheme);
      public LocalizedString DeleteDbOptionsTitle { get; } = new LocalizedString(() => AppResources.DeleteDbOptionsTitle);
      public LocalizedString Delete { get; } = new LocalizedString(() => AppResources.Delete);

      private bool isDefaultLanguageChecked;
      /// <summary>
      /// Set if the default language radio button is checked
      /// </summary>
      public bool IsDefaultLanguageChecked
      {
         get { return isDefaultLanguageChecked; }
         set { SetProperty(ref isDefaultLanguageChecked, value); }
      }

      private bool isHULanguageChecked;
      /// <summary>
      /// Set if the HU language radio button is checked
      /// </summary>
      public bool IsHULanguageChecked
      {
         get { return isHULanguageChecked; }
         set { SetProperty(ref isHULanguageChecked, value); }
      }

      private bool isENLanguageChecked;
      /// <summary>
      /// Set if the EN language radio button is checked
      /// </summary>
      public bool IsENLanguageChecked
      {
         get { return isENLanguageChecked; }
         set { SetProperty(ref isENLanguageChecked, value); }
      }

      private bool isDefaultThemeChecked;
      /// <summary>
      /// Set if the default theme radio button is checked
      /// </summary>
      public bool IsDefaultThemeChecked
      {
         get { return isDefaultThemeChecked; }
         set { SetProperty(ref isDefaultThemeChecked, value); }
      }

      private bool isLightThemeChecked;
      /// <summary>
      /// Set if the light theme radio button is checked
      /// </summary>
      public bool IsLightThemeChecked
      {
         get { return isLightThemeChecked; }
         set { SetProperty(ref isLightThemeChecked, value); }
      }

      private bool isDarkThemeChecked;
      /// <summary>
      /// Set if the dark theme radio button is checked
      /// </summary>
      public bool IsDarkThemeChecked
      {
         get { return isDarkThemeChecked; }
         set { SetProperty(ref isDarkThemeChecked, value); }
      }

      /// <summary>
      /// Change the language
      /// </summary>
      public ICommand ChangeLanguageCommand { get; }
      /// <summary>
      /// Change the theme
      /// </summary>
      public ICommand ChangeThemeCommand { get; }
      /// <summary>
      /// Delete all data from the SWLite DB
      /// </summary>
      public ICommand DeleteAllDataCommand { get; }

      /// <summary>
      /// Constructor
      /// </summary>
      public OptionsPageViewModel()
      {
         // Set the proper Culture radio button to be checked
         if (Settings.Culture == Settings.SupportedCultures.Default)
         {
            IsDefaultLanguageChecked = true;
         }
         else if (Settings.Culture == Settings.SupportedCultures.En)
         {
            IsENLanguageChecked = true;
         }
         else if (Settings.Culture == Settings.SupportedCultures.Hu)
         {
            IsHULanguageChecked = true;
         }

         // Set the proper Theme radio button to be checked
         if (Settings.Theme == Settings.SupportedThemes.Default)
         {
            IsDefaultThemeChecked = true;
         }
         else if (Settings.Theme == Settings.SupportedThemes.Light)
         {
            IsLightThemeChecked = true;
         }
         else if (Settings.Theme == Settings.SupportedThemes.Dark)
         {
            IsDarkThemeChecked = true;
         }

         ChangeLanguageCommand = new Command(() =>
         {
            // Changing the language has code part in App.xaml.cs as well
            if (IsDefaultLanguageChecked)
            {
               Settings.Culture = Settings.SupportedCultures.Default;
            }
            else if (IsENLanguageChecked)
            {
               Settings.Culture = Settings.SupportedCultures.En;
            }
            else if (IsHULanguageChecked)
            {
               Settings.Culture = Settings.SupportedCultures.Hu;
            }
         });

         ChangeThemeCommand = new Command(() =>
         {
            // Changing the theme has code part in App.xaml.cs as well
            if (IsDefaultThemeChecked)
            {
               Settings.Theme = Settings.SupportedThemes.Default;
            }
            else if (IsLightThemeChecked)
            {
               Settings.Theme = Settings.SupportedThemes.Light;
            }
            else if (IsDarkThemeChecked)
            {
               Settings.Theme = Settings.SupportedThemes.Dark;
            }
         });

         DeleteAllDataCommand = new Command(async () =>
         {
            await SavingAccountDBService.DeleteAllTablesAsync();
         });
      }
   }
}
