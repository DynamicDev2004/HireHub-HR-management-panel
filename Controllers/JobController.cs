using ABC_Company.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace ABC_Company.Controllers
{
    public class JobController : Controller
    {
        private readonly ApplicationDbContext _context;

        public JobController(ApplicationDbContext context)
        {
            this._context = context;
        }
        // GET: JobController
        public ActionResult Index()
        {

            List<TblJob> job = _context.TblJobs.ToList();
            return View(job);
        }



        [HttpGet]
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null) { 
                List<TblRole> roles = _context.TblRoles.ToList();

            ViewBag.roles = roles;

            return View();
            }
            else { return RedirectToAction("Index", "UserHome"); }
        }


        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblJob job)
        {

            try
            {
                List<TblRole> roles = _context.TblRoles.ToList();

                ViewBag.roles = roles;

                Console.WriteLine(ModelState);
                Console.WriteLine(ModelState.Count);
                Console.WriteLine(ModelState.ErrorCount);

                if (ModelState.IsValid)
                {
                    try
                    {
                        _context.TblJobs.Add(job);
                        _context.SaveChanges();
                        return RedirectToAction("AdminIndex", "AdminHome");
                    }

                    catch
                    {
                        return View(job);
                    }
                }
                return View(job);
            }
      

  catch (Exception ex)
            {
                return View(job);
            }






        }

      
        [HttpGet]
        public ActionResult Edit(int JobId)
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null) { 
            
                var job = _context.TblJobs.FirstOrDefault(x => x.JobId == JobId);

            List<TblRole> roles = _context.TblRoles.ToList();

            ViewBag.roles = roles;


            if (job == null)
            {
                TempData["ErrorMessage"] = "Job does not exist";
                return RedirectToAction("AdminIndex", "AdminHome");
            }
            return View(job);

                }

                else { return RedirectToAction("Index", "UserHome"); }

        }

        // POST: JobController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TblJob job)
        {
 

                try
                {
                    _context.TblJobs.Update(job);
                    _context.SaveChanges();
                TempData["SuccessMessage"] = "Job Updated";
                return RedirectToAction("AdminIndex", "AdminHome");
            }
                catch
                {
                    return View(job);
                }
          
        }

        // GET: JobController/Delete/5
        [HttpGet]
        public ActionResult Delete(int JobId)
        {



            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null)
            { 
                var job = _context.TblJobs.FirstOrDefault(x => x.JobId == JobId);

            if (job == null)
            {
                TempData["ErrorMessage"] = "Job does not exist";
                return View(job);
            }
            return View(job);
            }
            else { return RedirectToAction("AdminIndex", "AdminHome"); }
        }

        // POST: JobController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TblJob job)
        {
            try
            {

                _context.TblJobs.Remove(job);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Job Deleted";
                return RedirectToAction("AdminIndex", "AdminHome");
            }
            catch (DbUpdateException dbEx)
            {
             
                if (dbEx.InnerException is SqlException sqlEx && sqlEx.Number == 547)
                {
                    TempData["ErrorMessage"] = "Cannot delete this job because it is referenced by other records.";
                }
                else
                {
                    TempData["ErrorMessage"] = "An unexpected error occurred. Please try again.";
                }


                return RedirectToAction("AdminIndex", "AdminHome");
            }
            catch ( Exception err)
            {
                return View(job);
            }
        }
    }
}
