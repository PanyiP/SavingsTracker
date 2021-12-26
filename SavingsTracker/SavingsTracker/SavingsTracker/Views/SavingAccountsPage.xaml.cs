using SavingsTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SavingsTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SavingAccountsPage : ContentPage
   {
      public SavingAccountsPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         var vm = (SavingAccountsPageViewModel)BindingContext;
         vm.RefreshViewCommand.Execute(vm);

         base.OnAppearing();
      }
   }
}