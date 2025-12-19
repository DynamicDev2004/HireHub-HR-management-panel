using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ABC_Company.Models;

[Table("tblRoles")]
public partial class TblRole
{
    [Key]
    public int RoleId { get; set; }

    [StringLength(100)]
    [Unicode(false)]
    public string RoleName { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string RoleDescription { get; set; } = null!;

    [Column(TypeName = "datetime")]
    public DateTime? RoleCreatedAt { get; set; } = DateTime.Now;

    [InverseProperty("Role")]
    public virtual ICollection<TblJob> TblJobs { get; set; } = new List<TblJob>();
}
