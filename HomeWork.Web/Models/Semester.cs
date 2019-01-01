using System.Collections.Generic;
using HomeWork.Web.Models.Enum;

namespace HomeWork.Web.Models
{
    public class Semester
    {
        public IList<SchoolClass> Classes { get; set; }
        public Seasons Season { get; set; }
        public int Year { get; set; }
    }
}
