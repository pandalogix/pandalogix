using Engine.Contracts;
using System.Collections.Generic;

namespace Engine.Interfaces
{
  public interface IInstance
  {
    IEnumerable<InstanceMapping> GetMappings();
  }
}