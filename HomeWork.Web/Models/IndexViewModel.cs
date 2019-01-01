namespace HomeWork.Web.Models
{
    public class IndexViewModel
    {
        public IndexViewModel(){ }

        public IndexViewModel(Semester semester)
        {
            Semester = semester;
        }

        public Semester Semester { get; set; }
    }
}
