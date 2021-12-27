using SavingsTracker.Resources;
using Xamarin.CommunityToolkit.Helpers;

namespace SavingsTracker.ViewModels
{
   internal class AppShellViewModel : BaseViewModel
   {
      public LocalizedString SavingAccounts { get; } = new LocalizedString(() => AppResources.SavingAccounts);
      public LocalizedString Options { get; } = new LocalizedString(() => AppResources.Options);
   }
}
