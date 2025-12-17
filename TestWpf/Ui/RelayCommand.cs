using System;
using System.Windows.Input;

namespace TestWpf.Ui
{
    /// <summary>
    /// A minimal ICommand implementation for ViewModels.
    /// Supports executing methods with or without parameters.
    /// </summary>
    public class RelayCommand : ICommand
    {
        private readonly Action<object> _execute;
        private readonly Predicate<object> _canExecute;

        public event EventHandler CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }

        /// <summary>
        /// Creates a new command.
        /// </summary>
        /// <param name="execute">The execution logic.</param>
        /// <param name="canExecute">The execution status logic.</param>
        public RelayCommand(Action<object> execute, Predicate<object> canExecute = null)
        {
            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
            _canExecute = canExecute;
        }

        // Convenience constructor for methods that don't need parameters
        public RelayCommand(Action execute, Func<bool> canExecute = null)
        {
            if (execute == null) throw new ArgumentNullException(nameof(execute));
            _execute = _ => execute();
            if (canExecute != null) _canExecute = _ => canExecute();
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