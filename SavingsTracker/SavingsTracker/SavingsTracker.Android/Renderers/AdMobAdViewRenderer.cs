using Android.App;
using Android.Content;
using Android.Gms.Ads;
using Android.OS;
using Android.Runtime;
using Android.Views;
using Android.Widget;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Forms;
using Xamarin.Forms.Platform.Android;
using SavingsTracker.CustomControls;
using SavingsTracker.Droid.Renderers;

[assembly: ExportRenderer(typeof(AdMobAdView), typeof(AdMobAdViewRenderer))]
namespace SavingsTracker.Droid.Renderers
{
   public class AdMobAdViewRenderer : ViewRenderer
   {
      private readonly string defaultAdUnitId = "ca-app-pub-3940256099942544/6300978111"; // generic AdUnitId from google for testing purposes only
      private AdView adView;
      private double height;

      public AdMobAdViewRenderer(Context context) : base(context)
      {
      }

      protected override void OnElementChanged(ElementChangedEventArgs<Xamarin.Forms.View> e)
      {
         base.OnElementChanged(e);

         if (e.OldElement != null)
         {
            // Unsubscribe from event handlers and cleanup any resources
         }

         if (e.NewElement != null)
         {
            if (Control == null)
            {
               // Set properties of the view
               CreateAdView();

               // Instantiate the native control and assign it to the Control property with the SetNativeControl method
               SetNativeControl(adView);
            }

            // Configure the control and subscribe to event handlers
            e.NewElement.HeightRequest = height;
         }
      }

      AdView CreateAdView()
      {
         if (adView != null)
         {
            return adView;
         }

         adView = new AdView(Context);

         switch ((Element as AdMobAdView).Size)
         {
            case AdMobAdView.Sizes.Banner:
               adView.AdSize = AdSize.Banner;
               height = 50d;
               break;
            case AdMobAdView.Sizes.LargeBanner:
               adView.AdSize = AdSize.LargeBanner;
               height = 100d;
               break;
            case AdMobAdView.Sizes.MediumRectangle:
               adView.AdSize = AdSize.MediumRectangle;
               height = 250d;
               break;
            case AdMobAdView.Sizes.FullBanner:
               adView.AdSize = AdSize.FullBanner;
               height = 60d;
               break;
            case AdMobAdView.Sizes.Leaderboard:
               adView.AdSize = AdSize.Leaderboard;
               height = 90d;
               break;
            default:
               adView.AdSize = AdSize.Banner;
               height = 50d;
               break;
         }

         if ((Element as AdMobAdView).AdUnitId == string.Empty)
         {
            adView.AdUnitId = defaultAdUnitId;
         }
         else
         {
            adView.AdUnitId = (Element as AdMobAdView).AdUnitId;
         }

         adView.LayoutParameters = new LinearLayout.LayoutParams(LayoutParams.WrapContent, LayoutParams.WrapContent);

         adView.LoadAd(new AdRequest.Builder().Build());

         return adView;
      }
   }
}