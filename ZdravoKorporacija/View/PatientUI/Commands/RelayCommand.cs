using System;
using System.Windows.Input;

namespace ZdravoKorporacija.View.PatientUI.Commands
{
    public class RelayCommand : ICommand
    {
        private Action<object>? executeAction;
        private Predicate<object>? canExecute;

        public RelayCommand(Action<object>? execute)
        {
            executeAction = execute;
        }
        public RelayCommand(Action<object> execute, Predicate<object> canExec)
        {
            executeAction = execute;
            canExecute = canExec;
        }

        public bool CanExecute(object? parameter)
        {
            if (canExecute == null)
                return true;
            return canExecute(parameter);
        }

        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public void Execute(object? parameter)
        {
            executeAction(parameter);
        }
    }
}
