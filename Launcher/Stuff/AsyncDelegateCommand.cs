using Prism.Commands;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace Launcher.WPF.Stuff
{
    class AsyncDelegateCommand : DelegateCommandBase
    {
        private readonly Func<Task> action;
        private bool isExecuting;

        public AsyncDelegateCommand(Func<Task> action)
        {
            this.action = action;
        }
        protected override bool CanExecute(object parameter)
        {
            return !isExecuting;
        }

        protected override async void Execute(object parameter)
        {
            if (isExecuting) return;
            try
            {
                isExecuting = true;
                RaiseCanExecuteChanged();
                await action();
            }
            finally
            {
                isExecuting = false;
                RaiseCanExecuteChanged();
            }

        }
    }
}
