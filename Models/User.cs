using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using Microsoft.EntityFrameworkCore;

namespace ABC_Company.Models;

[Table("users")]
[Index("UserEmail", Name = "UQ__users__B0FBA212D125100A", IsUnique = true)]
public partial class User
{
    [Key]
    [Column("user_id")]
    public int UserId { get; set; }

    [Required(ErrorMessage = "User name is required.")]
    [StringLength(100, ErrorMessage = "User name cannot exceed 100 characters.")]
    [MinLength(4, ErrorMessage = "User name must be at least 4 characters long.")]
    [Column("user_name")]
    [Unicode(false)]
    public string UserName { get; set; } = null!;

    [Required(ErrorMessage = "User email is required.")]
    [EmailAddress(ErrorMessage = "Invalid email address format.")]
    [Column("user_email")]
    [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
    [Unicode(false)]
    public string UserEmail { get; set; } = null!;

    [Required(ErrorMessage = "Password is required.")]
    [StringLength(100, ErrorMessage = "Password cannot exceed 100 characters.")]
    [MinLength(8, ErrorMessage = "Password must be at least 8 characters long.")]
    [Column("user_password")]
    [Unicode(false)]
    public string UserPassword { get; set; } = null!;


    [Column("user_profile_picture")]
    [Unicode(false)]
    public string? UserProfilePicture { get; set; }

    [Required]
    [StringLength(255, ErrorMessage = "Location cannot exceed 255 characters.")]
    [Column("user_location")]
    [Unicode(false)]
    public string UserLocation { get; set; } = null!;

    [Required(ErrorMessage = "Contact number is required.")]
    [RegularExpression(@"^\+?[0-9]\d{1,14}$", ErrorMessage = "Invalid contact number format.")]
    [StringLength(100, ErrorMessage = "Contact number cannot exceed 100 characters.")]
    [Column("user_contact_no")]
    [Unicode(false)]
    public string UserContactNo { get; set; } = null!;


    [Column("user_created_at", TypeName = "datetime")]
    public DateTime UserCreatedAt { get; set; } = DateTime.Now;

    [NotMapped]
    public IFormFile? UserProfileImageFile { get; set; }

    [InverseProperty("User")]
    public virtual ICollection<TblApplicant> TblApplicants { get; set; } = new List<TblApplicant>();
}


