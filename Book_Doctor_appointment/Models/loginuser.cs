using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Book_Doctor_appointment.Models
{
    public class loginuser
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
        //[Required(ErrorMessage = "Enter Confirm Password")]
        [Compare("Userpassword", ErrorMessage = "The password and confirmation password do not match.")]
        [DataType(DataType.Password)]
        public string c_pwd { get; set; }


    }
    public class Enterintotable
    {
        public void InsertUser(loginuser li)
        {
            HospitalApplicationEntities db = new HospitalApplicationEntities();
            register rs = new register();
            rs.id = li.id;
            rs.name = li.Name;
            rs.emailid = li.Emailid;
            rs.userpassword = li.Userpassword;
            db.registers.Add(rs);
            db.SaveChanges();

        }
    }

    public class Searchuser
    {
        public string searchk(loginuser li)
        {

            HospitalApplicationEntities db = new HospitalApplicationEntities();
            register rs = new register();
            string passout = "";
            var pass = from m in db.registers where m.emailid == li.Emailid select m.userpassword;
            foreach (string query in pass)
            {
                passout = query;

            }
            return passout;

        }

    }

    public class GetUserid
    {
        public int Searchbyusername(string li)
        {

            HospitalApplicationEntities db = new HospitalApplicationEntities();
            register rs = new register();

            var id = (from m in db.registers where m.emailid == li select m.id).FirstOrDefault();

            return id;

        }

    }
}