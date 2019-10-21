using System;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Options;
using WebSPA.Middleware;

public class ProxyMiddleware
{
  private readonly RequestDelegate next;
  private readonly ProxyOptions options;
  private IHttpClientFactory _clientFactory;

  public ProxyMiddleware(RequestDelegate next, IOptions<ProxyOptions> options, IHttpClientFactory clientFactory)
  {
    if (next == null)
    {
      throw new ArgumentNullException(nameof(next));
    }
    if (options == null)
    {
      throw new ArgumentNullException(nameof(options));
    }

    this.next = next;
    this.options = options.Value;
    this._clientFactory = clientFactory;
  }

  public async Task Invoke(HttpContext context)
  {
    if (context == null)
    {
      throw new ArgumentNullException(nameof(context));
    }

    var request = context.Request;
    var clientName = GetClient(request);
    if (string.IsNullOrEmpty(clientName))
    {
      // context.Response.StatusCode = (int)System.Net.HttpStatusCode.BadRequest;
      await this.next(context);
      return;
    }
    using (HttpClient client = this._clientFactory.CreateClient(clientName))
    {

      var requestMessage = new HttpRequestMessage();
      var requestMethod = request.Method;
      if (!HttpMethods.IsGet(requestMethod) &&
         !HttpMethods.IsHead(requestMethod) &&
         !HttpMethods.IsDelete(requestMethod) &&
          !HttpMethods.IsTrace(requestMethod))
      {
        var streamContent = new StreamContent(request.Body);
        requestMessage.Content = streamContent;
      }

      // Copy the request headers
      foreach (var header in request.Headers)
      {
        if (!requestMessage.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray()) && requestMessage.Content != null)
        {
          requestMessage.Content?.Headers.TryAddWithoutValidation(header.Key, header.Value.ToArray());
        }
      }

      requestMessage.RequestUri = new Uri($"{request.Path}{request.QueryString}", UriKind.Relative);
      requestMessage.Method = new HttpMethod(request.Method);

      var response = await client.SendAsync(requestMessage, HttpCompletionOption.ResponseContentRead, context.RequestAborted);

      context.Response.StatusCode = (int)response.StatusCode;
      foreach (var header in response.Headers)
      {
        context.Response.Headers[header.Key] = header.Value.ToArray();
      }
      foreach (var header in response.Content.Headers)
      {
        context.Response.Headers[header.Key] = header.Value.ToArray();
      }

      // SendAsync removes chunking from the response. This removes the header so it doesn't expect a chunked response.
      context.Response.Headers.Remove("transfer-encoding");


      await response.Content.CopyToAsync(context.Response.Body);

    }
  }

  private string GetClient(HttpRequest request)
  {
    var clientName = string.Empty;

    foreach (var kv in this.options.Mappings)
    {
      if (request.Path.StartsWithSegments(kv.Key, StringComparison.InvariantCultureIgnoreCase))
      {
        Console.WriteLine($"client {kv.Value}");
        return kv.Value;
      }
    }

    return clientName;
  }
}

public static class ProxyExtension
{
  public static IApplicationBuilder UseProxy(this IApplicationBuilder application, ProxyOptions options)
  {
    return application.UseMiddleware<ProxyMiddleware>(Options.Create(options));
  }
}