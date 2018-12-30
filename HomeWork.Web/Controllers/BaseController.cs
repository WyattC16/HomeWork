using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Web.Controllers
{
    [ApiController]
    [Route("Controllers/[controller]")]
    public abstract class BaseController : Controller
    {
        protected abstract System.Collections.Generic.List<Models.SchoolClass> Classes { get; }
        protected abstract System.Threading.Tasks.Task SaveData();
    }
}
