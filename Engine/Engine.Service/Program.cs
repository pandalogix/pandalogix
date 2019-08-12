using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore;
using Microsoft.AspNetCore.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Logging;

namespace Engine.Service
{
  public class Program
  {
    public static void Main(string[] args)
    {
      CreateWebHostBuilder(args).Build().Run();
    }

    public static IWebHostBuilder CreateWebHostBuilder(string[] args) =>
        WebHost.CreateDefaultBuilder(args)
                .UseKestrel(options=>{
              options.Listen(System.Net.IPAddress.Loopback,3003);
            })
            .UseStartup<Startup>()
            .UseContentRoot(Directory.GetCurrentDirectory())
            .ConfigureLogging((hostingContext, builder) =>
            {
              builder.AddConfiguration(hostingContext.Configuration.GetSection("Logging"));
              builder.AddConsole();
              builder.AddDebug();
            })
            .ConfigureAppConfiguration((builderContext, config) =>
            {
              config.AddJsonFile("settings.json");
              config.AddEnvironmentVariables();
            });
  }
}
