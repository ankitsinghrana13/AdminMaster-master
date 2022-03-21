using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminMaster.Models
{
    public class Tbl_Users
    {
        public int Id { get; set; }
        public string F_Name { get; set; }
        public string L_Name { get; set; }
        public string Gender { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public string Profile_Image { get; set; }
        public bool isActive { get; set; }
        public bool isVerified { get; set; }
        public DateTime? Create_On { get; set; }
        public DateTime? Update_On { get; set; }


    }
}
