using ABC_Company.Models;
using Microsoft.AspNetCore.Mvc;

namespace ABC_Company.Controllers
{
    public class AdminHomeController : Controller
    {
        private readonly ApplicationDbContext context;

        public AdminHomeController(ApplicationDbContext _context)
        {
            this.context = _context;
        }


        public IActionResult AdminIndex()
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null &&  HttpContext.Session.GetString("Admin") != null)
            {
                var AdminParentModel = new AdminParentModel {
                    jobsdata = context.TblJobs.ToList(),
                    rolesdata = context.TblRoles.ToList(),
                    ApplicationsSubmitted = context.TblApplicants.ToList().Count,
                    TotalStaff = context.TblStaff.ToList().Count
                };

                return View(AdminParentModel);
            }
            else { return RedirectToAction("login", "Authentication"); }
        }
    }
}
