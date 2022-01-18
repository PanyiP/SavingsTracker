//TODO: Theme: Implement MyRadioButtonRenderer for iOS
/* There is no radio button in iOS
using SavingsTracker.CustomControls;
using SavingsTracker.iOS.Renderers;
using SavingsTracker.Services;
using UIKit;
using System;
using Xamarin.Forms;
using Xamarin.Forms.Platform.iOS;

[assembly: ExportRenderer(typeof(MyRadioButton), typeof(MyRadioButtonRenderer))]
namespace SavingsTracker.iOS.Renderers
{
   /// <summary>
   /// Custom renderer class for the custom RadioButton view MyRadioButton
   /// </summary>
   public class MyRadioButtonRenderer : RadioButtonRenderer
   {
      /// <summary>
      /// Sets theme specific button color for the view
      /// </summary>
      /// <param name="e"></param>
      protected override void OnElementChanged(ElementChangedEventArgs<Entry> e)
      {
         base.OnElementChanged(e);

         if (Control != null)
         {
            // do whatever you want to the UITextField here!
            Control.BackgroundColor = UIColor.FromRGB(204, 153, 255);
            Control.BorderStyle = UITextBorderStyle.Line;
         }
      }
   }
   
}
*/