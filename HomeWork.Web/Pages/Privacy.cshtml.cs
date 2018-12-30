using Microsoft.AspNetCore.Mvc.RazorPages;

namespace HomeWork.Web.Pages
{
    public class PrivacyModel : PageModel
    {
        public static string Message { get; private set; }
        public void OnGet()
        {
            Message = "This website uses cookies to improve your experience.";
        }
    }
}