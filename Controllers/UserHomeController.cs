using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC_Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;


// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ABC_Company.Controllers
{
    public class UserHomeController : Controller
    {
        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
        public UserHomeController(ApplicationDbContext _context, IWebHostEnvironment _webHost)
        {
            this.context = _context;
            this.webHostEnvironment = _webHost;
        }

        public IActionResult Index()
        {
            if (HttpContext.Session.GetInt32("Staff") != null) { return RedirectToAction("AdminIndex", "AdminHome"); }
                if (HttpContext.Session.GetString("ActiveSession") != null) {
                var userId = HttpContext.Session.GetInt32("UserID");
                UserHomeParentModel userHomeParent = new UserHomeParentModel
                {
                    jobApplied = context.TblApplicants.Count(a => a.user_id == userId.Value),
                    activeVacancies = context.TblJobs.ToList().Count,
                    jobsdata = context.TblJobs.OrderByDescending(j => j.JobId).Take(1).ToList(),
                    applicant = context.TblApplicants.OrderByDescending(j => j.ApplicantId).Where(x => x.user_id ==userId).Take(1).ToList(),


                };
                return View(userHomeParent);
            }
           return RedirectToAction("login", "Authentication");
        }
    }
}

