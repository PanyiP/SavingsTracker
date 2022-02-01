using System;
using System.Collections.Generic;
using System.Text;

namespace SavingsTracker.Models
{
   /// <summary>
   /// Class to store one SMS
   /// </summary>
   public class SMS
   {
      /// <summary>
      /// The Id of the SMS
      /// </summary>
      public string Id { get; set; }

      /// <summary>
      /// ???
      /// </summary>
      public string ThreadId { get; set; }

      /// <summary>
      /// The phone number of the sender
      /// </summary>
      public string Address { get; set; }

      /// <summary>
      /// ???
      /// </summary>
      public string Name { get; set; }

      /// <summary>
      /// The date of sending or receiving the SMS
      /// </summary>
      public DateTime Date { get; set; }

      /// <summary>
      /// The message part of the SMS
      /// </summary>
      public string Body { get; set; }

      /// <summary>
      /// ???
      /// </summary>
      public string Type { get; set; }
   }
}
