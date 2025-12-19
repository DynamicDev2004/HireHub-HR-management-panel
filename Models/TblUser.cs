using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ABC_Company.Models;

[Table("tblUsers")]
[Index("UserEmail", Name = "UQ__tblUsers__08638DF84A4E08DF", IsUnique = true)]
public partial class TblUser
{
    [Key]
    public int UserId { get; set; }

    [StringLength(50)]
    [Unicode(false)]
    public string UserName { get; set; } = null!;

    [StringLength(100)]
    [Unicode(false)]
    public string UserEmail { get; set; } = null!;

    [StringLength(50)]
    [Unicode(false)]
    public string UserPassword { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string UserProfilePicture { get; set; } = null!;

    [StringLength(15)]
    [Unicode(false)]
    public string UserContactNo { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? UserCreatedAt { get; set; }

}
