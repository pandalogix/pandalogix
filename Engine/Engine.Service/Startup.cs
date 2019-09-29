using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EventBus;
using MediatR;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;

namespace Engine.Service
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    // For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
    public void ConfigureServices(IServiceCollection services)
    {
      //mvc
      services.AddMvc(options =>
      {

      }).AddJsonOptions(options =>
    {

      // options.SerializerSettings.Converters.Add(new Newtonsoft.Json.Converters.StringEnumConverter());
      // options.SerializerSettings.NullValueHandling = Newtonsoft.Json.NullValueHandling.Ignore;
    });
      services.AddHttpClient("padMgr", configureClient =>
      {
        configureClient.BaseAddress = new System.Uri(Configuration.GetValue<string>("ServiceEndPoints:PadManagerService"));
        // configureClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        configureClient.DefaultRequestHeaders.Add("X-Request-Source", Environment.MachineName);
      });
      services.AddCors(options =>
      {
        options.AddPolicy("CorsPolicy",
              builder => builder.AllowAnyOrigin()
              .AllowAnyMethod()
              .AllowAnyHeader()
              .AllowCredentials());
      });

      services.AddSwaggerGen(options =>
      {
        options.DescribeAllEnumsAsStrings();
        options.SwaggerDoc("v1", new Info
        {
          Title = "Engine Service API",
          Version = "v1",
          Description = "Engine Service API"
        });
      });

      services.AddMediatR(typeof(ExecuteCommandHandler));

      services.AddScoped(typeof(PadExecutionHandler), typeof(PadExecutionHandler));
      services.AddSingleton<IEventBusSubscriptionsManager, InMemoryEventBusSubscriptionsManager>();
      services.AddSingleton<IEventBus, InMemoryEventBus>();
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IEventBus evtBus)
    {
      // if (env.IsDevelopment())
      // {
      //   app.UseDeveloperExceptionPage();
      // }

      app.UseCors("CorsPolicy");

      app.UseMvc();

      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "Engine Service V1");
      });

      evtBus.Subscribe<PadExecution, PadExecutionHandler>();
    }
  }
}
