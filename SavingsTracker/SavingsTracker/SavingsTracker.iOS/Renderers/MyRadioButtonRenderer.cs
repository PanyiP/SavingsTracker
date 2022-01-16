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
   public class MyRadioButtonRenderer : RadioButtonRenderer
   {
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