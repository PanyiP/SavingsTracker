using Android;
using Android.App;
using Android.Content;
using Android.OS;
using Android.Runtime;
using Android.Util;
using Android.Views;
using Android.Widget;
using AndroidX.Core.App;
using Google.Android.Material.Snackbar;
using Plugin.CurrentActivity;
using SavingsTracker.DependencyServices;
using SavingsTracker.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Xamarin.Essentials;
using Xamarin.Forms;

[assembly: Dependency(typeof(SavingsTracker.Droid.DependencyServices.SMSReader))]
namespace SavingsTracker.Droid.DependencyServices
{
   /// <summary>
   /// Parse SMSs from the phone
   /// </summary>
   public class SMSReader : ISMSReader
   {
      /// <summary>
      /// Store if we have the 'Manifest.Permission.ReadSms' permission
      /// </summary>
      private bool readSMSPermission = false;
      /// <summary>
      /// List of the parsed SMSs
      /// </summary>
      private List<SMS> SMSs;

      /// <summary>
      /// Initializes the object
      /// </summary>
      /// <param name="bankName">Bank name</param>
      /// <param name="phoneNumbers">List of phone numbers belonging to the bank</param>
      public void Init()
      {
         readSMSPermission = CheckPermission(Manifest.Permission.ReadSms, 0, this.GetType().FullName);
         SMSs = new List<SMS>();
      }

      /// <summary>
      /// Checks if a permission is available and if not it asks for the permission
      /// </summary>
      /// <param name="permission">Example: Manifest.Permission.ReadSms</param>
      /// <param name="requestLocation">An integer identifier for the permission request</param>
      /// <param name="callerClassName">The caller's parent class name for debugging purposes only</param>
      /// <returns>Returns true if the 'string permission' input paramter is already granted when this function is called, otherwise false</returns>
      private bool CheckPermission(string permission, int requestLocation, string callerClassName)
      {// more info: https://docs.microsoft.com/en-us/xamarin/android/app-fundamentals/permissions?tabs=windows
         if (AndroidX.Core.Content.ContextCompat.CheckSelfPermission(Android.App.Application.Context, permission) == (int)Android.Content.PM.Permission.Granted)
         {
            // We have permission
            return true;
         }
         else
         {
            // Permission is not granted.
            // If necessary display rationale & request.
            if (ActivityCompat.ShouldShowRequestPermissionRationale(Platform.CurrentActivity, permission))
            {
               // Provide an additional rationale to the user if the permission was not granted
               // and the user would benefit from additional context for the use of the permission.
               // For example if the user has previously denied the permission.
               Log.Info(callerClassName, $"Displaying {permission} permission rationale to provide additional context.");

               Activity activity = CrossCurrentActivity.Current.Activity; //https://github.com/jamesmontemagno/CurrentActivityPlugin
               if (activity != null)
               {
                  Android.Views.View view = activity.FindViewById(Android.Resource.Id.Content);
                  Snackbar.Make(view,
                                 $"{permission} is required to use this feature.",
                                 Snackbar.LengthIndefinite)
                          .SetAction("Ok",
                                     new Action<Android.Views.View>(delegate (Android.Views.View obj)
                                     {
                                        ActivityCompat.RequestPermissions(Platform.CurrentActivity, new String[] { permission }, requestLocation);
                                     }))
                          .Show();
               }
            }
            else
            {
               ActivityCompat.RequestPermissions(Platform.CurrentActivity, new String[] { permission }, requestLocation);
            }

            return false;
         }
      }

      /// <summary>
      /// Parses all SMSs on the phone
      /// </summary>
      /// <returns>Returns the list of all SMSs</returns>
      public List<SMS> ParseAllSMS()
      {
         readSMSPermission = CheckPermission(Manifest.Permission.ReadSms, 0, this.GetType().FullName);

         if (readSMSPermission)
         {
            SMSs.Clear();

            string INBOX = "content://sms/inbox";
            string[] reqCols = new string[] { "_id", "thread_id", "address", "person", "date", "body", "type" };
            Android.Net.Uri uri = Android.Net.Uri.Parse(INBOX);

            var cursor = Android.App.Application.Context.ContentResolver.Query(uri, reqCols, null, null);

            if (cursor.MoveToFirst())
            {
               do
               {
                  SMSs.Add(new SMS
                  {
                     Id = cursor.GetString(cursor.GetColumnIndex(reqCols[0])),
                     ThreadId = cursor.GetString(cursor.GetColumnIndex(reqCols[1])),
                     Address = cursor.GetString(cursor.GetColumnIndex(reqCols[2])),
                     Name = cursor.GetString(cursor.GetColumnIndex(reqCols[3])),
                     Date = DateTimeOffset.FromUnixTimeMilliseconds(long.Parse(cursor.GetString(cursor.GetColumnIndex(reqCols[4])))).UtcDateTime,
                     Body = cursor.GetString(cursor.GetColumnIndex(reqCols[5])),
                     Type = cursor.GetString(cursor.GetColumnIndex(reqCols[6]))
                  });
               } while (cursor.MoveToNext());
            }
         }

         return SMSs;
      }
   }
}