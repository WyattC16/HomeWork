using System.Collections.Generic;
using System.Linq;
using HomeWork.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;

namespace HomeWork.Web.Controllers
{
    public class ClassController : BaseJsonController
    {
        public ClassController(IHostingEnvironment iHostingEnvironment) : base(iHostingEnvironment)
        {
        }

        [HttpGet]
        public ActionResult<List<SchoolClass>> GetClasses() => Classes;

        [HttpPost]
        public async void Post([FromBody] SchoolClass schoolClass)
        {
            Classes.Add(schoolClass);
            await SaveData();
        }
        
        [HttpPut]
        public async void Put([FromBody] SchoolClass schoolClass)
        {
            if (Classes.Contains(schoolClass)) return;
            if (Classes.Any(x => x.Title == schoolClass.Title))
            {
                var classToReplace = Classes.Single(x => x.Title == schoolClass.Title);
                Classes[Classes.IndexOf(classToReplace)] = schoolClass;
                await SaveData();
                return;
            }
            Post(schoolClass);
        }
    }
}
