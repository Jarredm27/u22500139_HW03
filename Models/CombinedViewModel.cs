using HW03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PagedList;

namespace HW03.Models
{
    public class CombinedViewModel
    {
        public IPagedList<student> Students { get; set; }
        public IEnumerable<author> Authors { get; set; }
        public IPagedList<book> Books { get; set; }
        public int BookId { get; set; }
        public DateTime takendate { get; set; }
        public IEnumerable<borrow> Borrows { get; set; }
        public IEnumerable<type> Types { get; set; }
    }
}