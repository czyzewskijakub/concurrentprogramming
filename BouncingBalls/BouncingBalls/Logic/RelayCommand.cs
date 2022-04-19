using System;
using System.Windows.Input;

namespace BouncingBalls.Logic
{
    public class RelayCommand : ICommand
    {
        #region private

        private readonly Action _execute;
        private readonly Func<bool> _canExecute;

        #endregion private

        #region constructors

        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        #endregion constructors

        #region ICommand

        public bool CanExecute(object parameter)
        {
            if (_canExecute == null)
                return true;
            if (parameter == null)
                return _canExecute();
            return _canExecute();
        }

        public virtual void Execute(object parameter)
        {
            _execute();
        }

        public event EventHandler CanExecuteChanged;

        #endregion ICommand

        #region API

        internal void RaiseCanExecuteChanged()
        {
            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
        }

        #endregion API
    }
}
