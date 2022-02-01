using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using Xamarin.Essentials;
using Xamarin.Forms;
using SavingsTracker.DependencyServices;
using System.Drawing;
using AndroidX.Core.App;
using Plugin.CurrentActivity;
using Google.Android.Material.Snackbar;
using System;

[assembly: Dependency(typeof(SavingsTracker.Droid.DependencyServices.Environment))]

namespace SavingsTracker.Droid.DependencyServices
{
   /// <summary>
   /// Interface for the platform specific Dependency service
   /// </summary>
   public class Environment : IEnvironment
   {
      /// <summary>
      /// Sets the status bar color on the given platform
      /// </summary>
      /// <param name="color">The background color to be set</param>
      /// <param name="darkStatusBarTint">The foreground color of the status bar</param>
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