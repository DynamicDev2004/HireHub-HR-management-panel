using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC_Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using static System.Net.Mime.MediaTypeNames;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ABC_Company.Controllers
{
    public class StaffController : Controller
    {

        private readonly ApplicationDbContext context;

        public StaffController(ApplicationDbContext _context)
        {
            this.context = _context;
        }

        [HttpGet]
        public IActionResult Create(int applicationId)
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null)
            {
                try
                {
                    var tagetApplication = context.TblApplicants.FirstOrDefault(x => x.ApplicantId == applicationId);
                    var applicantDetils = context.Users.FirstOrDefault(x => x.UserId == tagetApplication.user_id);
                    List<TblRole> roles = context.TblRoles.ToList();
                    ViewBag.roles = roles;

                    StaffModel newEmployee = new StaffModel
                    {
                        Staff_Name = applicantDetils.UserName,
                        Staff_UserId = applicantDetils.UserId,
                        ApplicationId = applicationId

                    };

                    return View(newEmployee);
                }

                catch (Exception ex)
                {
                    return RedirectToAction("AllApplications", "Applicant");
                }
            }
            else { return RedirectToAction("login", "Authentication"); }

        }
        [HttpPost]
        public IActionResult Create(StaffModel newStaff)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    context.TblStaff.Add(newStaff);

                    var tagetApplication = context.TblApplicants.FirstOrDefault(x => x.ApplicantId == newStaff.ApplicationId);
                    tagetApplication.ApplicantIsHired = "hired";
                    context.TblApplicants.Update(tagetApplication);
                    context.SaveChanges();
                    TempData["SuccessMessage"] = "New Staff Member Has been Added";
                    return RedirectToAction("AllApplications", "Applicant");

                }
                else
                {
                    TempData["ErrorMessage"] = "Invalid Details";
                    return View(newStaff);
                }
            }
         
         

               catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Something Went Wrong";
                return View(newStaff);
            }

        }

        public IActionResult AllStaffData()
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null)
            {
                try
                {
                    List<StaffModel> staffList = context.TblStaff.ToList();

                    List<TblRole> roles = context.TblRoles.ToList();

                    ViewBag.roles = roles;
                    return View(staffList);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Something Went Wrong";
                    return RedirectToAction("AllStaffData", "Staff");
                }
            }
            else { return RedirectToAction("Index", "UserHome"); }
        }



        public IActionResult DeleteStaff(int id)
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null)
            {
                var staffRow = context.TblStaff.FirstOrDefault(x => x.Staff_Id == id);
                if (staffRow != null)
                {

                    context.TblStaff.Remove(staffRow);
                    context.SaveChanges();
                    TempData["SuccessMessage"] = "Staff Has Been Removed";
                    return RedirectToAction("AllStaffData", "Staff");
                }
                else
                {
                    TempData["ErrorMessage"] = "Staff Not Found";
                    return RedirectToAction("AllStaffData", "Staff");
                }
            }
            else { return RedirectToAction("Index", "UserHome"); }
        }
        [HttpGet]
        public IActionResult EditStaff(int id)
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null)
            {
                try
                {
                    var FetchedEditRow = context.TblStaff.FirstOrDefault(x => x.Staff_Id == id);
                    List<TblRole> roles = context.TblRoles.ToList();

                    ViewBag.roles = roles;

                    return View(FetchedEditRow);
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Something Went Wrong";
                    return RedirectToAction("AllStaffData", "Staff");
                }
            }
            else { return RedirectToAction("Index", "UserHome"); }
        }
        [HttpPost]
        public IActionResult EditStaff(StaffModel staff)
        {
            try
            {
                var targetStaffRow = context.TblStaff.FirstOrDefault(x => x.Staff_Id == staff.Staff_Id);
                if (targetStaffRow != null)
                {
                    targetStaffRow.Staff_LeftDateTime = staff.Staff_LeftDateTime;
                    targetStaffRow.Staff_Role = staff.Staff_Role;
                    targetStaffRow.Staff_Salary = staff.Staff_Salary;
                    targetStaffRow.Staff_Status = staff.Staff_Status;
                    targetStaffRow.Staff_TimingFrom = staff.Staff_TimingFrom;
                    targetStaffRow.Staff_TimingTo = staff.Staff_TimingTo;
                    targetStaffRow.Staff_DurationType = staff.Staff_DurationType;
                    context.TblStaff.Update(targetStaffRow);
                    context.SaveChanges();
                    TempData["SuccessMessage"] = "Staff Details has been updated";
                    return RedirectToAction("AllStaffData", "Staff");
                }
                else { TempData["ErrorMessage"] = "Staff Member Not Found"; }
            }
            catch (Exception ex)
            {
                TempData["ErrorMessage"] = "Something Went Wrong";
            }

            return RedirectToAction("AllStaffData", "Staff");
        }


        public IActionResult DetailsStaff(int id)
        {
            if (HttpContext.Session.GetString("ActiveSession") != null && HttpContext.Session.GetString("UserID") != null && HttpContext.Session.GetString("Admin") != null)
            {

                try
                {
                    var SelectedStaff = context.TblStaff.FirstOrDefault(x => x.Staff_Id == id);
                    List<TblRole> roles = context.TblRoles.ToList();

                    ViewBag.roles = roles;
                    return View(SelectedStaff);
                }
                catch (Exception ex)
                {
                    return RedirectToAction("AllStaffData", "Staff");
                }
            }
           return RedirectToAction("AllStaffData", "Staff");
        }



    }
}

