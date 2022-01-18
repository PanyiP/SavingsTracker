using Android.Content;
using Android.Content.Res;
using SavingsTracker.CustomControls;
using SavingsTracker.Droid.Renderers;
using SavingsTracker.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyEntry), typeof(MyEntryRenderer))]
namespace SavingsTracker.Droid.Renderers
{
   /// <summary>
   /// Custom renderer class for the custom Entry view MyEntry
   /// </summary>
   internal class MyEntryRenderer : EntryRenderer
   {
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="context"></param>
      public MyEntryRenderer(Context context) : base(context)
      {
      }

      /// <summary>
      /// Sets theme specific background color for the view
      /// </summary>
      /// <param name="e"></param>
      protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
      {//TODO: Theme: Entry cursor color should be set according to the theme.
         base.OnElementChanged(e);

         if (Control != null)
         {
            object backgroundColor;

            if (Settings.Theme == Settings.SupportedThemes.Dark)
            {
               if (Application.Current.Resources.TryGetValue("DarkThemePrimary", out backgroundColor))
               {
                  Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Argb(
                     Convert.ToInt32(((Color)backgroundColor).A * 255),
                     Convert.ToInt32(((Color)backgroundColor).R * 255),
                     Convert.ToInt32(((Color)backgroundColor).G * 255),
                     Convert.ToInt32(((Color)backgroundColor).B * 255)
                     ));
               }
            }
            else
            {
               if (Application.Current.Resources.TryGetValue("LightThemePrimary", out backgroundColor))
               {
                  Control.BackgroundTintList = ColorStateList.ValueOf(Android.Graphics.Color.Argb(
                     Convert.ToInt32(((Color)backgroundColor).A * 255),
                     Convert.ToInt32(((Color)backgroundColor).R * 255),
                     Convert.ToInt32(((Color)backgroundColor).G * 255),
                     Convert.ToInt32(((Color)backgroundColor).B * 255)
                     ));
               }
            }
         }
      }
   }
}