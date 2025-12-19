using System;
using ABC_Company.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ABC_Company.Controllers
{
    public class ApplicantController : Controller
    {
        private readonly ApplicationDbContext _context;
        private readonly IWebHostEnvironment webHostEnvironment;

        public ApplicantController(ApplicationDbContext context, IWebHostEnvironment _webHost)
        {
            this._context = context;
            this.webHostEnvironment = _webHost;
        }


     
        public ActionResult Create(int jobId)
        {
            var userId = HttpContext.Session.GetInt32("UserID");
            if(userId != null) { 
            TblApplicant applicant = new TblApplicant {

                user_id = userId.Value,


            JobId = jobId
            };
    
            return View(applicant);
            }
            else { return RedirectToAction("login", "Authentication"); }
         
        }

        // POST: ApplicantController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create(TblApplicant newApplicant)
        {

            if (ModelState.IsValid) {

                var fetchingPreviousRecords = _context.TblApplicants.FirstOrDefault(x => x.JobId == newApplicant.JobId && x.user_id == newApplicant.user_id);

                if(fetchingPreviousRecords == null)
                { 
                  try
                  {
                    string filename = Guid.NewGuid().ToString() + "_" + newApplicant.CVFile.FileName;

                    string dirPath = Path.Combine(webHostEnvironment.WebRootPath, "UsersCv");
                    string filePath = Path.Combine(dirPath, filename);

                    using (var stream = new FileStream(filePath, FileMode.CreateNew))
                    {
                        await newApplicant.CVFile.CopyToAsync(stream);
                    }
                    newApplicant.ApplicantCv = "/UsersCv/" + filename;


                       _context.TblApplicants.Add(newApplicant);
                        _context.SaveChanges();
                        TempData["SuccessMessage"] = "Your Application Has been Submitted";
                        return RedirectToAction("Index", "Job");
                  }
                   catch(Exception ex)
                   {
                   return View();
                   }
                }
                else { TempData["ErrorMessage"] = "You have already Applied"; }

        }
            return View(newApplicant);
        }

        // GET: ApplicantController/Edit/5
        public ActionResult UserAppliedJobList(CombinedAppliedJobsData combinedAppliedData)
        {

          
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null) { 
            var userId = HttpContext.Session.GetInt32("UserID");
                CombinedAppliedJobsData appliedDatapassing = new CombinedAppliedJobsData
                {

                   jobs = _context.TblJobs.ToList(),
                    applicant = _context.TblApplicants.Where(a => a.user_id == userId.Value).ToList()
            };
           
            return View(appliedDatapassing);
            }
            else { return RedirectToAction("login", "Authentication"); }
        }


        public ActionResult AllApplications(CombinedAppliedJobsData combinedAppliedData)
        {


            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null)
            {

                CombinedAppliedJobsData appliedDatapassing = new CombinedAppliedJobsData
                {

                    jobs = _context.TblJobs.ToList(),
                    applicant = _context.TblApplicants.ToList(),
                    users = _context.Users.ToList()
                };

                return View(appliedDatapassing);
        }
            else { return RedirectToAction("login", "Authentication"); }
        }

        [HttpGet]
        public ActionResult EditApplication(int applicationId)
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null) {

                try
                {

                    TblApplicant Fetchedapplicantlist = _context.TblApplicants.FirstOrDefault(x => x.ApplicantId == applicationId);
                    if (Fetchedapplicantlist != null)
                    {
                        return View(Fetchedapplicantlist);

                    }
                    else
                    {
                        TempData["ErrorMessage"] = "Application Not Found";
                        return RedirectToAction("AllApplications", "Applicant");
                    }
                }


              catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Something Went Wrong";
                    return RedirectToAction("AllApplications", "Applicant");
             }

            }
            else { return RedirectToAction("Authentication", "login"); }
        }



        [HttpPost]
        public ActionResult EditApplication(TblApplicant updatedApplication)
        {

            try
            {

       
            var Gettingapplicantlist = _context.TblApplicants.FirstOrDefault(x => x.ApplicantId == updatedApplication.ApplicantId);
            if (Gettingapplicantlist != null) { 
            Gettingapplicantlist.Applicant1Status = updatedApplication.Applicant1Status;
            Gettingapplicantlist.Applicant2Status = updatedApplication.Applicant2Status;
            Gettingapplicantlist.Applicant3Status = updatedApplication.Applicant3Status;
            Gettingapplicantlist.ApplicantInterview1DateTime = updatedApplication.ApplicantInterview1DateTime;
            Gettingapplicantlist.ApplicantInterview2DateTime = updatedApplication.ApplicantInterview2DateTime;
            Gettingapplicantlist.ApplicantInterview3DateTime = updatedApplication.ApplicantInterview3DateTime;
                if(updatedApplication.ApplicantIsHired == "rejected") { Gettingapplicantlist.ApplicantIsHired = updatedApplication.ApplicantIsHired; }
            Gettingapplicantlist.ApplicantIsSelected = updatedApplication.ApplicantIsSelected;
              

            _context.TblApplicants.Update(Gettingapplicantlist);
            _context.SaveChanges();
                TempData["SuccessMessage"] = "Application Status Has been Updated";
                Console.WriteLine(updatedApplication.ApplicantIsHired);
                if (updatedApplication.ApplicantIsHired == "hired")
                {
                   return RedirectToAction("Create", "Staff", new { applicationId = updatedApplication.ApplicantId });
                }

                return RedirectToAction("AllApplications", "Applicant");
            }

            else { return View(updatedApplication); }

            }

            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Something Went Wrong";
                return RedirectToAction("AllApplications", "Applicant");
            }





        }

    }
}
