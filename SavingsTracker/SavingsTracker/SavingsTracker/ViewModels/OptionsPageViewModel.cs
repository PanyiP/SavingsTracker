using SavingsTracker.Resources;
using SavingsTracker.Services;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   internal class OptionsPageViewModel : BaseViewModel
   {//TODO: Theme: Radio button color should be changed based on the Theme
      //TODO: Theme: Radio button underlining should change color according to the theme.
      //TODO: Theme: Changing the theme is sometimes crashes the App.
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
         if (Settings.Culture == Settings.defaultLanguage)
         {
            IsDefaultLanguageChecked = true;
         }
         else if (Settings.Culture == Settings.english)
         {
            IsENLanguageChecked = true;
         }
         else if (Settings.Culture == Settings.hungarian)
         {
            IsHULanguageChecked = true;
         }

         if (Settings.Theme == Settings.defaultTheme)
         {
            IsDefaultThemeChecked = true;
         }
         else if (Settings.Theme == Settings.light)
         {
            IsLightThemeChecked = true;
         }
         else if (Settings.Theme == Settings.dark)
         {
            IsDarkThemeChecked = true;
         }

         ChangeLanguageCommand = new Command(() =>
         {
            // Changing the language has code part in App.xaml.cs as well
            if (IsDefaultLanguageChecked)
            {
               Settings.Culture = Settings.defaultLanguage;
            }
            else if (IsENLanguageChecked)
            {
               Settings.Culture = Settings.english;
            }
            else if (IsHULanguageChecked)
            {
               Settings.Culture = Settings.hungarian;
            }
         });

         ChangeThemeCommand = new Command(() =>
         {
            // Changing the theme has code part in App.xaml.cs as well
            if (IsDefaultThemeChecked)
            {
               Settings.Theme = Settings.defaultTheme;
            }
            else if (IsLightThemeChecked)
            {
               Settings.Theme = Settings.light;
            }
            else if (IsDarkThemeChecked)
            {
               Settings.Theme = Settings.dark;
            }
         });

         DeleteAllDataCommand = new Command(async () =>
         {
            await SavingAccountDBService.DeleteAllTablesAsync();
         });
      }
   }
}
