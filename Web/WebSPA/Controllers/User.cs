using System;

namespace WebSPA.Controllers
{
  public class User
  {
    public long Id { get; set; }
    public Guid Identifier { get; set; }

    public string Email { get; set; }
    public string Tier { get; set; }

    public string FirstName { get; set; }
    public string LastName { get; set; }

    public DateTimeOffset ActiveUntil { get; set; }

    public DateTimeOffset ActviatedDate { get; set; }

    public long Quota { get; set; }

    public bool Active { get; set; }

    public string StripeCustomerId { get; set; }

    public Guid ApiKey { get; set; }
  }
}