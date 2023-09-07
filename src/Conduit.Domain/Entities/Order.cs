using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Conduit.Domain.Entities;
public class Order
{
    public int ArticleId { get; set; }
    public virtual required Article Article { get; set; }

    public int UserId { get; set; }
    public virtual required User User { get; set; }

    public required bool PyshicalCopy { get; set; }

    [MaxLength(255)]
    public required string Email { get; set; }

    [MaxLength(255)]
    public string SnailMail { get; set; } = string.Empty;

}
