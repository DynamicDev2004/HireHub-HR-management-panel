using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using ABC_Company.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using ABC_Company.Models;

// For more information on enabling MVC for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace ABC_Company.Controllers
{
    public class AuthenticationController : Controller

    {

        private readonly ApplicationDbContext context;
        private readonly IWebHostEnvironment webHostEnvironment;
      public AuthenticationController (ApplicationDbContext _context, IWebHostEnvironment _webHost)
        {
            this.context = _context;
            this.webHostEnvironment = _webHost;
        }

        [HttpGet]
        public IActionResult login()
        {
            if (HttpContext.Session.GetString("ActiveSession") == null) { 
                return View();
        }
            else { return RedirectToAction("Index", "UserHome"); }
        }
        [HttpPost]
        public IActionResult login(UserLoginModel LoginCredentials)


        {
            if (ModelState.IsValid) {
                try
                {
                     var FetchedUserData =  context.Users.FirstOrDefault(x => x.UserEmail == LoginCredentials.UserEmail && x.UserPassword == LoginCredentials.Password);

                     if(FetchedUserData != null)
                     {
                        var StaffChecker = context.TblStaff.FirstOrDefault(x => x.Staff_UserId == FetchedUserData.UserId);
                    
                      HttpContext.Session.SetString("ActiveSession", Guid.NewGuid().ToString());
                      HttpContext.Session.SetString("UserName", FetchedUserData.UserName);
                        HttpContext.Session.SetInt32("UserID", FetchedUserData.UserId);
                        HttpContext.Session.SetString("UserProfile", FetchedUserData.UserProfilePicture.ToString());

                      TempData["SuccessMessage"] = "Login Successful";

                        if (StaffChecker.Staff_AccessType == "Admin" || StaffChecker.Staff_AccessType == "admin")
                        {
                            HttpContext.Session.SetString("Admin", "true");
                            return RedirectToAction("AdminIndex", "AdminHome");
                        }

                
                        return RedirectToAction("Index", "UserHome");
                     }
                        else { TempData["ErrorMessage"] = "Invalid Username or Password"; }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Something Went Wrong";
                }



            }
           


            return View();
        }





        [HttpGet]
        public IActionResult signup()
        {
            if (HttpContext.Session.GetString("ActiveSession") == null)
            {
                return View();
            }
            else { return RedirectToAction("Index", "UserHome"); }

  
        }
        [HttpPost]
        
        public async Task< IActionResult > signup(User newUserData)
        {
          
            if (ModelState.IsValid) {

            
             try {
                    var FetchingExistingEmails = context.Users.FirstOrDefault(x => x.UserEmail == newUserData.UserEmail);
                 if(FetchingExistingEmails == null) { 

                if (newUserData.UserProfileImageFile != null) {

                 string filename = Guid.NewGuid().ToString() + "_" + newUserData.UserProfileImageFile.FileName;

                 string dirPath = Path.Combine(webHostEnvironment.WebRootPath, "usersProfileImages");
                 string filePath = Path.Combine(dirPath, filename);

                 using (var stream = new FileStream(filePath, FileMode.CreateNew))
                 {
                    await newUserData.UserProfileImageFile.CopyToAsync(stream);
                 }
                 newUserData.UserProfilePicture = "/usersProfileImages/" + filename;
                }

       
                else { newUserData.UserProfilePicture = "/usersProfileImages/DefaultProfileImage.svg"; }
                
                context.Users.AddAsync(newUserData);
               await context.SaveChangesAsync();
             
                TempData["SuccessMessage"] = "Your Account Has been Created";

                return RedirectToAction("login", "Authentication");

                    }
                    else {

                        TempData["ErrorMessage"] = "The Email Already Exists";

                        return View(newUserData);
                    }
                }
                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Something Went Wrong";
                    return View(newUserData);
                }

            }
            else { TempData["ErrorMessage"] = "Please enter valid details";
                return View(newUserData);
            }

        }



        [HttpGet]
        public async Task<IActionResult> ForgetPassword()
        {
            if (HttpContext.Session.GetString("ActiveSession") == null)
            {
                return View();
            }
            else { return RedirectToAction("Index", "UserHome"); }
          
        }
        [HttpPost]
        public async Task<IActionResult> ForgetPassword(UserForgotPasswordModel NewUserPassword)
        {
            if (ModelState.IsValid)
            {

                try
                {

                    var CheckingUsersData = context.Users.FirstOrDefault(x => x.UserEmail == NewUserPassword.Email);
                    if (CheckingUsersData != null)
                    {
                        CheckingUsersData.UserPassword = NewUserPassword.Password;
                        context.Users.Update(CheckingUsersData);
                        context.SaveChanges();
                        TempData["SuccessMessage"] = "Your Password Has been Updated";
                    return RedirectToAction("login");
                    }

                    else
                    {
                        TempData["ErrorMessage"] = "Please Enter Valid Email";
                    }
                }




                catch (Exception ex)
                {
                    TempData["ErrorMessage"] = "Something Went Wrong";
                    return View(NewUserPassword);
                }




            }
            return View(NewUserPassword);
        
        }


        public async Task<IActionResult> logout()
        {
            HttpContext.Session.Clear();
            TempData["SuccessMessage"] = "Logout Successful";
            return  RedirectToAction("login");
        }

    }


}

