using Book_Doctor_appointment.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Mail;
using System.Web;
using System.Web.Mvc;
using static Book_Doctor_appointment.Models.Searchdoctor;

namespace Book_Doctor_appointment.Controllers
{
    public class LoginController : Controller
    {
     
        public ActionResult Index()
        { 
            return View();

        }
        public ActionResult Signup(loginuser li)
        {
            return View(li);
        }

        public ActionResult SubmitData(loginuser li)
        {
            if (string.IsNullOrEmpty(li.Name) || string.IsNullOrEmpty(li.Emailid) || string.IsNullOrEmpty(li.Userpassword) || string.IsNullOrEmpty(li.c_pwd)|| li.Userpassword != li.c_pwd)
            {
                if (string.IsNullOrEmpty(li.Name))
                {
                    ModelState.AddModelError("Name", "Please Enter your Name");
                }
                if (string.IsNullOrEmpty(li.Emailid))
                {
                    ModelState.AddModelError("Emailid", "Please enter your Email Id");
                }

                if (string.IsNullOrEmpty(li.Userpassword))
                {
                    ModelState.AddModelError("Userpassword", "Please enter your password");
                }
                if (string.IsNullOrEmpty(li.c_pwd))
                {
                    ModelState.AddModelError("c_pwd", "Please enter your password again");
                }

                if (li.Userpassword!=li.c_pwd)
                {
                    ModelState.AddModelError("c_pwd", "Password and confirm password not match please try again..");
                }

                return View("Signup");
            }

            else
            {
                Enterintotable en = new Models.Enterintotable();
                en.InsertUser(li);
                ViewBag.name = li.Name;
                ViewBag.Message = "Sucessfully Registered Please Login to proced further..";
                return View("login");
            }

        }
        [HttpGet]
        public ActionResult login(loginuser li)
        {
            return View(li);

        }

        [HttpGet]

        public ActionResult LoginDoctor(Logindoctor li)
        {
            return View(li);

        }

        [Authorize]
        [HttpPost]

        public ActionResult LoginDoctors(Logindoctor li)
        {
            if (string.IsNullOrEmpty(li.Userpassword) || string.IsNullOrEmpty(li.Emailid))
            {
                if (string.IsNullOrEmpty(li.Emailid))
                {
                    ModelState.AddModelError("Emailid", "Please Enter your email id");
                }
                if (string.IsNullOrEmpty(li.Userpassword))
                {
                    ModelState.AddModelError("Userpassword", "Please enter your password");
                }
                return View("LoginDoctor");

            }

            else
            {
                Searchdoctor d = new Searchdoctor();
                string pass = d.searchk(li);
                if (pass == li.Userpassword)
                {
                    @ViewBag.name = li.Emailid;
                    GetDoctorid s = new Models.GetDoctorid();
                    int id = s.Searchbyusername(li.Emailid);
                    return RedirectToAction("Patientappointments", "Hospital", new { id = id });
                }
                else
                    @ViewBag.Message = "Invalid Doctor account Please try again..";
                return View("LoginDoctor");

            }  
        }
        public ActionResult Loginsearch(loginuser li)
        {

            if (string.IsNullOrEmpty(li.Userpassword) || string.IsNullOrEmpty(li.Emailid))
            {
                if (string.IsNullOrEmpty(li.Emailid))
                {
                    ModelState.AddModelError("Emailid", "Please Enter your email id");
                }
                if (string.IsNullOrEmpty(li.Userpassword))
                {
                    ModelState.AddModelError("Userpassword", "Please enter your password");
                }

                return View("login");

            }

            else
            {

                Searchuser ss = new Models.Searchuser();
                string pass = ss.searchk(li);
                if (pass == li.Userpassword)
                {
                    @ViewBag.name = li.Emailid;
                    GetUserid s = new Models.GetUserid();
                    int id = s.Searchbyusername(li.Emailid);
                    @ViewBag.Id = id;
                    @ViewBag.name = li.Emailid;
                    //@ViewBag.Id = li.id;
                    Session["username"] = li.Emailid;
                    return View("loadlogin");
                }
                else
                {
                    @ViewBag.data = "Invalid User Please try again..";
                    return View("login");
                }

            }
        }

        public ActionResult loadlogin()
        {

            return View();
        }

     }

}