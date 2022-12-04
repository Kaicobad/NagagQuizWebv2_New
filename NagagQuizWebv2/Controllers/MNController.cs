using Microsoft.AspNetCore.Mvc;

namespace NagagQuizWebv2.Controllers
{
    public class MNController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}
