using System.Collections.Generic;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Engine.Core.Nodes;
using Engine.Enums;
using Engine.Interfaces;

namespace Engine.Core
{
  [NodeMetaData(NodeClass = typeof(RegexNode), Category = "String", Name = nameof(RegexNode))]

  public class RegexNode : NodeBase
  {
    [FieldMetaData(Name = nameof(Pattern), ValueType = typeof(string))]
    public string Pattern { get; set; } = null;

    [FieldMetaData(Name = nameof(Value), ValueType = typeof(string))]
    public string Value { get; set; } = null;

    [FieldMetaData(Name = nameof(EnableGroup), ValueType = typeof(bool))]
    public bool EnableGroup { get; set; } = false;

    [FieldMetaData(Name = nameof(Matches), Direction = FieldDirection.Output)]
    public List<List<string>> Matches { get; set; } = null;

    protected async override Task InternalExecute(IContext context)
    {
      string pattern = Pattern ?? GetFieldValue(nameof(Pattern)).ToString();
      string value = Value ?? GetFieldValue(nameof(Value)).ToString();
      this.Context.Result = Regex.Match(value, pattern) != null;
      if (EnableGroup)
      {
        Matches = new List<List<string>>();
        MatchCollection matches = Regex.Matches(value, pattern);

        foreach (Match match in matches)
        {
          var grp = new List<string>();
          foreach (Group v in match.Groups)
          {
            grp.Add(v.Value);
          }
          Matches.Add(grp);

        }
      }
      await base.InternalExecute(context);
    }

  }
}