﻿using SavingsTracker.Resources;
using System.Globalization;
using Xamarin.CommunityToolkit.Helpers;
using Xamarin.Essentials;
using Xamarin.Forms;

namespace SavingsTracker
{
   public partial class App : Application
   {

      public App()
      {
         // I have implemented localization three different ways:
         // 1- On pages that are accessible from the AppShell.Flyout menu I had to use LocalizedString properties in the viewmodel and bind it in the view
         // 2- On pages that are accesed with Shell.Current.GoToAsync I bound the view directly to the AppResources, no property needed here because
         //    these pages are always loaded brand new so the localization does not need to be updated when it changes
         // 3- On the AppShell Flyout menu itself the binding mode had to be set to OneWay. Otherwise the Flyout menu did not update when localization changed
         LocalizationResourceManager.Current.PropertyChanged += (sender, e) => AppResources.Culture = LocalizationResourceManager.Current.CurrentCulture;
         LocalizationResourceManager.Current.Init(AppResources.ResourceManager);

         if (Preferences.Get("Culture", "defaultLanguage") == "defaultLanguage")
         {
            LocalizationResourceManager.Current.CurrentCulture = CultureInfo.InstalledUICulture;
         }
         else
         {
            CultureInfo language = new CultureInfo(Preferences.Get("Culture", "en"));
            LocalizationResourceManager.Current.CurrentCulture = language;
         }

         InitializeComponent();

         MainPage = new AppShell();
      }

      protected override void OnStart()
      {
      }

      protected override void OnSleep()
      {
      }

      protected override void OnResume()
      {
      }
   }
}
