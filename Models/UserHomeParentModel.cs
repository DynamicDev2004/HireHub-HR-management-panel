using System;
namespace ABC_Company.Models
{
	public class UserHomeParentModel
	{
        public int activeVacancies { get; set; }
        public int jobApplied { get; set; }
        public List<TblJob> jobsdata { get; set; }
        public List<TblApplicant> applicant { get; set; }

    }
}

