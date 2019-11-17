using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace DesignPatterns.Client.View
{
    class Command:ICommand
    {
        public Action<object> execute;
        public Predicate<object> canExecute;

        public Command(Action<object> action, Predicate<object> predicate = null)
        {
            execute = action;
            canExecute = predicate;
        }

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        public bool CanExecute(object parameter)
        {
            if (canExecute != null)
                return canExecute.Invoke(parameter);
            return true;
        }

        public void Execute(object parameter)
        {
            execute?.Invoke(parameter);
        }
    }
}
