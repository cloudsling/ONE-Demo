using System;
using System.Threading;
using System.Windows.Input;

namespace Demo.Common
{
    public class CommandBase : ICommand
    {
        public Action<object> _excute;
        public Func<object, bool> _canexcute;
        public event EventHandler CanExecuteChanged;

        public CommandBase(Action<object> excute, Func<object, bool> canexcute)
        {
            _excute = excute;
            _canexcute = canexcute;
        }

        public CommandBase(Action<object> excute) : this(excute, b => true) { }
        
        public void OnCanExecuteChange()
        {
            var temp = Volatile.Read(ref CanExecuteChanged);
            if (temp != null)
            {
                temp(this, EventArgs.Empty);
            }
        }

        public bool CanExecute(object parameter)
        {
            return _canexcute(parameter);
        }

        public void Execute(object parameter)
        {
            _excute(parameter);
        }
    }
}
