using System.Collections.Generic;
using Microsoft.AspNetCore.Http;

namespace WebSPA.Middleware
{
    public class ProxyOptions
    {
        public Dictionary<PathString,string> Mappings { get; set; }
    }
}