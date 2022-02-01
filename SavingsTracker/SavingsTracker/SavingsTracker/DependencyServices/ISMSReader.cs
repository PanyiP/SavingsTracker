using SavingsTracker.Models;
using System.Collections.Generic;

namespace SavingsTracker.DependencyServices
{
   public interface ISMSReader
   {
      /// <summary>
      /// Parses all SMSs on the phone
      /// </summary>
      /// <returns>Returns the list of all SMSs</returns>
      List<SMS> ParseAllSMS();

      /// <summary>
      /// Initializes the object
      /// </summary>
      void Init();
   }
}
