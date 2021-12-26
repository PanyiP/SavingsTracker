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

         Routing.RegisterRoute(nameof(NewSavingAccountPage), typeof(NewSavingAccountPage));
      }

      private async void OnMenuItemClicked(object sender, EventArgs e)
      {
         await Current.GoToAsync("//LoginPage");
      }
   }
}
