using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NagagQuizWebv2.Repository;
using System.Threading.Tasks;

namespace NagagQuizWebv2.Controllers
{
    public class LeaderboardController : Controller
    {
        private readonly UserProfileServices _profileService;
        public LeaderboardController(UserProfileServices profileService)
        {
            _profileService = profileService;
        }
        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Profile");
            }

            return View();
        }

        public async Task<IActionResult> GetYesterdayResult(int totalpage)
        {
            var token = HttpContext.Session.GetString("Token");
            var data =await _profileService.GetYesterdaysResult(token,totalpage);
            ViewBag.data = data;
            return PartialView("GetTodaysResult");
        }


        public async Task<PartialViewResult>  GetTodaysResult(int totalpage)
        {
            var token = HttpContext.Session.GetString("Token");
            var data =await _profileService.GetResult(token, totalpage);
            ViewBag.data = data;
            return PartialView("GetTodaysResult");
        }

        public async Task<PartialViewResult> GetWeeklyResult()
        {
            var token = HttpContext.Session.GetString("Token");
            var data =await _profileService.GetResultWeekly(token);
            ViewBag.data = data;
            return PartialView("GetTodaysResult");
        }


    }
}
