using Launcher.WPF.Models;
using Launcher.WPF.Stuff;
using Prism.Commands;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Windows;

namespace Launcher.WPF.ViewModels
{
    class LoginVM : BindableBase
    {
        private LoginModel loginModel = new LoginModel();
        // TODO Browser visibility
        public Visibility BrowserVisibility => Visibility.Hidden;
        public LoginVM()
        {
            LoginCommand = new AsyncDelegateCommand(() => loginModel.Authorize());
        }
        public AsyncDelegateCommand LoginCommand { get; set; }
    }
}
