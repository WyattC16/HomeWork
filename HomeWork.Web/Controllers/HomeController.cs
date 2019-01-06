using System.Collections.Generic;
using System.Diagnostics;
using System.IO;
using System.Linq;
using Microsoft.AspNetCore.Mvc;
using HomeWork.Web.Models;
using HomeWork.Web.Models.Enum;
using Microsoft.AspNetCore.Hosting;
using Newtonsoft.Json;

namespace HomeWork.Web.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        private string DataPath { get; }
        public HomeController(IHostingEnvironment iHostingEnvironment)
        {
            DataPath = $"{iHostingEnvironment.ContentRootPath}\\json\\HomeWork.json";
        }

        [HttpGet("")]
        public IActionResult Index()
        {
            return View("Index", new IndexViewModel());
        }

        [HttpGet("/{season}/{year}")]
        public IActionResult IndexWithModel(Seasons season, int? year)
        {
            using (var reader = new StreamReader(DataPath))
                return View("Index",
                    new IndexViewModel(
                        JsonConvert.DeserializeObject<IEnumerable<Semester>>(reader.ReadToEnd()).
                            SingleOrDefault(x =>
                                x.Year == year &&
                                x.Season == season) ?? new Semester
                        {
                            Season = season,
                            Year = year ?? default
                        }));
        }

        [HttpPut("")]
        public IActionResult SaveJson([FromBody] Semester semester)
        {
            List<Semester> semesters;
            if (semester == null)
            {
                return BadRequest();
            }
            using (var reader = new StreamReader(DataPath))
            {
                semesters = JsonConvert.DeserializeObject<List<Semester>>(reader.ReadToEnd());
                if (semesters.Any(x =>
                    x.Season == semester.Season &&
                    x.Year == semester.Year))
                {
                    var oldSemester = semesters.Single(x =>
                        x.Season == semester.Season &&
                        x.Year == semester.Year);
                    semesters = semesters.Select(x => x == oldSemester ? semester : x).ToList();
                }
                else
                {
                    var oldSemesters = semesters.ToList();
                    oldSemesters.Add(semester);
                    semesters = oldSemesters;
                }
            }
            var task = System.IO.File.WriteAllTextAsync(DataPath, 
                JsonConvert.SerializeObject(semesters, 
                    Formatting.Indented));
            task.Wait();
            return Ok();
        }

        [HttpGet("/Error")]
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel
            {
                RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier
            });
        }
    }
}
