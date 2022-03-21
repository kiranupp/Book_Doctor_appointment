using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Doctor_appointment.Models
{
    public class Myappointments
    {

        public string Name { get; set; }

        public string Emailid { get; set; }
        public int Id { get; set; }
        [Display(Name ="Doctor Name")]
        public string DoctorName { get; set; }
        public string Speciality { get; set; }
        public string Location { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date of Appointment")]
        public DateTime? Dateofappointment { get; set; }

        public string Timeslot { get; set; }
        
        public DateTime? Booked_Date { get; set; }
      
    }
}