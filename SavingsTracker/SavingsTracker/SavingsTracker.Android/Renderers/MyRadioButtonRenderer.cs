using Android.Content;
using Android.Content.Res;
using SavingsTracker.CustomControls;
using SavingsTracker.Droid.Renderers;
using SavingsTracker.Services;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;

[assembly: ExportRenderer(typeof(MyRadioButton), typeof(MyRadioButtonRenderer))]
namespace SavingsTracker.Droid.Renderers
{
   /// <summary>
   /// Custom renderer class for the custom RadioButton view MyRadioButton
   /// </summary>
   public class MyRadioButtonRenderer : RadioButtonRenderer
   {
      /// <summary>
      /// Constructor
      /// </summary>
      /// <param name="context"></param>
      public MyRadioButtonRenderer(Context context) : base(context)
      {
      }

      /// <summary>
      /// Sets theme specific button color for the view
      /// </summary>
      /// <param name="e"></param>
      protected override void OnElementChanged(ElementChangedEventArgs<RadioButton> e)
      {
         base.OnElementChanged(e);
         //TODO: Theme: Radio button button color not changing upon theme change.
         //TODO: Theme: Radio button underlining should change color according to the theme.

         if (Control != null)
         {
            object buttonColor;

            if (Settings.Theme == Settings.SupportedThemes.Dark)
            {
               if (Application.Current.Resources.TryGetValue("DarkThemePrimary", out buttonColor))
               {
                  Control.ButtonTintList = ColorStateList.ValueOf(Android.Graphics.Color.Argb(
                     Convert.ToInt32(((Color)buttonColor).A * 255),
                     Convert.ToInt32(((Color)buttonColor).R * 255),
                     Convert.ToInt32(((Color)buttonColor).G * 255),
                     Convert.ToInt32(((Color)buttonColor).B * 255)
                     ));
               }
            }
            else
            {
               if (Application.Current.Resources.TryGetValue("LightThemePrimary", out buttonColor))
               {
                  Control.ButtonTintList = ColorStateList.ValueOf(Android.Graphics.Color.Argb(
                     Convert.ToInt32(((Color)buttonColor).A * 255),
                     Convert.ToInt32(((Color)buttonColor).R * 255),
                     Convert.ToInt32(((Color)buttonColor).G * 255),
                     Convert.ToInt32(((Color)buttonColor).B * 255)
                     ));
               }
            }
         }
      }
   }
}