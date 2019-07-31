using IdentityModel.OidcClient;
using Launcher.WPF.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace Launcher.WPF
{
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();
            Loaded += StartAsync;
        }

        private async void StartAsync(object sender, RoutedEventArgs e)
        {
            var options = new OidcClientOptions
            {
                Authority = "http://127.0.0.1:5000",
                ClientId = "launcher",
                Scope = "games",
                RedirectUri = "http://127.0.0.1/callback",
                ResponseMode = OidcClientOptions.AuthorizeResponseMode.FormPost,
                Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
                Browser = new WpfBrowser()
            };
            var oidcClient = new OidcClient(options);

            LoginResult result;

            try
            {
                result = await oidcClient.LoginAsync(new LoginRequest
                {
                    BrowserDisplayMode = IdentityModel.OidcClient.Browser.DisplayMode.Visible,
                    BrowserTimeout = 300
                });
            }
            catch (Exception ex)
            {
                MessageBox.Show($"exception {ex.Message}");
                return;
            }
            if (result.IsError)
            {
                MessageBox.Show(result.Error == "UserCancel" ? "The sign-in window was closed before authorization was completed." : result.Error);
            }
            else
            {
                var name = result.User.Identity.Name;
                MessageBox.Show($"Hello {name}");
            }
        }
    }
}
