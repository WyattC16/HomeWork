using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using HomeWork.Web.Models;
using HomeWork.Web.Models.Enum;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore.Internal;
using Newtonsoft.Json;

namespace HomeWork.Web.Pages
{
    public class IndexModel : PageModel
    {
        public static string Color(DateTime dueDate) => 
            dueDate.Date <= DateTime.Today.Date ? "red;" :
            dueDate.Date <= DateTime.Today.AddDays(7).Date ? "yellow;" :
            dueDate.Date <= DateTime.Today.AddMonths(1).Date ? "green;" : 
            "blue;";

        public const string BorderColor = "1px solid black;";

        public static string FormatStatus(Status status) => 
            status.ToString("F").SplitByUpperCase().Join(" ").ToProper();

        public static string SelectedOption(Status status, Status status2)
        {
            return status.ToString("F").Equals(status2.ToString("F")) ? "selected=\"selected\"" : "";
        }

        private static IEnumerable<SchoolClass> _classes;
        public static IEnumerable<SchoolClass> Classes
        {
            get
            {
                var request = (HttpWebRequest)WebRequest.Create(new Uri(new Uri(Properties.Resources.Url), "controllers/Class"));
                request.Method = "GET";
                var response = (HttpWebResponse)request.GetResponse();
                using (var stream = response.GetResponseStream())
                {
                    return _classes ?? (_classes = JsonConvert.DeserializeObject<List<SchoolClass>>(
                        new StreamReader(stream ?? throw new InvalidOperationException(), 
                            System.Text.Encoding.UTF8)
                            .ReadToEnd()));
                }
            }
        }
        public void OnGet()
        {
        }
    }
}
