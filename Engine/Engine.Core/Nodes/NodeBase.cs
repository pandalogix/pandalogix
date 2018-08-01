using Engine.Contracts;
using Engine.Enums;
using Engine.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Threading.Tasks;

namespace Engine.Core.Nodes
{
    public abstract class NodeBase : INode, IIdentifiableEntity
    {
        public long Id { get; set; }
        public NodeMetaData MetaDate { get; set; }
        public InstanceMapping NodeMapping { get; set; }
        protected IContext _context = null;
        public IContext Context { get { return _context; } }
        public IEnumerable<INode> InPorts { get; set; }
        public IEnumerable<INode> OutPorts { get; set; }
        public bool LogicPath { get; set; }

        public NodeType Type { get; set; }

        protected virtual Task InternalExecute(IContext context)
        {
            context.Status = ExecutionStatus.Success;
            return Task.FromResult(true);
        }

        protected virtual void CreateExecutionSummary()
        {
            this._context.ExecutionSummary = $"Done : {this.Id}";
        }

        public async Task Execute(IContext context)
        {
            context.Status = ExecutionStatus.Executing;

            try
            {
                if (CanExecute())
                {
                    await this.InternalExecute(context);
                    if (this.Type == NodeType.Output)
                        this.Context.Pad.Context.Result = this.Context.Result;
                    this.CreateExecutionSummary();
                }
            }
            catch (Exception e)
            {
                context.Status = ExecutionStatus.Failed;
                this._context.ExecutionSummary = $"Failed with error:{e}";

                throw;
            }
        }

        protected virtual bool CanExecute()
        {
            bool canexecute = true;
            if (this.InPorts?.Count() > 0)
            {
                canexecute = this.InPorts.Aggregate(canexecute, (d, node) =>
                {
                    if (node.Context != null)
                    {
                        return d && (node.Context!=null && node.Context.Status == ExecutionStatus.Success);
                    }
                    else
                    {
                        return d && false;
                    }
                });

                return canexecute == this.LogicPath;
            }
            return canexecute;
        }

        public Task<IContext> Init(IContext parent)
        {
            this._context = new NodeExecutionContext()
            {

                Pad = parent.Pad,
                Mode = parent.Mode,
                Instance = parent.Instance,
                ExecutionSummary = string.Empty,
                Status = ExecutionStatus.Pending
            };

            this.SetupNodes(_context);

            return Task.FromResult<IContext>(this._context);
        }

        private void SetupNodes(IContext context)
        {
            var nodes = context.Pad.Nodes;


        }

        protected object GetFieldValue(string fieldName)
        {
            object value = null;

            var fieldMetaData = (from m in this.MetaDate.FieldsMetaData where m.Name == fieldName select m).FirstOrDefault();
            if (fieldMetaData != null)
            {

                PropertyInfo prop = this.GetType().GetRuntimeProperty(fieldName);
                if (prop != null)
                {

                    if (fieldMetaData.MappedNodeId > 0)
                    {
                        var mappedNode = (from i in this.Context.Pad.Nodes where (i as NodeBase).Id == fieldMetaData.MappedNodeId select i).FirstOrDefault();
                        if (mappedNode != null)
                        {
                            if (fieldMetaData.MappedFieldName == Engine.Constants.DEFAULT_MAPFIELDNAME)
                            {
                                value = mappedNode.Context.Result;
                            }
                            else
                            {
                                var mappedProp = mappedNode.GetType().GetRuntimeProperty(fieldMetaData.MappedFieldName);
                                if (mappedProp != null)
                                    value = mappedProp.GetValue(mappedNode);
                            }
                        }
                        if (value != null)
                        {
                            if (prop.PropertyType.GetTypeInfo().IsEnum)
                            {
                                prop.SetValue(this, Enum.Parse(prop.PropertyType, value.ToString()));
                            }
                            else
                            {
                                if (value is TimeSpan)
                                {
                                    prop.SetValue(this, value);
                                }
                                else
                                {
                                    prop.SetValue(this, Convert.ChangeType(value, prop.PropertyType, null));
                                }
                            }
                        }
                    }
                    else
                    {
                        value = prop.GetValue(this);
                    }


                }
            }

            return value;
        }
    }
}
