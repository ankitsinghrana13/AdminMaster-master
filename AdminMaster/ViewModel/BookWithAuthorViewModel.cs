using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace AdminMaster.ViewModel
{
    public class BookWithAuthorViewModel
    {
        public int id { get; set; }
        public string Title { get; set; }
        public decimal Price { get; set; }
        public int Quantity { get; set; }
        public string Published_On { get; set; }
     
        public string AuthorName { get; set; }
        public string AuthorMobile { get; set; }
        public string AuthorEmail { get; set; }
    }
}
