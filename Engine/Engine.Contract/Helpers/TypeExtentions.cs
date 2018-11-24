using System;

namespace Engine.Helpers
{
  public static class TypeExtentions
  {
    public static bool IsNumericType(this Type type)
    {
      if (type == typeof(int)
        || type == typeof(Int16)
        || type == typeof(Int32)
        || type == typeof(Int64)
        || type == typeof(float)
        || type == typeof(Double)
        || type == typeof(Decimal))
      {
        return true;
      }
      return false;
    }

    public static bool IsDateTimeType(this Type type)
    {
      if (type == typeof(DateTime)
          || type == typeof(TimeSpan)
          || type == typeof(DateTimeOffset))
      {
        return true;
      }
      return false;
    }
  }
}
