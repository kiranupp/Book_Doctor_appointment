using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Doctor_appointment.Controllers
{
    public class HomeController : Controller
    {
        HospitalApplicationEntities db = new HospitalApplicationEntities();
        public ActionResult Index()
        {
           
            using (var dbs = new HospitalApplicationEntities())
            {
                var contrs = dbs.Hospital_location.ToList();

                ViewBag.location = new SelectList(contrs, "id", "Hospital_Location1");
            }
            return View();
        }
     
           

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}