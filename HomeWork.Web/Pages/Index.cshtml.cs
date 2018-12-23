using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using HomeWork.Web.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Newtonsoft.Json;

namespace HomeWork.Web.Pages
{
    public class IndexModel : PageModel
    {
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
