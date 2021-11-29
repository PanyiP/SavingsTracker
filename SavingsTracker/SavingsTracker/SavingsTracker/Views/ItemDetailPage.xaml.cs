using SavingsTracker.ViewModels;
using System.ComponentModel;
using Xamarin.Forms;

namespace SavingsTracker.Views
{
   public partial class ItemDetailPage : ContentPage
   {
      public ItemDetailPage()
      {
         InitializeComponent();
         BindingContext = new ItemDetailViewModel();
      }
   }
}