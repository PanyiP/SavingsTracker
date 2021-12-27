using SavingsTracker.ViewModels;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SavingsTracker.Views
{
   [XamlCompilation(XamlCompilationOptions.Compile)]
   public partial class OptionsPage : ContentPage
   {
      public OptionsPage()
      {
         InitializeComponent();
      }

      private void LanguageRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
      {
         var vm = (OptionsPageViewModel)BindingContext;
         vm.ChangeLanguageCommand.Execute(vm);
      }

      private void ThemeRadioButton_CheckedChanged(object sender, CheckedChangedEventArgs e)
      {
         var vm = (OptionsPageViewModel)BindingContext;
         vm.ChangeThemeCommand.Execute(vm);
      }
   }
}
