using SavingsTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SavingsTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class SavingAccountDetailsPage : ContentPage
   {
      public SavingAccountDetailsPage()
      {
         InitializeComponent();
      }

      protected override void OnAppearing()
      {
         var vm = (SavingAccountDetailsPageViewModel)BindingContext;
         vm.RefreshViewCommand.Execute(vm);

         base.OnAppearing();
      }
   }
}