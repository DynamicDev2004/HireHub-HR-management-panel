using System;
namespace ABC_Company.Models
{
	public class AdminParentModel
	{
	public List<TblJob> jobsdata  { get; set; }
    public List<TblRole> rolesdata { get; set; }
		public int ApplicationsSubmitted { get; set; }
		public int TotalStaff { get; set; }
    }

}

