using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ABC_Company.Models;

[Table("tblJobs")]
public partial class TblJob
{
    [Key]
    public int JobId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string JobTitle { get; set; } = null!;

    [StringLength(255)]
    [Unicode(false)]
    public string JobDescription { get; set; } = null!;

 
    public int JobSalary { get; set; }


    [StringLength(255)]
    public string JobDurationType { get; set; }

    public TimeSpan JobTimingFrom { get; set; }

    public TimeSpan JobTimingTo { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string JobLocation { get; set; } = null!;

    [StringLength(255)]
    public string JobOpenStatus { get; set; }

    public int JobOfNumbers { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? JobCreatedAt { get; set; }

    public int RoleId { get; set; }

    [ForeignKey("RoleId")]
    [InverseProperty("TblJobs")]
    public virtual TblRole Role { get; set; } = null!;

    [InverseProperty("Job")]
    public virtual ICollection<TblApplicant> TblApplicants { get; set; } = new List<TblApplicant>();
}
