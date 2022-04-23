using System.Collections.Specialized;
using System.ComponentModel;
using System.Runtime.CompilerServices;

namespace PresentationViewModel.MVVMLight
{
    public class ViewModelBase : INotifyPropertyChanged, INotifyCollectionChanged
    {
        #region INotify

        public event PropertyChangedEventHandler PropertyChanged;
        public event NotifyCollectionChangedEventHandler CollectionChanged;

        #endregion INotify

        #region API
        
        protected virtual void RaisePropertyChanged([CallerMemberName] string propertyName = null)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(propertyName));
        }

        protected virtual void RaiseCollectionChanged(NotifyCollectionChangedAction action)
        {
            CollectionChanged?.Invoke(this, new NotifyCollectionChangedEventArgs(action));
        }

        #endregion API
        
    }
}