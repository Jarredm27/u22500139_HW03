using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Services.Description;

namespace HW03.Models
{
    public class ChartData
    {
        public List<string> Labels { get; set; }
        public List<int> Values { get; set; }

        public List<string> ClassNames { get; set; }
        public List<int> StudentCounts { get; set; }

        public IEnumerable<student> Students { get; set; }

        public IEnumerable<book> Books { get; set; }

        public IEnumerable<borrow> Borrows { get; set; }

        public IEnumerable<type> Types { get; set; }

        public IEnumerable<author> Authors { get; set; }

        public int TotalStudents { get; set; }
        public int TotalBooks { get; set; }
        public int TotalBorrows { get; set; }

        public List<MostPopularBookViewModel> MostPopularBooks { get; set; }
        public List<StudentsByClassViewModel> StudentsByClass { get; set; }

        public class MostPopularBookViewModel
        {
            public string BookName { get; set; }
            public int TotalBorrows { get; set; }
        }

        public class StudentsByClassViewModel
        {
            public string ClassName { get; set; }
            public int TotalStudents { get; set; }
        }
    }
}
