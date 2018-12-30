using HomeWork.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;

namespace HomeWork.Web.Controllers
{
    public class HomeWorkController : BaseJsonController
    {

        public HomeWorkController(IHostingEnvironment iHostingEnvironment) : base(iHostingEnvironment)
        {
        }

        [HttpGet("{class}/homework")]
        public ActionResult<List<Models.HomeWork>> GetHomeWorks(string title)
        {
            return Classes?.SingleOrDefault(x => 
                string.Equals(x.Title, title, StringComparison.CurrentCultureIgnoreCase))?.HomeWorks;
        }

        [HttpGet("{class}/{homework}")]
        public ActionResult<Models.HomeWork> GetHomeWork(string classTitle, string homeworkTitle)
        {
            return Classes?.SingleOrDefault(x => 
                    string.Equals(x.Title, classTitle, StringComparison.CurrentCultureIgnoreCase))
                ?.HomeWorks.SingleOrDefault(x => x.Title == homeworkTitle);
        }

        [HttpPost("{class}/{homeworks}")]
        public async void Post(string classTitle, [FromBody] IEnumerable<Models.HomeWork> homeWorks)
        {
            var originalClass = Classes.SingleOrDefault(x =>
                string.Equals(x.Title, classTitle, StringComparison.CurrentCultureIgnoreCase));
            if (originalClass == null)
            {
                Classes.Add(new SchoolClass
                {
                    HomeWorks = homeWorks.ToList(),
                    Title = classTitle
                });
            }
            else
            {
                var newClass = originalClass;
                newClass.HomeWorks.AddRange(homeWorks);
                Classes[Classes.IndexOf(originalClass)] = newClass;
            }
            await SaveData();
        }

        [HttpPost("{class}/{homework}")]
        public async void Post(string classTitle, [FromBody] Models.HomeWork homeWork)
        {
            var originalClass = Classes.SingleOrDefault(x =>
                string.Equals(x.Title, classTitle, StringComparison.CurrentCultureIgnoreCase));
            if (originalClass == null)
            {
                Classes.Add(new SchoolClass
                {
                    HomeWorks = new List<Models.HomeWork>
                    {
                        homeWork
                    },
                    Title = classTitle
                });
            }
            else
            {
                var newClass = originalClass;
                newClass.HomeWorks.Add(homeWork);
                Classes[Classes.IndexOf(originalClass)] = newClass;
            }
            await SaveData();
        }
    }
}
