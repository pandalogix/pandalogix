using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
namespace PadManager.Core.Models
{
  public class AccountPad
  {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public long Id { get; set; }

        public Guid UserId { get; set; }
        public Guid PadId { get; set; }

  }
}