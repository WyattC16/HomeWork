using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Web.Pages
{
    public class ContactModel : PageModel
    {
        public string Message { get; private set; }

        public void OnGet()
        {
            Message = "Your contact page.";
        }
    }
}
