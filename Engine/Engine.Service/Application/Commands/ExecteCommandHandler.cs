using System.Threading;
using System.Threading.Tasks;
using Engine.Core;
using Engine.Enums;
using MediatR;

namespace Engine.Service
{

    public class ExecuteCommandHandler : IRequestHandler<ExecutionCommand, bool>
    {
        public async Task<bool> Handle(ExecutionCommand request, CancellationToken cancellationToken)
        {
           var pad = PadFactory.CreateInstance(request.Pad, ExecutionMode.Normal,request.Instances);
           await pad.Init();
           await pad.Execute(pad.Context,request.Instances);
           return true;
        }
    }
}