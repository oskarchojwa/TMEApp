using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows.Input;

namespace TME_App.VievModel
{
    public interface IAsyncRelayCommand : ICommand
    {
        bool IsExecuting { get; }

        bool CanExecute();
        Task ExecuteAsync();
        Task ExecuteAsync(CancellationToken cancellationToken);
        Task ExecuteAsync(object parameter);
        Task ExecuteAsync(object parameter, CancellationToken cancellationToken);

        void InvalidateCommand();
    }
    public class AsyncRelayCommand : IAsyncRelayCommand
    {
        public bool IsExecuting => this.executionCount > 0;

        public object CanExecuteChangedDelegate { get; private set; }

        protected readonly Func<Task> ExecuteAsyncNoParam;
        protected readonly Action ExecuteNoParam;
        protected readonly Func<bool> CanExecuteNoParam;

        private readonly Func<object, Task> executeAsync;
        private readonly Action<object> execute;
        private readonly Predicate<object> canExecute;
        private EventHandler canExecuteChangedDelegate;
        private int executionCount;

        public event EventHandler CanExecuteChanged
        {
            add
            {
                CommandManager.RequerySuggested += value;
                this.canExecuteChangedDelegate = (EventHandler)Delegate.Combine(this.canExecuteChangedDelegate, value);
            }
            remove
            {
                CommandManager.RequerySuggested -= value;
                this.canExecuteChangedDelegate = (EventHandler)Delegate.Remove(this.canExecuteChangedDelegate, value);
            }
        }

        #region Constructors

        public AsyncRelayCommand(Action<object> execute)
          : this(execute, param => true)
        {
        }

        public AsyncRelayCommand(Action executeNoParam)
          : this(executeNoParam, () => true)
        {
        }

        public AsyncRelayCommand(Func<object, Task> executeAsync)
          : this(executeAsync, param => true)
        {
        }

        public AsyncRelayCommand(Func<Task> executeAsyncNoParam)
          : this(executeAsyncNoParam, () => true)
        {
        }

        public AsyncRelayCommand(Action executeNoParam, Func<bool> canExecuteNoParam)
        {
            this.ExecuteNoParam = executeNoParam ?? throw new ArgumentNullException(nameof(executeNoParam));
            this.CanExecuteNoParam = canExecuteNoParam ?? (() => true);
        }

        public AsyncRelayCommand(Action<object> execute, Predicate<object> canExecute)
        {
            this.execute = execute ?? throw new ArgumentNullException(nameof(execute));
            this.canExecute = canExecute ?? (param => true); ;
        }

        public AsyncRelayCommand(Func<Task> executeAsyncNoParam, Func<bool> canExecuteNoParam)
        {
            this.ExecuteAsyncNoParam = executeAsyncNoParam ?? throw new ArgumentNullException(nameof(executeAsyncNoParam));
            this.CanExecuteNoParam = canExecuteNoParam ?? (() => true);
        }

        public AsyncRelayCommand(Func<object, Task> executeAsync, Predicate<object> canExecute)
        {
            this.executeAsync = executeAsync ?? throw new ArgumentNullException(nameof(executeAsync));
            this.canExecute = canExecute ?? (param => true); ;
        }

        #endregion Constructors


        public bool CanExecute() => CanExecute(null);

        public bool CanExecute(object parameter) => this.canExecute?.Invoke(parameter)
                                                    ?? this.CanExecuteNoParam?.Invoke()
                                                    ?? true;

        async void ICommand.Execute(object parameter) => await ExecuteAsync(parameter, CancellationToken.None);

        public async Task ExecuteAsync() => await ExecuteAsync(null, CancellationToken.None);

        public async Task ExecuteAsync(CancellationToken cancellationToken) => await ExecuteAsync(null, cancellationToken);

        public async Task ExecuteAsync(object parameter) => await ExecuteAsync(parameter, CancellationToken.None);

        public async Task ExecuteAsync(object parameter, CancellationToken cancellationToken)
        {
            try
            {
                Interlocked.Increment(ref this.executionCount);
                cancellationToken.ThrowIfCancellationRequested();

                if (this.executeAsync != null)
                {
                    await this.executeAsync.Invoke(parameter).ConfigureAwait(false);
                    return;
                }
                if (this.ExecuteAsyncNoParam != null)
                {
                    await this.ExecuteAsyncNoParam.Invoke().ConfigureAwait(false);
                    return;
                }
                if (this.ExecuteNoParam != null)
                {
                    this.ExecuteNoParam.Invoke();
                    return;
                }

                this.execute?.Invoke(parameter);
            }
            finally
            {
                Interlocked.Decrement(ref this.executionCount);
            }
        }

        public void InvalidateCommand()
        {
            throw new NotImplementedException();
        }

        /*public void InvalidateCommand() => OnCanExecuteChanged();

        protected virtual void OnCanExecuteChanged()
        {
            this.CanExecuteChangedDelegate?.Invoke(this, EventArgs.Empty);
        }*/
    }
}
