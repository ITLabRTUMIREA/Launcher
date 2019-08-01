using IdentityModel.OidcClient.Browser;
using Microsoft.Toolkit.Wpf.UI.Controls;
using System;
using System.Collections.Generic;
using System.IO;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;

namespace Launcher.WPF.Services
{
    class WpfBrowser : IBrowser
    {
        public async Task<BrowserResult> InvokeAsync(BrowserOptions options)
        {
            var window = new Window
            {
                Width = 900,
                Height = 625,
                Title = "Вход в аккаунт"
            };

            var browser = new WebBrowser();

            var result = new BrowserResult
            {
                ResultType = BrowserResultType.UserCancel
            };

            using (var signal = new SemaphoreSlim(0, 1))
            {
                browser.Navigating += (s, e) =>
                {
                    if (!e.Uri.AbsoluteUri.StartsWith(options.EndUrl))
                        return;
                    e.Cancel = true;

                    result = new BrowserResult
                    {
                        ResultType = BrowserResultType.Success,
                        Response = e.Uri.Query
                    };

                    signal.Release();
                    window.Close();
                };

                window.Closing += (s, e) => signal.Release();

                window.Content = browser;

                window.Show();
                browser.Navigate(new Uri(options.StartUrl));

                await signal.WaitAsync();
            }

            return result;
        }
    }
}
