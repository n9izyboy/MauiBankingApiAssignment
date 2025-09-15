using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data;

namespace MauiBankingExercise.Configurations
{
    public class ApplicationSettings
    {
        public object ServiceUrl { get; internal set; }

        // https://localhost:7258/api/Accounts/customers/{customerId}
        public class ApiSettings
        {
            public string ServerName { get; set; }
            public int Port { get; set; }
            public string BaseRoute { get; set; }
            public string ServiceUrl { get; internal set; }

            public ApiSettings()
            {
                ServerName = "localhost";
#if DEBUG
                if (DeviceInfo.Platform == DevicePlatform.Android)
                {
                    ServerName = "10.0.2.2";
                }
#endif
                Port = 7258;
                BaseRoute = "api/Accounts/customers/{customerId}";
                ServiceUrl = $"https://{ServerName}:{Port}/{BaseRoute}";
            }
        }
    }
}
