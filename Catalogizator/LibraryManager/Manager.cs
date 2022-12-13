using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Input;

namespace Catalogizator.LibraryManager
{
    abstract class Manager : ICommand
    {
        public event EventHandler? CanExecuteChanged;

        bool ICommand.CanExecute(object? parameter)
        {
            return CanExecute();
        }

        void ICommand.Execute(object? parameter)
        {
            Execute();
        }

        protected virtual void OnCanExecuteChanged(EventArgs e)
        {
            CanExecuteChanged?.Invoke(this, e);
        }

        protected virtual bool CanExecute()
        {
            return true;
        }

        protected abstract void Execute();
    }
}
