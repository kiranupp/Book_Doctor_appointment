using Book_Doctor_appointment.Models;
using System;
using System.Collections.Generic;
using System.Data.Objects.SqlClient;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Doctor_appointment.Controllers
{
    public class HospitalController : Controller
    {
        public ActionResult Index(int? id)
        {
            List<SelectListItem> locationlist = GetLocations();
            List<SelectListItem> Specialtylist = GetSpecialties();
            ViewBag.location = locationlist;
            ViewBag.Specialty = Specialtylist;
            ViewBag.Userid = id;
            return View();
        }
        private static List<SelectListItem> GetLocations()
        {
            HospitalApplicationEntities entities = new HospitalApplicationEntities();
            List<SelectListItem> locationlist = (from p in entities.Hospital_location.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.Hospital_Location1,
                                                     Value = p.id.ToString()
                                                 }).ToList();
            locationlist.Insert(0, new SelectListItem { Text = "--Select Hospital--", Value = "" });

            return locationlist;
        }

        private static List<SelectListItem> GetSpecialties()
        {
            HospitalApplicationEntities entities = new HospitalApplicationEntities();
            List<SelectListItem> Specialtylist = (from p in entities.Specialties.AsEnumerable()
                                                 select new SelectListItem
                                                 {
                                                     Text = p.Specialty1,
                                                     Value = p.id.ToString()
                                                 }).ToList();
            Specialtylist.Insert(0, new SelectListItem { Text = "--Select Specialty--", Value = "" });

            return Specialtylist;
        }

        public ActionResult AllDctor()
        {
            HospitalApplicationEntities entities = new HospitalApplicationEntities();
            List<Alldoctor> listuser = new List<Alldoctor>();
            var listofdoctor = (from p in entities.ListofDoctors
                                join q in entities.Specialties on p.Specialityid equals q.id
                                join r in entities.Hospital_location on p.locid equals r.id
                                select new Alldoctor
                                {
                                    Id = p.id,
                                    Doctorname = p.Doctorname,
                                    Speciality = q.Specialty1,
                                    Location = r.Hospital_Location1

                                }).ToList();
            listuser = listofdoctor.ToList();
             return View(listuser);
        }
        public ActionResult GetListofDoctorforselected(int locid,int spid,int? userid)
        {
            List<SelectListItem> Timeslot = Gettimeslot();
            ViewBag.Timeslot = Timeslot;
            Session["userid"] = userid;
            ViewBag.Userid = userid;
            TempData["Userid"] = userid;
            HospitalApplicationEntities entities = new HospitalApplicationEntities();
            List<Alldoctor> listofdoctor = new List<Alldoctor>();
            listofdoctor = (from p in entities.ListofDoctors
                            join q in entities.Specialties on p.Specialityid equals q.id
                            join r in entities.Hospital_location on p.locid equals r.id
                            where p.Specialityid== spid && r.id==locid
                            select new Alldoctor
                            {
                                Id=p.id,
                                Doctorname = p.Doctorname,
                                Speciality = q.Specialty1,
                                Location=r.Hospital_Location1

                            }).ToList();
            return View(listofdoctor);
        }

        [HttpPost]
        public JsonResult GetDoctor()
        {
            HospitalApplicationEntities entities = new HospitalApplicationEntities();
            List<Alldoctor> listuser = new List<Alldoctor>();
            var listofdoctor = (from p in entities.ListofDoctors
                                join q in entities.Specialties on p.Specialityid equals q.id
                                join r in entities.Hospital_location on p.locid equals r.id
                                select new Alldoctor
                                {
                                    Id = p.id,
                                    Doctorname = p.Doctorname,
                                    Speciality = q.Specialty1,
                                    Location = r.Hospital_Location1

                                }).ToList();
            listuser = listofdoctor.ToList();
            return Json(listuser);

        }

        [HttpGet]
        public ActionResult Bookappointment(int id)
        {
            object Userid = Session["Userid"];
            int ids = (int)Userid;
            List<SelectListItem> Timeslot = Gettimeslot();
            ViewBag.Timeslot = Timeslot;
            ViewBag.Userid = ids;
            ViewBag.DoctorId=id;
            HospitalApplicationEntities entities = new HospitalApplicationEntities();
            List<Appointments> listofdoctor = new List<Appointments>();
            listofdoctor = (from p in entities.ListofDoctors
                            join q in entities.Specialties on p.Specialityid equals q.id
                            join r in entities.Hospital_location on p.locid equals r.id
                            where p.id== id
                            select new Appointments
                            {
                                DoctorName = p.Doctorname,
                                Speciality = q.Specialty1,
                                Location = r.Hospital_Location1

                            }).ToList();
            return View(listofdoctor);
        }

        private static List<SelectListItem> Gettimeslot()
        {
            HospitalApplicationEntities entities = new HospitalApplicationEntities();
            List<SelectListItem> Specialtylist = (from p in entities.Timeslots.AsEnumerable()
                                                  select new SelectListItem
                                                  {
                                                      Text = p.timeslot1,
                                                      Value = p.id.ToString()
                                                  }).ToList();
            Specialtylist.Insert(0, new SelectListItem { Text = "--Select TimeSlot--", Value = "" });

            return Specialtylist;
        }

        public ActionResult Gettimeslotbyavailability(int doctorid,DateTime Date)
        {
            HospitalApplicationEntities entities = new HospitalApplicationEntities();
            var data1 = (from p in entities.Timeslots
                         join q in entities.Appointments on p.id equals q.Timeslot
                         where q.Date_of_Appointment == Date && q.Doctorid == doctorid
                         select new
                         {   p.id,
                             p.timeslot1
                         });
            var data2 = (from p in entities.Timeslots
                         join q in entities.ListofDoctors on p.Doctorid equals q.id
                         where p.Doctorid == doctorid
                         select new
                         {  
                             p.id,
                             p.timeslot1      
                         });

            var data3 = (from p in data2 select p).Except(data1);
            List<SelectListItem> availabletimeslots = (from p in data3.AsEnumerable()

                                                       select new  SelectListItem
                                                       { 
                                                           Text = p.timeslot1,
                                                           Value = p.id.ToString()
                                                       }).ToList();
            
            return Json(availabletimeslots, JsonRequestBehavior.AllowGet);
        }
        public ActionResult BookappointmentSucess (DateTime date ,int userid,int doctorid,int timeslotid)
        {
            Session["userid"] = userid;
            ViewBag.Id = userid;
                 HospitalApplicationEntities db = new HospitalApplicationEntities();
                 Appointment rs = new Appointment();
                 rs.Date_of_Appointment = date;
                 rs.Userid = userid;
                 rs.Doctorid = doctorid;
                 rs.Booked_appointment_date = DateTime.Now;
                 rs.Timeslot = timeslotid;
                 db.Appointments.Add(rs);
                 db.SaveChanges();
            ViewBag.Message = "Appointment Booked Sucessfully Please check My Appointments for more details";
            return View("Bookappointment_Sucess");
        }

        public ActionResult Myappointments (int id)
        {
            Session["userid"] = id;
            ViewBag.Userid = id;
            HospitalApplicationEntities entities = new HospitalApplicationEntities();
            List<Myappointments> my_pp = new List<Myappointments>();
             my_pp = (from a in entities.Appointments
                      join b in entities.registers on a.Userid equals b.id
                      join c in entities.ListofDoctors on a.Doctorid equals c.id
                      join d in entities.Hospital_location on c.locid equals d.id
                      join e in entities.Specialties on c.Specialityid equals e.id
                      join f in entities.Timeslots on a.Timeslot equals f.id
                      where b.id==id
             select new Myappointments
                                {
                                    Id=a.id,
                                    DoctorName = c.Doctorname,
                                    Speciality = e.Specialty1,
                                    Location = d.Hospital_Location1,
                                    Dateofappointment=a.Date_of_Appointment,
                                    Timeslot=f.timeslot1,
                                    Booked_Date=a.Booked_appointment_date
                                }).ToList();
            return View(my_pp);
        }

        public ActionResult CancelAppointment(int id)
        {
            object Userid = Session["Userid"];
            int ids = (int)Userid;
            ViewBag.Id = ids;
            HospitalApplicationEntities db = new HospitalApplicationEntities();

             var x = (from y in db.Appointments where y.id==id
                                   
                                    select y).FirstOrDefault();

            db.Appointments.Remove(x);
            db.SaveChanges();

            ViewBag.Message = "Appointment Cancelled sucessfully..";
            return View("CancelAppointment");


        }

        public ActionResult Patientappointments(int id)
        {
            Session["userid"] = id;
            ViewBag.Userid = id;
           
            HospitalApplicationEntities entities = new HospitalApplicationEntities();
            List<Myappointments> my_pp = new List<Myappointments>();
            my_pp = (from a in entities.Appointments
                     join b in entities.registers on a.Userid equals b.id
                     join c in entities.ListofDoctors on a.Doctorid equals c.id
                     join d in entities.Hospital_location on c.locid equals d.id
                     join e in entities.Specialties on c.Specialityid equals e.id
                     join f in entities.Timeslots on a.Timeslot equals f.id
                     where c.id == id
                     select new Myappointments
                     {
                         Id = a.id,
                         Name = b.name,
                         Emailid=b.emailid,
                         Location = d.Hospital_Location1,
                         Dateofappointment = a.Date_of_Appointment,
                         Timeslot = f.timeslot1,
                         Booked_Date = a.Booked_appointment_date
                     }).ToList();
            return View(my_pp);
        }

    }
}