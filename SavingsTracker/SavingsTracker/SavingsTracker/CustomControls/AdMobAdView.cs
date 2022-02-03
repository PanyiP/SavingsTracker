using Xamarin.Forms;

namespace SavingsTracker.CustomControls
{
   public class AdMobAdView : View
   {
      public enum Sizes { Banner, LargeBanner, MediumRectangle, FullBanner, Leaderboard }

      public static readonly BindableProperty SizeProperty = BindableProperty.Create(
               nameof(Size),
               typeof(Sizes),
               typeof(AdMobAdView),
               Sizes.Banner);
      /// <summary>
      /// Banner size
      /// </summary>
      public Sizes Size
      {
         get { return (Sizes)GetValue(SizeProperty); }
         set { SetValue(SizeProperty, value); }
      }


      public static readonly BindableProperty AdUnitIdProperty = BindableProperty.Create(
               nameof(AdUnitId),
               typeof(string),
               typeof(AdMobAdView),
               string.Empty);
      /// <summary>
      /// AdUnitId from Google AdMob
      /// </summary>
      public string AdUnitId
      {
         get { return (string)GetValue(AdUnitIdProperty); }
         set { SetValue(AdUnitIdProperty, value); }
      }

      public AdMobAdView()
      {
         this.BackgroundColor = Color.Transparent;
      }
   }
}
