namespace BasicWpfLibrary;

using System;
using System.Windows.Input;

public class RelayCommand : ICommand
{
    #region Fields 
    readonly Action<object?> _execute;
    readonly Predicate<object?>? _canExecute;
    #endregion // Fields 

    #region Constructors 
    public RelayCommand(Action<object?> execute) : this(execute, null) { }
    public RelayCommand(Action<object?> execute, Predicate<object?>? canExecute)
    {
        if (execute == null)
            throw new ArgumentNullException("execute");
        _execute = execute; _canExecute = canExecute;
    }
    #endregion // Constructors 
    #region ICommand Members 


    public bool CanExecute(object? parameter)
    {
        return _canExecute == null ? true : _canExecute(parameter);
    }

    public event EventHandler? CanExecuteChanged
    {
        add { CommandManager.RequerySuggested += value; }
        remove { CommandManager.RequerySuggested -= value; }
    }

    public void Execute(object? parameter) { _execute(parameter); }
    #endregion // ICommand Members 
}


public class RelayCommand<T> : ICommand
{
    private readonly Action<T> _execute;
    private readonly Func<T, bool>? _canExecute;

    public RelayCommand(Action<T> execute, Func<T, bool>? canExecute = null)
    {
        _execute = execute ?? throw new ArgumentNullException(nameof(execute));
        _canExecute = canExecute;
    }

    public event EventHandler? CanExecuteChanged
    {
        add => CommandManager.RequerySuggested += value;
        remove => CommandManager.RequerySuggested -= value;
    }

    public bool CanExecute(object? parameter)
    {
        if (_canExecute == null) return true;
        if (parameter == null && typeof(T).IsValueType) return false;

        if(parameter is not T)
        {
            throw new ArgumentException($"Parameter is not of type {typeof(T).Name}", nameof(parameter));
        }
        return _canExecute((T)parameter);
    }

    public void Execute(object? parameter)
    {
        if (parameter is not T)
        {
            throw new ArgumentException($"Parameter is not of type {typeof(T).Name}", nameof(parameter));
        }
        _execute((T)parameter);
    }
}
