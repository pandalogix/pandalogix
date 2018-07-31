using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using Engine.Contracts;
using Newtonsoft.Json;


namespace PandaDoctor.Nodes
{
    public class NodeFactory
    {
        public static NodeBase CreateNode(NodeBaseContract contract, IEnumerable<InstanceMapping> mappings)
        {


            NodeBase node = Activator.CreateInstance(contract.MetaData.NodeData.NodeClass) as NodeBase;
            node.Type = contract.Type;
            node.Id = contract.Id;
            node.LogicPath = contract.LogicPath;
            node.MetaDate = contract.MetaData;
            var nodeMapping = (from o in mappings where o.NodeId == contract.Id select o).FirstOrDefault();
            node.NodeMapping = nodeMapping;
            if (nodeMapping != null)
            {
                foreach (var field in contract.MetaData.FieldsMetaData)
                {
                    var fieldValues = from o in nodeMapping.FieldMappings where o.FieldName == field.Name select o;
                    if (fieldValues.Any())
                    {
                        var fieldValue = fieldValues.ElementAtOrDefault(0);
                        PropertyInfo prop = contract.MetaData.NodeData.NodeClass.GetRuntimeProperty(field.Name);
                        if (prop != null)
                        {
                            if (prop.PropertyType.GetTypeInfo().IsEnum)
                            {
                                prop.SetValue(node, Enum.Parse(prop.PropertyType, fieldValue.Value?.ToString()));
                            }
                            else
                            {

                                if (field.ValueType != prop.PropertyType)
                                {
                                    if (field.ValueType == typeof(TimeSpan))
                                    {
                                        prop.SetValue(node, TimeSpan.Parse(fieldValue?.Value));
                                    }
                                    else
                                    {
                                        prop.SetValue(node, Convert.ChangeType(fieldValue?.Value, field.ValueType, null));
                                    }
                                }
                                else
                                {
                                    if (fieldValue != null)
                                    {
                                        if (fieldValue.Value.StartsWith("{") && fieldValue.Value.EndsWith("}"))
                                        {
                                            var value = JsonConvert.DeserializeObject(fieldValue.Value, prop.PropertyType);
                                            prop.SetValue(node, Convert.ChangeType(value, prop.PropertyType, null));
                                        }
                                        else
                                        {
                                            prop.SetValue(node, Convert.ChangeType(fieldValue?.Value, prop.PropertyType, null));
                                        }
                                    }
                                    else
                                    {
                                        prop.SetValue(node, fieldValue?.Value);
                                    }

                                }


                            }
                        }

                    }
                }
            }

            return node;
        }
    }
}
