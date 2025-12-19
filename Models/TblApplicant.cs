using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ABC_Company.Models;

[Table("tblApplicants")]
public partial class TblApplicant
{
    [Key]
    public int ApplicantId { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string? ApplicantCv { get; set; }

    // Make the interview date properties nullable
    [Column(TypeName = "datetime")]
    public DateTime? ApplicantInterview1DateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApplicantInterview2DateTime { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApplicantInterview3DateTime { get; set; }

    // Make the status properties nullable
    [StringLength(100)]
    public string? Applicant1Status { get; set; }

    [StringLength(100)]
    public string? Applicant2Status { get; set; }

    [StringLength(100)]
    public string? Applicant3Status { get; set; }

    [StringLength(100)]
    public string? ApplicantIsSelected { get; set; }

    [StringLength(100)]
    public string? ApplicantIsHired { get; set; }

    [Column(TypeName = "datetime")]
    public DateTime? ApplicantCreatedAt { get; set; } = DateTime.Now;

    public int JobId { get; set; }

    public int user_id { get; set; }

    [StringLength(255)]
    [Unicode(false)]
    public string ApplicantProposal { get; set; } = null!;

    [ForeignKey("JobId")]
    [InverseProperty("TblApplicants")]
    public virtual TblJob? Job { get; set; } = null!;

    [ForeignKey("user_id")]
    [InverseProperty("TblApplicants")]
    public virtual User? User { get; set; } = null!;

    [Required]
    [NotMapped]
    public IFormFile? CVFile { get; set; }
}


