using System;
using System.Windows.Input;

namespace SilverlightValidation.Commands
{
    public class RelayCommand : ICommand
    {
        public event EventHandler CanExecuteChanged = delegate { };

        readonly Action<object> _execute;
        readonly Predicate<object> _canExecute;

        public RelayCommand(Action<object> execute,
                            Predicate<object> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException("execute");

            _execute = execute;
            _canExecute = canExecute;
        }


        public void UpdateCanExecuteCommand()
        {
            CanExecuteChanged(this, new EventArgs());
        }


        public bool CanExecute(object parameter)
        {
            return _canExecute == null || _canExecute(parameter);
        }


        public void Execute(object parameter)
        {
            _execute(parameter);
        }
    }
}
