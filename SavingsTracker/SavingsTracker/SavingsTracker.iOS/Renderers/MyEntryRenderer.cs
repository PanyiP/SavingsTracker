using SavingsTracker.CustomControls;
using SavingsTracker.iOS.Renderers;
using SavingsTracker.Services;
using System;
using UIKit;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace SavingsTracker.iOS.Renderers
{// TODO: Theme: Set entry underline in iOS to proper color
 //https://stackoverflow.com/questions/38207168/is-it-possible-to-change-the-colour-of-the-line-below-border-of-a-textbox-ent
   /// <summary>
   /// Custom renderer class for the custom Entry view MyEntry
   /// </summary>
   internal class MyEntryRenderer : EntryRenderer
   {
      /// <summary>
      /// Sets theme specific background color for the view
      /// </summary>
      /// <param name="e"></param>
      protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
      {
         base.OnElementChanged(e);

         if (Control != null)
         {
            object backgroundColor;

            if (Settings.Theme == Settings.SupportedThemes.Dark)
            {
               if (Xamarin.Forms.Application.Current.Resources.TryGetValue("DarkThemePrimary", out backgroundColor))
               {
                  UIColor color = UIColor.FromRGBA(
                     Convert.ToInt32(((Color)backgroundColor).R * 255),
                     Convert.ToInt32(((Color)backgroundColor).G * 255),
                     Convert.ToInt32(((Color)backgroundColor).B * 255),
                     Convert.ToInt32(((Color)backgroundColor).A * 255));

                  // Use tint color to change the cursor's color
                  Control.TintColor = color;
               }
            }
            else
            {
               if (Xamarin.Forms.Application.Current.Resources.TryGetValue("LightThemePrimary", out backgroundColor))
               {
                  UIColor color = UIColor.FromRGBA(
                     Convert.ToInt32(((Color)backgroundColor).R * 255),
                     Convert.ToInt32(((Color)backgroundColor).G * 255),
                     Convert.ToInt32(((Color)backgroundColor).B * 255),
                     Convert.ToInt32(((Color)backgroundColor).A * 255));

                  // Use tint color to change the cursor's color
                  Control.TintColor = color;
               }
            }
         }
      }
   }
}