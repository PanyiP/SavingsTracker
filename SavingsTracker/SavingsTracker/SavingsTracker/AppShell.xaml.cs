using SavingsTracker.ViewModels;
using SavingsTracker.Views;
using System;
using System.Collections.Generic;
using Xamarin.Forms;

namespace SavingsTracker
{
   public partial class AppShell : Xamarin.Forms.Shell
   {
      public AppShell()
      {
         InitializeComponent();
         Routing.RegisterRoute(nameof(ItemDetailPage), typeof(ItemDetailPage));
         Routing.RegisterRoute(nameof(NewItemPage), typeof(NewItemPage));
      }

      private async void OnMenuItemClicked(object sender, EventArgs e)
      {
         await Shell.Current.GoToAsync("//LoginPage");
      }
   }
}
