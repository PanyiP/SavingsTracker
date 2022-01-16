using Foundation;
using SavingsTracker.DependencyServices;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using UIKit;
using Xamarin.Forms;

[assembly: Dependency(typeof(SavingsTracker.iOS.DependencyServices.Environment))]

namespace SavingsTracker.iOS.DependencyServices
{
   public class Environment : IEnvironment
   {
      public void SetStatusBarColor(System.Drawing.Color color, bool darkStatusBarTint)
      {/*
         if (UIDevice.CurrentDevice.CheckSystemVersion(13, 0))
         {
            var statusBar = new UIView(UIApplication.SharedApplication.KeyWindow.WindowScene.StatusBarManager.StatusBarFrame);
            statusBar.BackgroundColor = color.ToPlatformColor();
            UIApplication.SharedApplication.KeyWindow.AddSubview(statusBar);
         }
         else
         {
            var statusBar = UIApplication.SharedApplication.ValueForKey(new NSString("statusBar")) as UIView;
            if (statusBar.RespondsToSelector(new ObjCRuntime.Selector("setBackgroundColor:")))
            {
               statusBar.BackgroundColor = color.ToPlatformColor();
            }
         }*/
         var style = darkStatusBarTint ? UIStatusBarStyle.DarkContent : UIStatusBarStyle.LightContent;
         UIApplication.SharedApplication.SetStatusBarStyle(style, false);
         Xamarin.Essentials.Platform.GetCurrentUIViewController()?.SetNeedsStatusBarAppearanceUpdate();
      }
   }
}