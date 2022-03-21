using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminMaster.Models
{
    public class Verified_Account
    {
        public int Id { get; set; }
        public string OTP { get; set; }
        public string UserId { get; set; }
        public DateTime? SendTime { get; set; }

    }
}
