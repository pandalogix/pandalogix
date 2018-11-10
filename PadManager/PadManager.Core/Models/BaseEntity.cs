using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace PadManager.Core.Models
{
  public class BaseEntity : IIdentifiableEntity
  {
    [Key]
    [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
    public long Id { get; set; }
    public Guid Identifier { get; set; }
    public DateTimeOffset CreatedDate { get; set; }
    public DateTimeOffset LastUpdatedDate { get; set; }

    [StringLength(255)]
    public string CreatedBy { get; set; }
    [StringLength(255)]
    public string UpdatedBy { get; set; }

    Guid UserId { get; set; }
  }
}