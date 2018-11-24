using System;
namespace EventBus
{
  public class SubscriptionInfo
  {
    public SubscriptionInfo(Type handlerType)
    {
      HandlerType = handlerType;
    }

    public Type HandlerType { get; }

  }
}