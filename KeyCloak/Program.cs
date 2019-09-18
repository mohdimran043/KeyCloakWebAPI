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
    //http://localhost:3000/api/values
    //http://localhost:8080/auth/realms/master/protocol/openid-connect/token

    //Approach 2 
    // CALL THE BELOW URL IN POSTMAN AND SPECIFY "AUTHORIZATION" as "BEARER" AND THEN CLICK ON PREVIEW
    // COPY THE ID_TOKEN FROM THE ANGULAR APP(f12 and copy) AND CALL THE CODE 
    // IT SHOULD RETURN THE RESULT
    //http://localhost:3000/api/values


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
