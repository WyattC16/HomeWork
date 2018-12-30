using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HomeWork.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace HomeWork.Web.Controllers
{
    public class BaseJsonController : BaseController
    {
        protected override List<SchoolClass> Classes
        {
            get
            {
                using (var reader = new StreamReader(JsonPath))
                    return JsonConvert.DeserializeObject<List<SchoolClass>>(reader.ReadToEnd());
            }
        }

        private string JsonPath { get; }

        public BaseJsonController(IHostingEnvironment iHostingEnvironment)
        {
            JsonPath = $"{iHostingEnvironment.ContentRootPath}/json/HomeWork.json";
        }

        protected override Task SaveData() => 
            System.IO.File.WriteAllTextAsync(JsonPath, 
                JsonConvert.SerializeObject(Classes, Formatting.Indented));
    }
}
