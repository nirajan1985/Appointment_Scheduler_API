using Microsoft.AspNetCore.Mvc;

namespace AppointmentSchedulerAPI.Controllers
{
    public class AppointmentCategoryController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
