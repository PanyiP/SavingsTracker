using SavingsTracker.Models;
using SavingsTracker.ViewModels;
using SavingsTracker.Views;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xamarin.Forms;
using Xamarin.Forms.Xaml;

namespace SavingsTracker.Views
{
   public partial class ItemsPage : ContentPage
   {
      ItemsViewModel _viewModel;

      public ItemsPage()
      {
         InitializeComponent();

         BindingContext = _viewModel = new ItemsViewModel();
      }

      protected override void OnAppearing()
      {
         base.OnAppearing();
         _viewModel.OnAppearing();
      }
   }
}