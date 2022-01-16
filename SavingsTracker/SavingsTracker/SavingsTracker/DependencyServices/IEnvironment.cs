using System.Drawing;

namespace SavingsTracker.DependencyServices
{
   public interface IEnvironment
   {
      void SetStatusBarColor(Color color, bool darkStatusBarTint);
   }
}
