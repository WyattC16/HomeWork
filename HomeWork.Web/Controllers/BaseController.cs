using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HomeWork.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeWork.Web.Controllers
{
    [ApiController]
    [Route("Controllers/[controller]")]
    public class BaseController : Controller
    {
        protected List<SchoolClass> Classes { get; }
        private IHostingEnvironment HostingEnvironment { get; }
        private string JsonPath { get; }
        private string FormattedJson => JsonConvert.SerializeObject(Classes.ToArray(), Formatting.Indented);

        public BaseController(IHostingEnvironment iHostingEnvironment)
        {
            HostingEnvironment = iHostingEnvironment;
            JsonPath = $"{HostingEnvironment.ContentRootPath}/json/HomeWork.json";
            using (var reader = new StreamReader(JsonPath))
            {
                Classes = JsonConvert.DeserializeObject<List<SchoolClass>>(reader.ReadToEnd());
            }
        }
        protected Task SaveJson()
        {
            return System.IO.File.WriteAllTextAsync(JsonPath, FormattedJson);
        }
    }
}
