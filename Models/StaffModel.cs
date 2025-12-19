using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace ABC_Company.Models
{
	public class StaffModel
	{
        [Key]
        public int Staff_Id { get; set; }

        [Required]
        public int Staff_UserId { get; set; }
      

        [Required]
        [StringLength(255, ErrorMessage = "Name cannot exceed 255 characters.")]
        public string Staff_Name { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Salary must be a positive value.")]
        public int Staff_Salary { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Invalid role specified.")]
        public int Staff_Role { get; set; }

        [Required]
        [DataType(DataType.DateTime, ErrorMessage = "Invalid joining date format.")]
        public DateTime Staff_JoiningDate { get; set; } = DateTime.Now;

        [Required]
        [StringLength(100, ErrorMessage = "Status cannot exceed 100 characters.")]
        public string Staff_Status { get; set; }

        [Required]
        [DataType(DataType.Time, ErrorMessage = "Invalid time format.")]
        public DateTime Staff_TimingFrom { get; set; }

        [Required]
        [DataType(DataType.Time, ErrorMessage = "Invalid time format.")]
        public DateTime Staff_TimingTo { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Duration type cannot exceed 100 characters.")]
        public string Staff_DurationType { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "Access type cannot exceed 100 characters.")]
        public string Staff_AccessType { get; set; } = "Staff";

        [DataType(DataType.DateTime, ErrorMessage = "Invalid left date format.")]
        public DateTime? Staff_LeftDateTime { get; set; }

        [NotMapped]
        public int ApplicationId { get; set; }
    }
}

