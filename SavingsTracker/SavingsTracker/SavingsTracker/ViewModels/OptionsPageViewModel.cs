using SavingsTracker.Resources;
using SavingsTracker.Services;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
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
      public bool IsDefaultLanguageChecked
      {
         get { return isDefaultLanguageChecked; }
         set { SetProperty(ref isDefaultLanguageChecked, value); }
      }

      private bool isHULanguageChecked;
      public bool IsHULanguageChecked
      {
         get { return isHULanguageChecked; }
         set { SetProperty(ref isHULanguageChecked, value); }
      }

      private bool isENLanguageChecked;
      public bool IsENLanguageChecked
      {
         get { return isENLanguageChecked; }
         set { SetProperty(ref isENLanguageChecked, value); }
      }

      private bool isDefaultThemeChecked;
      public bool IsDefaultThemeChecked
      {
         get { return isDefaultThemeChecked; }
         set { SetProperty(ref isDefaultThemeChecked, value); }
      }

      private bool isLightThemeChecked;
      public bool IsLightThemeChecked
      {
         get { return isLightThemeChecked; }
         set { SetProperty(ref isLightThemeChecked, value); }
      }

      private bool isDarkThemeChecked;
      public bool IsDarkThemeChecked
      {
         get { return isDarkThemeChecked; }
         set { SetProperty(ref isDarkThemeChecked, value); }
      }

      public ICommand ChangeLanguageCommand { get; }
      public ICommand ChangeThemeCommand { get; }
      public ICommand DeleteAllDataCommand { get; }

      public OptionsPageViewModel()
      {
         if (Settings.Culture == Settings.SupportedCultures.DefaultLanguage)
         {
            IsDefaultLanguageChecked = true;
         }
         else if (Settings.Culture == Settings.SupportedCultures.English)
         {
            IsENLanguageChecked = true;
         }
         else if (Settings.Culture == Settings.SupportedCultures.Hungarian)
         {
            IsHULanguageChecked = true;
         }

         if (Settings.Theme == Settings.SupportedThemes.DefaultTheme)
         {
            IsDefaultThemeChecked = true;
         }
         else if (Settings.Theme == Settings.SupportedThemes.Light)
         {
            IsLightThemeChecked = true;
         }
         else if (Settings.Theme == Settings.SupportedThemes.dark)
         {
            IsDarkThemeChecked = true;
         }

         ChangeLanguageCommand = new Command(() =>
         {
            // Changing the language has code part in App.xaml.cs as well
            if (IsDefaultLanguageChecked)
            {
               Settings.Culture = Settings.SupportedCultures.DefaultLanguage;
            }
            else if (IsENLanguageChecked)
            {
               Settings.Culture = Settings.SupportedCultures.English;
            }
            else if (IsHULanguageChecked)
            {
               Settings.Culture = Settings.SupportedCultures.Hungarian;
            }
         });

         ChangeThemeCommand = new Command(() =>
         {
            // Changing the theme has code part in App.xaml.cs as well
            if (IsDefaultThemeChecked)
            {
               Settings.Theme = Settings.SupportedThemes.DefaultTheme;
            }
            else if (IsLightThemeChecked)
            {
               Settings.Theme = Settings.SupportedThemes.Light;
            }
            else if (IsDarkThemeChecked)
            {
               Settings.Theme = Settings.SupportedThemes.dark;
            }
         });

         DeleteAllDataCommand = new Command(async () =>
         {
            await SavingAccountDBService.DeleteAllTablesAsync();
         });
      }
   }
}
