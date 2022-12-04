using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using NagagQuizWebv2.Models;
using NagagQuizWebv2.Repository;
using System.Diagnostics;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace NagagQuizWebv2.Controllers
{
    public class HomeController : Controller
    {
        public UserProfileServices _profileService;
        public Category _category;
        public LiveScore liveScore;

        public HomeController(UserProfileServices _hajjService,Category category, LiveScore liveScore)
        {
            this._profileService = _hajjService;
            this._category = category;
            this.liveScore = liveScore;
        }

        public async Task<IActionResult> Index()
        {
            var token = HttpContext.Session.GetString("Token");
            if (string.IsNullOrEmpty(token))
            {
                return RedirectToAction("Index", "Profile");
            }
 
            var mobile = HttpContext.Session.GetString("Mobile");
            ViewBag.mobile=mobile;
            return View();
        }

        [HttpGet]
        public async Task<IActionResult> GetLiveWorldCupScore()
        {
            var result = await liveScore.GetLiveScore();

            List<LiveMatchModel> liveScoreList = new List<LiveMatchModel>();

            if (result == null)
            {
                liveScoreList = null;
            }
            else
            {
                foreach (var item in result)
                {
                    if (item.country_name == "Worldcup")
                    {
                        liveScoreList.Add(item);
                    }
                }
            }
            return Json(liveScoreList);
        }

        public async Task<IActionResult> GetuserScore()
        {
            var token = HttpContext.Session.GetString("Token");
            var data = await _profileService.GetUserProfile(token);

            return Ok(data);

        }

        //[HttpGet]
        //public async Task<IActionResult> GetCategories()
        //{
        //    string SessionKeyToken = "Token";
        //    var Session = HttpContext.Session;
        //    var token = HttpContext.Session.GetString(SessionKeyToken);
            
        //    if (!string.IsNullOrEmpty(token)) 
        //    {
        //        ResponseModel? result = _category.GetCategories(token);
        //        var data = result.data.ToJson();
        //        dynamic source = JObject.Parse(data);
               
        //        ViewBag.category = source;

        //        return View("Index");
        //    }
        //    return RedirectToAction("Index");
        //}

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