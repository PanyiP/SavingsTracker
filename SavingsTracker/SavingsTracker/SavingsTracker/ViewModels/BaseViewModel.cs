using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace SavingsTracker.ViewModels
{
   /// <summary>
   /// Base View Model every view model should inherit from. It implements the INotifyPropertyChanged interface.
   /// </summary>
   public class BaseViewModel : INotifyPropertyChanged
   {
      bool isBusy = false;
      /// <summary>
      /// Property to store if the VM is busy
      /// </summary>
      public bool IsBusy
      {
         get { return isBusy; }
         set { SetProperty(ref isBusy, value); }
      }

      /// <summary>
      /// Sets property by invoking the OnPropertyChanged event
      /// </summary>
      /// <typeparam name="T"></typeparam>
      /// <param name="backingStore">A reference to the backingstore of the property</param>
      /// <param name="value">The value to be set</param>
      /// <param name="propertyName">Optional: Name of the property</param>
      /// <param name="onChanged">Optional: Additional method to be called before raising the OnPropertyChanged event</param>
      /// <returns></returns>
      protected bool SetProperty<T>(ref T backingStore, T value,
          [CallerMemberName] string propertyName = "",
          Action onChanged = null)
      {
         if (EqualityComparer<T>.Default.Equals(backingStore, value))
            return false;

         backingStore = value;
         onChanged?.Invoke();
         OnPropertyChanged(propertyName);
         return true;
      }

      #region INotifyPropertyChanged
      public event PropertyChangedEventHandler PropertyChanged;
      protected void OnPropertyChanged([CallerMemberName] string propertyName = "")
      {
         var changed = PropertyChanged;
         if (changed == null)
            return;

         changed.Invoke(this, new PropertyChangedEventArgs(propertyName));
      }
      #endregion
   }
}
