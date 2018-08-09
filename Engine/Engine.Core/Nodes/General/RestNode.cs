using System;
using System.Threading.Tasks;
using System.Net.Http;
using System.Net.Http.Headers;
using Engine;
using Engine.Interfaces;

namespace Engine.Core.Nodes.General
{
    [NodeMetaData(Category = "General", NodeClass = typeof(RestNode), Name = nameof(RestNode))]

    public class RestNode : NodeBase
    {
        [FieldMetaData(Name = nameof(EndPoint), ValueType = typeof(string))]
        public string EndPoint { get; set; }

        [FieldMetaData(Name = nameof(Method), ValueType = typeof(string))]
        public string Method { get; set; }

        [FieldMetaData(Name = nameof(Body), ValueType = typeof(string))]
        public string Body { get; set; }


        protected override async Task InternalExecute(IContext context)
        {

            var _client = new HttpClient();
            _client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
            HttpResponseMessage response;
            switch (Method.ToUpperInvariant())
            {
                case "GET":
                    response = await _client.GetAsync(EndPoint);
                    break;
                case "POST":
                    HttpContent content = new StringContent(Body);
                    response = await _client.PostAsync(EndPoint, content);
                    break;
                case "PUT":
                    response = await _client.PutAsync(EndPoint, new StringContent(Body));
                    break;
                case "DELETE":
                    response = await _client.DeleteAsync(EndPoint);
                    break;
                default:
                    throw new Exception("not support Methods");
            }

            if (response.IsSuccessStatusCode)
            {
                context.Result = await response.Content.ReadAsStringAsync();

            }
            else
            {
                response.EnsureSuccessStatusCode();
            }


            await base.InternalExecute(context);
        }

    }
}
