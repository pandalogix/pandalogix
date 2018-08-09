using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Engine.Core.Nodes;
using Engine.Enums;
using Engine.Interfaces;

namespace Engine.Core
{
    [NodeMetaData(NodeClass = typeof(StringSplitNode), Category = "String", Name = nameof(StringSplitNode))]

    public class StringSplitNode : NodeBase
    {
        [FieldMetaData(Name = nameof(Seperator), ValueType = typeof(string))]
        public string Seperator { get; set; } = null;

        [FieldMetaData(Name = nameof(Value), ValueType = typeof(string))]
        public string Value { get; set; } = null;


        [FieldMetaData(Name = nameof(Substrings), Direction = FieldDirection.Output)]
        public List<string> Substrings { get; set; } = null;
        protected async override Task InternalExecute(IContext context)
        {
            string seperator = Seperator ?? GetFieldValue(nameof(Seperator)).ToString();
            string value = Value ?? GetFieldValue(nameof(Value)).ToString();
            Substrings = value.Split(new string[] { seperator }, StringSplitOptions.None).ToList();
            await base.InternalExecute(context);
        }
    }
}