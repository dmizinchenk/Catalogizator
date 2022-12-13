using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Catalogizator.LibraryManager
{
    class AddCommand : Manager, INotifyPropertyChanged
    {
        Func<bool> canExecute;
        Action execute;

        //public Func<bool> CanExecuteChange
        //{
        //    get => canExecute;
        //    set
        //    {
        //        canExecute = value;
        //        OnPropertyChenged(nameof(CanExecuteChange));
        //    }
        //}

        public event PropertyChangedEventHandler? PropertyChanged;
        void OnPropertyChenged(string property)
        {
            PropertyChanged?.Invoke(this, new PropertyChangedEventArgs(property));
        }

        public AddCommand(Action execute, Func<bool> canExecute)
        {
            this.canExecute = canExecute;
            this.execute = execute;
        }
        public AddCommand(Action execute) : this(execute, () => true) { }
        protected override void Execute()
        {
            execute();
        }
        protected override bool CanExecute()
        {
            return canExecute();
        }
    }
}
