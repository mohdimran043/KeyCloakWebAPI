using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace KeyCloak
{
    
//{
// 'client_id': 'demo-app',
//    'username': 'imu1',
//    'password': 'admin',
//'grant_type': 'password'
//,'client_secret':''

//}


//http://localhost:50883/api/values
//http://localhost:8080/auth/realms/master/protocol/openid-connect/token
    public class Program
    {
        public static void Main(string[] args)
        {
            BuildWebHost(args).Run();
        }

        public static IWebHost BuildWebHost(string[] args) =>
            WebHost.CreateDefaultBuilder(args)
                .UseStartup<Startup>()
                .Build();
    }
}
