using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SafeCity2607last.Models
{
    public class ApplicationUser : IdentityUser
    {
        //public ApplicationUser(): base()
        //{

        //}
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public int UsernameChangeLimit { get; set; } = 10;
        public byte[] ProfilePicture { get; set; }
        public string Adresse { get; internal set; }
        public string CIN { get; internal set; }
        public string City { get; internal set; }
        public string PhoneNumber2 { get; internal set; }
        public string Lati { get; internal set; }
        public string Long { get; internal set; }
        public string Function { get; internal set; }
        //public string Function { get; set; }

        //public string CIN { get; set; }

        //public string Adresse { get; set; }

        //public string City { get; set; }

        ////public string PhoneNumber1 { get; set; }


        //public string PhoneNumber2 { get; set; }

        //public string photo { get; set; }

        //public DateTime DateDebut { get; set; }

        //public DateTime DateFin { get; set; }
        //[DataType(DataType.Date)]
        //[DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        //[Display(Name = "Enrollment Date")]
        //public DateTime EnrollmentDate { get; set; }

        //public string Lati { get; set; }


        //public string Long { get; set; }



    }
}
