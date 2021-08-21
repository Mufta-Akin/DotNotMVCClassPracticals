using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;

using SMS.Data.Services;
using SMS.Web.Models;

namespace SMS.Web.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;

        private IStudentService _svc;

        public HomeController(ILogger<HomeController> logger, IStudentService svc)
        {
            _logger = logger;
            _svc = svc;
        }

        public IActionResult Index()
        {            
            return View();
        }

        public IActionResult About()
        {
            var about = new AboutViewModel {
                Title = "About",
                Message = "Our mission is to develop great solutions for educational student management.",
                Formed = new DateTime(2000,10,1)
            };
            return View(about);
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
