using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Doctor_appointment.Models
{
    public class Logindoctor
    {
        public int id { get; set; }

        [Display(Name = "Name")]
        public string Name { get; set; }

        [Display(Name = "Email id")]
        [RegularExpression(@"\A(?:[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?)\Z",
        ErrorMessage = "Please enter correct email address")]
        public string Emailid { get; set; }
        [Display(Name = "Password")]

        [DataType(DataType.Password)]
        public string Userpassword { set; get; }
        [Display(Name = "Confirm new password")]
        [Required(ErrorMessage = "Enter Confirm Password")]
        [Compare("Userpassword", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string c_pwd { get; set; }

    }

    public class Searchdoctor
    {
        public string searchk(Logindoctor li)
        {

            HospitalApplicationEntities db = new HospitalApplicationEntities();
            ListofDoctor rs = new ListofDoctor();
            string passout = "";
            var pass = from m in db.ListofDoctors where m.Email == li.Emailid select m.Password;
            foreach (string query in pass)
            {
                passout = query;

            }
            return passout;

        }
    }
        public class GetDoctorid
        {
            public int Searchbyusername(string li)
            {

                HospitalApplicationEntities db = new HospitalApplicationEntities();
                ListofDoctor rs = new ListofDoctor();

                var id = (from m in db.ListofDoctors where m.Email == li select m.id).FirstOrDefault();

                return id;

            }

        }
    }
