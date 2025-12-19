using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC_Company.Models
{
    public class UserLoginModel
    {
        [Required(ErrorMessage = "User email is required.")]
        [EmailAddress(ErrorMessage = "Invalid email address format.")]
        [Column("user_email")]
        [StringLength(255, ErrorMessage = "Email cannot exceed 255 characters.")]
        [Unicode(false)]
        public string UserEmail { get; set; } = null!;

        [Required(ErrorMessage = "Password is required.")]
        [StringLength(100, ErrorMessage = "Password must not exceed 100 characters.")]
        public string Password { get; set; }
    }
}
