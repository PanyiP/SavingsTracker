using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using Xamarin.Forms;
using Xamarin.Forms.Internals;
using SavingsTracker.DependencyServices;
using System.Drawing;
using Xamarin.Essentials;

[assembly: Dependency(typeof(SavingsTracker.Droid.DependencyServices.Environment))]

namespace SavingsTracker.Droid.DependencyServices
{
   public class Environment : IEnvironment
   {
      public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
      {
         if (Build.VERSION.SdkInt < Android.OS.BuildVersionCodes.Lollipop)
            return;

         var activity = Platform.CurrentActivity;
         var window = activity.Window;
         window.AddFlags(Android.Views.WindowManagerFlags.DrawsSystemBarBackgrounds);
         window.ClearFlags(Android.Views.WindowManagerFlags.TranslucentStatus);
         window.SetStatusBarColor(color.ToPlatformColor());

         if (Build.VERSION.SdkInt >= Android.OS.BuildVersionCodes.M)
         {
            var flag = (Android.Views.StatusBarVisibility)Android.Views.SystemUiFlags.LightStatusBar;
            window.DecorView.SystemUiVisibility = darkStatusBarTint ? flag : 0;
         }
      }
   }
}