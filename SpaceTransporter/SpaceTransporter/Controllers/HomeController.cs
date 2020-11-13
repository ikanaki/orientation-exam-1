using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using SpaceTransporter.Interfaces;

namespace SpaceTransporter.Controllers
{
    [Route("")]
    public class HomeController : Controller
    {
        public IService Service { set; get; }
        public HomeController(IService service)
        {
            Service = service;
        }


        [HttpGet("")]
        public IActionResult FrontendGet()
        {
            return View("Frontend");
        }
    }
}
