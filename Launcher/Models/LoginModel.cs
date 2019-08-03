using IdentityModel.OidcClient;
using Launcher.WPF.Services;
using Prism.Mvvm;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using System.Windows;

namespace Launcher.WPF.Models
{
    class LoginModel : BindableBase
    {
        private bool isAuthorized = false;

        public bool IsAuthorized { get => isAuthorized; private set => SetProperty(ref isAuthorized, value); }
        public async Task Authorize()
        {
            IsAuthorized = false;

            var oidcClient = AppServiceProvider.Instance.GetService<OidcClient>();

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
                MessageBox.Show($"Hello {name} {result.RefreshTokenHandler}");
                IsAuthorized = true;
            }
        }
    }
}
