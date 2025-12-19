using ABC_Company.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;

namespace ABC_Company.Controllers
{
    public class RolesController : Controller
    {
        private readonly ApplicationDbContext _context;

        public RolesController(ApplicationDbContext context)
        {
            this._context = context;
        }
   

  

        // GET: RolesController/Create
        public ActionResult Create()
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null)
            {
                return View();
            }
            else { return RedirectToAction("Index", "UserHome"); }
        }

        // POST: RolesController/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(TblRole role)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    _context.TblRoles.Add(role);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Role Created";
                    return RedirectToAction("AdminIndex", "AdminHome");
                }

                catch
                {
                    TempData["SuccessMessage"] = "Operation Unsuccessful";
                    return View(role);
                }
            }
            return View(role);
        }

        // GET: RolesController/Edit/5
        [HttpGet]
        public ActionResult Edit(int RoleId)
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null)
            {
                var role = _context.TblRoles.FirstOrDefault(x => x.RoleId == RoleId);

                if (role == null)
                {
                    return NotFound();
                }
                return View(role);
            }
            else { return RedirectToAction("Index", "UserHome"); }
        }

        // POST: RolesController/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(TblRole role)
        {
            if (ModelState.IsValid)
            {

                try
                {
                    _context.TblRoles.Update(role);
                    _context.SaveChanges();
                    TempData["SuccessMessage"] = "Role Updated";
                    return RedirectToAction("AdminIndex", "AdminHome");
                }
                catch
                {
                    TempData["ErrorMessage"] = "Something Went Wrong";
                    return View(role);
                }
            }
            else
            {
                TempData["ErrorMessage"] = "Invalid Data Entered";
                return View(role);
            }
        }

        // GET: RolesController/Delete/5
        [HttpGet]
        public ActionResult Delete(int RoleId)
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null)
            {
                var role = _context.TblRoles.FirstOrDefault(x => x.RoleId == RoleId);

                if (role == null)
                {
                    return NotFound();
                }
                return View(role);
            }
            else { return RedirectToAction("Index", "UserHome"); }
        }

        // POST: RolesController/Delete/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(TblRole role)
        {
            try
            {
                _context.TblRoles.Remove(role);
                _context.SaveChanges();
                TempData["SuccessMessage"] = "Role Deleted";
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
            catch
            {
                TempData["ErrorMessage"] = "Unable to Delete Role";
                return RedirectToAction("AdminIndex", "AdminHome");
 
            }
 
        }
    }
}
