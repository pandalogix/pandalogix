using Microsoft.AspNetCore;

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
                .UseKestrel(options =>
                {
                  options.Listen(System.Net.IPAddress.Any, 80);
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
