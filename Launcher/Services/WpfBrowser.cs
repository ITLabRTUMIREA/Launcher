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

            var browser = new WebView();

            var signal = new SemaphoreSlim(0, 1);

            var result = new BrowserResult
            {
                ResultType = BrowserResultType.UserCancel
            };

            browser.DOMContentLoaded += (s, e) =>
            {
                Log($"Loaded {e?.ToString()}");

            };

            browser.Loaded += (s, e) =>
            {
                Log($"Loaded {e?.ToString()}");

            };

            browser.NavigationCompleted += (s, e) =>
            {
                Log($"NavigationCompleted {e.Uri.AbsoluteUri}");

            };
            browser.NavigationStarting += (s, e) =>
            {
                Log($"NavigationStarting {e.Uri.AbsoluteUri}");
                if (!e.Uri.AbsoluteUri.StartsWith(options.EndUrl))
                    return;
                e.Cancel = true;

                var responseData = GetResponseDataFromFormPostPage(browser);
                result = new BrowserResult
                {
                    ResultType = BrowserResultType.Success,
                    Response = responseData
                };

                signal.Release();
                window.Close();
            };

            window.Closing += (s, e) => signal.Release();

            window.Content = browser;

            window.Show();
            browser.Navigate(new Uri(options.StartUrl));

            await signal.WaitAsync();

            return result;
        }

        private string GetResponseDataFromFormPostPage(WebView webBrowser)
        {
            return "?data=123";

            //var document = (IHTMLDocument3)webBrowser.Document;
            //var inputElements = document.getElementsByTagName("INPUT").OfType<IHTMLElement>();
            //var resultUrl = "?";

            //foreach (var input in inputElements)
            //{
            //    resultUrl += input.getAttribute("name") + "=";
            //    resultUrl += input.getAttribute("value") + "&";
            //}

            //resultUrl = resultUrl.TrimEnd('&');

            //return resultUrl;
        }

        private object locker = new object();
        private void Log(string message)
        {
            lock(locker)
            {
                File.AppendAllLines(@"log.txt", new string[] { message });
            }
        }
    }
}
