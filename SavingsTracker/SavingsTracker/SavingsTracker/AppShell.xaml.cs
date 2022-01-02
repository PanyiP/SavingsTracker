using SavingsTracker.ViewModels;
using SavingsTracker.Views;
using System;
using Xamarin.Forms;

namespace SavingsTracker
{
   public partial class AppShell : Shell
   {
      public AppShell()
      {
         InitializeComponent();

         Routing.RegisterRoute(nameof(SavingAccountDetailsPage), typeof(SavingAccountDetailsPage));
         Routing.RegisterRoute(nameof(NewSavingAccountPage), typeof(NewSavingAccountPage));
         Routing.RegisterRoute(nameof(NewBalancePage), typeof(NewBalancePage));
      }

      private async void OnMenuItemClicked(object sender, EventArgs e)
      {
         await Current.GoToAsync("//LoginPage");
      }
   }
}
