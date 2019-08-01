using IdentityModel.OidcClient;
using Launcher.WPF.Options;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Options;
using System;
using System.Collections.Generic;
using System.Text;

namespace Launcher.WPF.Services
{
    class AppServiceProvider
    {
        private readonly IServiceProvider serviceProvider;

        private AppServiceProvider()
        {
            serviceProvider = BuildServices();
        }

        private static readonly Lazy<AppServiceProvider> instance 
            = new Lazy<AppServiceProvider>(() => new AppServiceProvider());

        public static AppServiceProvider Instance => instance.Value;

        public T GetService<T>() => serviceProvider.GetService<T>();

        private IConfiguration BuildConfiguration()
        {
            return new ConfigurationBuilder()
                .AddJsonFile("appsettings.json")
                .Build();
        }

        private IServiceProvider BuildServices()
        {
            var configuration = BuildConfiguration();
            var services = new ServiceCollection();
            services.AddSingleton(configuration);
            services.Configure<IdentityOptions>(configuration.GetSection(nameof(IdentityOptions)));


            services.AddSingleton<OidcClient>(sp =>
            {
                var options = sp.GetRequiredService<IOptions<IdentityOptions>>().Value;

                return new OidcClient(new OidcClientOptions
                {
                    Authority = options.Authority,
                    ClientId = options.ClientId,
                    Scope = options.Scope,
                    RedirectUri = options.RedirectUri,
                    ResponseMode = OidcClientOptions.AuthorizeResponseMode.Redirect,
                    Flow = OidcClientOptions.AuthenticationFlow.AuthorizationCode,
                    Browser = new WpfBrowser(),
                });
            });

            return services.BuildServiceProvider();
        }
    }
}
