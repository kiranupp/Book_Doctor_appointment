using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Book_Doctor_appointment.Models
{
    public class Appointments
    {
        public int Userid { get; set; }
        public int DoctorId { get; set; }
        [Display(Name ="Doctor Name")]
        public string DoctorName { get; set; }
        public string Speciality { get; set; }
        public string Location { get; set; }

        [DataType(DataType.Date)]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        public DateTime? Date { get; set; }

        [DisplayName("Available Time Slots")]
        public IEnumerable<SelectListItem> Timeslot { get; set; }



    }
}