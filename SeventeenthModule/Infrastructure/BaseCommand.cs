using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace SeventeenthModule.Infrastructure
{
    /// <summary>
    /// Базовый класс для реализации команд во Вью
    /// </summary>
    internal abstract class BaseCommand : ICommand
    {
        public event EventHandler? CanExecuteChanged
        {
            add => CommandManager.RequerySuggested += value;
            remove => CommandManager.RequerySuggested -= value;
        }
        public abstract bool CanExecute(object? parameter);
        public abstract void Execute(object? parameter);
        
    }
}
