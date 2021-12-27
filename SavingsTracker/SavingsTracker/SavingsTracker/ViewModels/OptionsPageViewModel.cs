using SavingsTracker.Resources;
using System.Globalization;
using System.Windows.Input;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SavingsTracker.ViewModels
{
   internal class OptionsPageViewModel : BaseViewModel
   {//TODO: Radio button color should be changed to default blue instead of pink
      public LocalizedString Title { get; } = new LocalizedString(() => AppResources.Options);
      public LocalizedString LanguageOptionsTitle { get; } = new LocalizedString(() => AppResources.LanguageOptionsTitle);
      public LocalizedString Default { get; } = new LocalizedString(() => AppResources.Default);
      public LocalizedString EnglishLanguage { get; } = new LocalizedString(() => AppResources.EnglishLanguage);
      public LocalizedString HungarianLanguage { get; } = new LocalizedString(() => AppResources.HungarianLanguage);
      public LocalizedString ThemeOptionsTitle { get; } = new LocalizedString(() => AppResources.ThemeOptionsTitle);
      public LocalizedString LightTheme { get; } = new LocalizedString(() => AppResources.LightTheme);
      public LocalizedString DarkTheme { get; } = new LocalizedString(() => AppResources.DarkTheme);

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

      public ICommand ChangeLanguageCommand { get; }
      public ICommand ChangeThemeCommand { get; }

      public OptionsPageViewModel()
      {
         string culture = Preferences.Get("Culture", "defaultLanguage");
         if (culture == "defaultLanguage")
         {
            IsDefaultLanguageChecked = true;
         }
         else if (culture == "en")
         {
            IsENLanguageChecked = true;
         }
         else if (culture == "hu")
         {
            IsHULanguageChecked = true;
         }

         ChangeLanguageCommand = new Command(() =>
         {
            // Changing the language has code part in App.xaml.cs as well
            if (IsDefaultLanguageChecked)
            {
               LocalizationResourceManager.Current.CurrentCulture = CultureInfo.InstalledUICulture;
               Preferences.Set("Culture", "defaultLanguage");
            }
            else if (IsENLanguageChecked)
            {
               CultureInfo language = new CultureInfo("en");
               LocalizationResourceManager.Current.CurrentCulture = language;
               Preferences.Set("Culture", language.Name);

               IsENLanguageChecked = true;
            }
            else if (IsHULanguageChecked)
            {
               CultureInfo language = new CultureInfo("hu");
               LocalizationResourceManager.Current.CurrentCulture = language;
               Preferences.Set("Culture", language.Name);

               IsHULanguageChecked = true;
            }
         });

         ChangeThemeCommand = new Command(() =>
         {
            //TODO: Make it possible to have normal or dark theme
         });
      }
   }
}
