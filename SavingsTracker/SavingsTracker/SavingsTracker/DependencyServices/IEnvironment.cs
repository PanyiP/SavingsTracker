using System.Drawing;

namespace SavingsTracker.DependencyServices
{
   /// <summary>
   /// Interface for the platform specific Dependency service
   /// </summary>
   public interface IEnvironment
   {
      /// <summary>
      /// Sets the status bar color on the given platform
      /// </summary>
      /// <param name="color">The background color to be set</param>
      /// <param name="darkStatusBarTint">The foreground color of the status bar</param>
      void SetStatusBarColor(Color color, bool darkStatusBarTint);
   }
}
