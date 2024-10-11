using Microsoft.AspNetCore.Mvc;

namespace MediscreenCMS.Controllers
{
    public class DoctorNotesController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
