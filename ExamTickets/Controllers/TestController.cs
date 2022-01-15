using Microsoft.AspNetCore.Mvc;

namespace ExamTickets.Controllers
{
    public class TestController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
