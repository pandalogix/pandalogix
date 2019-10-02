using System;
using System.Net.Http;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.HttpsPolicy;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Swashbuckle.AspNetCore.Swagger;
using Microsoft.OpenApi.Models;
using Microsoft.Extensions.Hosting;

namespace WebSPA
{
  public class Startup
  {
    public Startup(IConfiguration configuration)
    {
      Configuration = configuration;
    }

    public IConfiguration Configuration { get; }

    // This method gets called by the runtime. Use this method to add services to the container.
    public void ConfigureServices(IServiceCollection services)
    {

      services.AddMvc(option => option.EnableEndpointRouting = false);

      // When working with hierarchical keys in environment variables, a colon separator (:) may not work on all platforms. A double underscore (__) is supported by all platforms and is replaced by a colon.
      services.AddHttpClient("accountMgr", configureClient =>
      {
        configureClient.BaseAddress = new System.Uri(Configuration.GetValue<string>("ServiceEndPoints:AccountService"));
        // configureClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        configureClient.DefaultRequestHeaders.Add("X-Request-Source", Environment.MachineName);
      });

      services.AddHttpClient("padMgr", configureClient =>
      {
        configureClient.BaseAddress = new System.Uri(Configuration.GetValue<string>("ServiceEndPoints:PadManagerService"));
        // configureClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        configureClient.DefaultRequestHeaders.Add("X-Request-Source", Environment.MachineName);
      });

      services.AddHttpClient("engineMgr", configureClient =>
      {
        configureClient.BaseAddress = new System.Uri(Configuration.GetValue<string>("ServiceEndPoints:EngineService"));
        // configureClient.DefaultRequestHeaders.Add("Content-Type", "application/json");
        configureClient.DefaultRequestHeaders.Add("X-Request-Source", Environment.MachineName);
      });
      // .ConfigurePrimaryHttpMessageHandler(()=>
      //   new HttpClientHandler(){

      //   }
      // );
      services.AddSwaggerGen(c =>
                {
                  c.SwaggerDoc("v1", new OpenApiInfo { Title = "PandaLogix Manager", Version = "v1" });
                });
    }

    // This method gets called by the runtime. Use this method to configure the HTTP request pipeline.
    public void Configure(IApplicationBuilder app, IWebHostEnvironment env)
    {
      if (env.IsDevelopment())
      {
        app.UseDeveloperExceptionPage();
      }
      else
      {
        app.UseExceptionHandler("/Error");
        app.UseHsts();
      }

      // app.UseHttpsRedirection();

      app.UseDefaultFiles();
      app.UseStaticFiles();
      app.UseProxy(new Middleware.ProxyOptions()
      {
        Mappings = new System.Collections.Generic.Dictionary<Microsoft.AspNetCore.Http.PathString, string> {
          { new PathString("/api/account"), "accountMgr"},
          {new PathString("/api/pad"),"padMgr"},
          {new PathString("/api/metadata"),"engineMgr"}
        }
      });
      app.UseMvc(routes =>
      {
        routes.MapRoute(
                  name: "default",
                  template: "{controller}/{action=Index}/{id?}");
      });
      // Enable middleware to serve generated Swagger as a JSON endpoint.
      app.UseSwagger();

      // Enable middleware to serve swagger-ui (HTML, JS, CSS, etc.),
      // specifying the Swagger JSON endpoint.
      app.UseSwaggerUI(c =>
      {
        c.SwaggerEndpoint("/swagger/v1/swagger.json", "PandaLogix V1");
      });
    }
  }
}
