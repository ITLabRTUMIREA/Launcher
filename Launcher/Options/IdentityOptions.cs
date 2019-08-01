using System;
using System.Collections.Generic;
using System.Text;

namespace Launcher.WPF.Options
{
    class IdentityOptions
    {
        public string Authority { get; set; }
        public string ClientId { get; set; }
        public string Scope { get; set; }
        public string RedirectUri { get; set; }
    }
}
