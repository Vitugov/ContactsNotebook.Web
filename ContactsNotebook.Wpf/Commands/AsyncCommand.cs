using System.Windows.Input;

namespace ContactsNotebook.Wpf.Commands
{
    public abstract class AsyncCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add { CommandManager.RequerySuggested += value; }
            remove { CommandManager.RequerySuggested -= value; }
        }
        private bool _isExecuting;
        protected bool IsExecuting
        {
            get => _isExecuting;
            private set
            {
                _isExecuting = value;
                RaiseCanExecuteChanged();
            }
        }

        public bool CanExecute(object? parameter)
        {
            return !IsExecuting && CanExecuteAsync(parameter).GetAwaiter().GetResult();
        }

        public async void Execute(object? parameter)
        {
            IsExecuting = true;
            await ExecuteAsync(parameter);
            IsExecuting = false;
        }

        public virtual Task<bool> CanExecuteAsync(object? parameter)
        {
            return Task.FromResult(true);
        }

        public abstract Task ExecuteAsync(object? parameter);

        protected void RaiseCanExecuteChanged()
        {
            CommandManager.InvalidateRequerySuggested();
        }
    }
}
