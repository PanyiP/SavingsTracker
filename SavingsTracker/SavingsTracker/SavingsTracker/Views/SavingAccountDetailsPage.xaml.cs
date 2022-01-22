using Microcharts;
using SavingsTracker.Services;
using SavingsTracker.ViewModels;
using System;
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
         base.OnAppearing();

         // load data
         var vm = (SavingAccountDetailsPageViewModel)BindingContext;
         vm.RefreshViewCommand.Execute(vm);
      }

      private void GraphViewItem_TabTapped(object sender, Xamarin.CommunityToolkit.UI.Views.TabTappedEventArgs e)
      {
         // Create chart
         var vm = (SavingAccountDetailsPageViewModel)BindingContext;
         chartView.Chart = new LineChart();

         // Set colors based on theme
         if (Settings.Theme == Settings.SupportedThemes.Dark)
         {
            // Background color
            if (Application.Current.Resources.TryGetValue("DarkThemeSurface", out var backgroundColor))
            {
               string color = "#" +
                  Convert.ToInt32(((Color)backgroundColor).R * 255).ToString("X2") +
                  Convert.ToInt32(((Color)backgroundColor).G * 255).ToString("X2") +
                  Convert.ToInt32(((Color)backgroundColor).B * 255).ToString("X2");

               chartView.Chart.BackgroundColor = SkiaSharp.SKColor.Parse(color);
            }
            // Set label color
            if (Application.Current.Resources.TryGetValue("DarkThemeOnSurface", out var valueLabelColor))
            {
               string color = "#" +
                  Convert.ToInt32(((Color)valueLabelColor).R * 255).ToString("X2") +
                  Convert.ToInt32(((Color)valueLabelColor).G * 255).ToString("X2") +
                  Convert.ToInt32(((Color)valueLabelColor).B * 255).ToString("X2");

               chartView.Chart.LabelColor = SkiaSharp.SKColor.Parse(color);

               foreach (var entry in vm.ChartEntries)
               {
                  entry.ValueLabelColor = SkiaSharp.SKColor.Parse(color);
               }
            }
            // Set Chart color
            if (Application.Current.Resources.TryGetValue("DarkThemePrimary", out var chartColor))
            {
               string color = "#" +
                  Convert.ToInt32(((Color)chartColor).R * 255).ToString("X2") +
                  Convert.ToInt32(((Color)chartColor).G * 255).ToString("X2") +
                  Convert.ToInt32(((Color)chartColor).B * 255).ToString("X2");

               foreach (var entry in vm.ChartEntries)
               {
                  entry.Color = SkiaSharp.SKColor.Parse(color);
               }
            }
         }
         else
         {
            // Background color
            if (Application.Current.Resources.TryGetValue("LightThemeSurface", out var backgroundColor))
            {
               string background = "#" +
                  Convert.ToInt32(((Color)backgroundColor).R * 255).ToString("X2") +
                  Convert.ToInt32(((Color)backgroundColor).G * 255).ToString("X2") +
                  Convert.ToInt32(((Color)backgroundColor).B * 255).ToString("X2");

               chartView.Chart.BackgroundColor = SkiaSharp.SKColor.Parse(background);
            }
            // Set Chart value label color
            if (Application.Current.Resources.TryGetValue("LightThemeOnSurface", out var valueLabelColor))
            {
               string color = "#" +
                  Convert.ToInt32(((Color)valueLabelColor).R * 255).ToString("X2") +
                  Convert.ToInt32(((Color)valueLabelColor).G * 255).ToString("X2") +
                  Convert.ToInt32(((Color)valueLabelColor).B * 255).ToString("X2");

               chartView.Chart.LabelColor = SkiaSharp.SKColor.Parse(color);

               foreach (var entry in vm.ChartEntries)
               {
                  entry.ValueLabelColor = SkiaSharp.SKColor.Parse(color);
               }
            }
            // Set Chart color
            if (Application.Current.Resources.TryGetValue("LightThemePrimary", out var chartColor))
            {
               string color = "#" +
                  Convert.ToInt32(((Color)chartColor).R * 255).ToString("X2") +
                  Convert.ToInt32(((Color)chartColor).G * 255).ToString("X2") +
                  Convert.ToInt32(((Color)chartColor).B * 255).ToString("X2");

               foreach (var entry in vm.ChartEntries)
               {
                  entry.Color = SkiaSharp.SKColor.Parse(color);
               }
            }
         }

         ((LineChart)chartView.Chart).LabelOrientation = Orientation.Vertical;
         ((LineChart)chartView.Chart).ValueLabelOrientation = Orientation.Vertical;
         ((LineChart)chartView.Chart).LabelTextSize = 40;

         chartView.Chart.Entries = vm.ChartEntries;
      }
   }
}