using System;
using System.Windows.Input;

namespace InsuranceV2.Common.MVVM
{
    public class ActionCommand : ICommand
    {
        private readonly Action<object> _action;
        private readonly Predicate<object> _predicate;

        public ActionCommand(Action<object> action, Predicate<object> predicate = null)
        {
            if (action == null)
            {
                throw new ArgumentNullException(nameof(action), "You must specify an Action<T>.");
            }

            _action = action;
            _predicate = predicate;
        }

        public event EventHandler CanExecuteChanged;

        public bool CanExecute(object parameter)
        {
            if (_predicate == null)
            {
                return true;
            }
            return _predicate(parameter);
        }

        public void Execute(object parameter = null)
        {
            _action(parameter);
        }
    }
}