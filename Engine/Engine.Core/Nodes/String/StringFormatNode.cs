using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Engine.Core.Nodes;
using Engine.Interfaces;
using HandlebarsDotNet;
using Newtonsoft.Json;

namespace Engine.Core
{
    public class StringFormatNode : NodeBase
    {
        [FieldMetaData(Name = nameof(Pattern), ValueType = typeof(string))]
        public string Pattern { get; set; } = null;

        [FieldMetaData(Name = nameof(Data), ValueType = typeof(string))]
        public string Data { get; set; } = null;

        protected async override Task InternalExecute(IContext context)
        {
            string pattern = Pattern ?? GetFieldValue(nameof(Pattern)).ToString();
            var dd = Data??GetFieldValue(nameof(Data)).ToString();
            var template = Handlebars.Compile(pattern);
            var data = JsonConvert.DeserializeObject(dd);
            this.Context.Result = template(data);
            await base.InternalExecute(context);
        }
    }
}