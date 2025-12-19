using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ABC_Company.Models
{
    public class UserViewModel
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

        [Unicode(false)]
        public IFormFile UserProfilePicture { get; set; } = null!;

        [StringLength(15)]
        [Unicode(false)]
        public string UserContactNo { get; set; } = null!;

        [Column(TypeName = "datetime")]
        public DateTime? UserCreatedAt { get; set; } = DateTime.Now;

        [InverseProperty("User")]
        public virtual ICollection<TblApplicant> TblApplicants { get; set; } = new List<TblApplicant>();
    }

}
