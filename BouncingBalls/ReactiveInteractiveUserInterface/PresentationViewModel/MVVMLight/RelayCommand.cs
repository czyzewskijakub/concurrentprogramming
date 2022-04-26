using System.Windows.Input;

namespace PresentationViewModel.MVVMLight
{
    public sealed class RelayCommand : ICommand
    {
        #region constructors

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _mExecute = execute ?? throw new ArgumentNullException(nameof(execute));
            _mCanExecute = canExecute;
        }

        #endregion constructors

        #region ICommand
        
        public bool CanExecute(object parameter)
        {
            return _mCanExecute == null || _mCanExecute();
        }
        
        public void Execute(object parameter)
        {
            _mExecute();
        }
        
        public event EventHandler CanExecuteChanged;

        #endregion ICommand

        #region API
        
        internal void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion API

        #region private

        private readonly Action _mExecute;
        private readonly Func<bool> _mCanExecute;

        #endregion private
    }
}