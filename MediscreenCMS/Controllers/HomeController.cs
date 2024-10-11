using MediscreenCMS.Models;
using MediscreenCMS.Services;
using Microsoft.AspNetCore.Mvc;
using System.Diagnostics;

namespace MediscreenCMS.Controllers
{
    public class HomeController : Controller
    {
        private readonly ILogger<HomeController> _logger;
        private readonly DoctorNotesServices _doctorNotesServices;

        public HomeController(ILogger<HomeController> logger, DoctorNotesServices doctorNotesServices)
        {
            _logger = logger;
            _doctorNotesServices = doctorNotesServices;
        }

        public IActionResult Index()
        {
            return View();
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
