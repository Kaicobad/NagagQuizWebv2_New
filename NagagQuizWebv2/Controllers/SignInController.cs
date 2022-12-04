using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using NagagQuizWebv2.Models;
using NagagQuizWebv2.Models.UserModel;
using NagagQuizWebv2.Repository;
using NagagQuizWebv2.Viewmodels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using RestSharp;
using System;
using System.Net;
using System.Threading.Tasks;

namespace NagagQuizWebv2.Controllers
{
    public class SignInController : Controller
    {
        private readonly SignIn signin;

        public SignInController(SignIn signin)
        {
            this.signin = signin;
        }
        public IActionResult Index()
        {
            return View();
        }

        //public IActionResult validateCp(string capresponse)
        //{
        //    if (string.IsNullOrEmpty(capresponse)){
        //        return Ok(false);
        //    }
        //    else
        //    {
        //        var options = new RestClientOptions(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", "6LfD4CEjAAAAAP01GUOY9FNqw2Uc0PxWm42raud-", capresponse));
        //        var client = new RestClient(options);
        //        var request = new RestRequest();
        //        request.Method = Method.Post;
        //        RestResponse response = (RestResponse)client.Execute(request);
        //        var content = response.Content;
        //        var result = JsonConvert.DeserializeObject<CaptchaResponse>(content);
        //        return Ok(result.Success);
        //    }
        //}
        public async Task<IActionResult> CreateUser(string mobileNumber)
        {
            try
            {               
                string SessionKeyName = "UserName";
                string SessionKeyToken = "Token";
                string SessionMobile = "Mobile";

                var Session = HttpContext.Session;
                SIgnInViewmodel result =await signin.GetUserStatus(mobileNumber);


                string UserName =result.userName.ToString();
                string Token = result.token.ToString();

                if (string.IsNullOrEmpty(HttpContext.Session.GetString(SessionKeyName)))
                {
                    Session.SetString(SessionKeyName, UserName);
                    Session.SetString(SessionKeyToken, Token);
                    Session.SetString(SessionMobile, mobileNumber);
                }

                if (!string.IsNullOrEmpty(Token))
                {
                    return RedirectToAction("Index", "Home");
                }
                else
                {
                    ViewBag.msg = "Something went wrong";
                    return View("ErrorView");
                }



            }
            catch(Exception ex)
            {
                ViewBag.msg = "Something went wrong";
                return View("ErrorView");
            }
           
        }


        public IActionResult Logout()
        {

            return RedirectToAction("Index", "Profile");
        }
        
    }
}
