using System.Collections.Generic;

namespace HomeWork.Web.Models
{
    public class SchoolHistory
    {
        public IEnumerable<Semester> Semesters { get; set; }
        public string User { get; set; }
    }
}
