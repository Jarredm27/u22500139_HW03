using HW03.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.UI.WebControls;
using PagedList;

namespace HW03.Models
{
    public class MaintainVM
    {
        public IEnumerable<student> students { get; set; }
        public IEnumerable<book> books { get; set; }
        public IPagedList<author> authors { get; set; }
        public IPagedList<type> types { get; set; }
        public IPagedList<borrow> borrows { get; set; }
    }
}