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
            DataPath = $"{iHostingEnvironment.ContentRootPath}/json/HomeWork.json";
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
                        JsonConvert.DeserializeObject<SchoolHistory>(reader.ReadToEnd()).
                            Semesters.SingleOrDefault(x =>
                                x.Year == year &&
                                x.Season == season)));
        }

        [HttpPut("")]
        public IActionResult SaveJson([FromBody] Semester semester)
        {
            SchoolHistory history;
            using (var reader = new StreamReader(DataPath))
                history = JsonConvert.DeserializeObject<SchoolHistory>(reader.ReadToEnd());
            if (history.Semesters.Any(x => 
                x.Season == semester.Season && 
                x.Year == semester.Year))
            {
                var oldSemester = history.Semesters.Single(x =>
                    x.Season == semester.Season &&
                    x.Year == semester.Year);
                history.Semesters = history.Semesters.Select(x => x == oldSemester ? semester : x);
            }
            else
            {
                var oldSemesters = history.Semesters.ToList();
                oldSemesters.Add(semester);
                history.Semesters = oldSemesters;
            }
            return Ok(history);
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
