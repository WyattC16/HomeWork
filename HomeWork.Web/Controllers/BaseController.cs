using System;
using System.Collections.Generic;
using System.IO;
using System.Threading.Tasks;
using HomeWork.Web.Models;
using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace HomeWork.Web.Controllers
{
    internal enum BaseControllerOptions
    {
        UseEntityFrameWork,
        UseJson,
        UseXml
    }

    [ApiController]
    [Route("Controllers/[controller]")]
    public abstract class BaseController : Controller
    {
        private static BaseControllerOptions Options => BaseControllerOptions.UseJson;
        private string DataPath { get; }

        protected List<SchoolClass> Classes
        {
            get
            {
                switch (Options)
                {
                    case BaseControllerOptions.UseEntityFrameWork:
                        throw new NotImplementedException();
                    case BaseControllerOptions.UseJson:
                        using (var reader = new StreamReader(DataPath))
                            return JsonConvert.DeserializeObject<List<SchoolClass>>(reader.ReadToEnd());
                    case BaseControllerOptions.UseXml:
                        throw new NotImplementedException();
                    default:
                        throw new ArgumentOutOfRangeException();
                }
            }
        }

        protected BaseController(IHostingEnvironment iHostingEnvironment)
        {
            switch (Options)
            {
                case BaseControllerOptions.UseEntityFrameWork:
                    //set DataPath to connectionstring here
                    throw new NotImplementedException();
                case BaseControllerOptions.UseJson:
                    DataPath = $"{iHostingEnvironment.ContentRootPath}/json/HomeWork.json";
                    break;
                case BaseControllerOptions.UseXml:
                    DataPath = $"{iHostingEnvironment.ContentRootPath}/xml/HomeWork.xml";
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }

        protected Task SaveData()
        {
            switch (Options)
            {
                case BaseControllerOptions.UseEntityFrameWork:
                    throw new NotImplementedException();
                case BaseControllerOptions.UseJson:
                    return System.IO.File.WriteAllTextAsync(DataPath,
                        JsonConvert.SerializeObject(Classes, Formatting.Indented));
                case BaseControllerOptions.UseXml:
                    throw new NotImplementedException();
                default:
                    throw new ArgumentOutOfRangeException();
            }
        }
    }
}
