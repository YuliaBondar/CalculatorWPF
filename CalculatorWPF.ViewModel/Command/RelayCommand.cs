//using System.Windows.Input;

//namespace CalculatorWPF.Command
//{
//    public class RelayCommand<T> : ICommand
//    {
//        private readonly Action<T> _execute;
//        private readonly Predicate<T>? _canExecute;

//        public RelayCommand(Action<T> execute, Predicate<T>? canExecute = null)
//        {
//            _execute = execute ?? throw new ArgumentNullException(nameof(execute));
//            _canExecute = canExecute;
//        }

//        public bool CanExecute(object? parameter)
//        {
//            if (parameter is T t)
//                return _canExecute == null || _canExecute(t);
//            return false;
//        }

//        public void Execute(object? parameter)
//        {
//            if (parameter is T t)
//                _execute(t);
//        }

//        public event EventHandler? CanExecuteChanged;

//        public void RaiseCanExecuteChanged()
//        {
//            CanExecuteChanged?.Invoke(this, EventArgs.Empty);
//        }

//    }
//}
