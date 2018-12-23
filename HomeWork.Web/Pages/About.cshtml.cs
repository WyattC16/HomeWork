using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Web.Pages
{
    public class AboutModel : PageModel
    {
        public string Message { get; private set; }

        public void OnGet()
        {
            Message = "This application is used to manage homework assignments.";
        }
    }
}
