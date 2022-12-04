using NagagQuizWebv2.Models;
using NagagQuizWebv2.Viewmodels;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace NagagQuizWebv2.Repository
{
    public class UserProfileServices
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public UserProfileServices(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<List<TopResult>> GetResult(string token,int totalpage)
        {

            List<TopResult> aList = new List<TopResult>();
            try
            {
                var client = _httpClientFactory.CreateClient("result");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Quiz/GetDailyLeaderboard?numberOfResults="+ totalpage);
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;

                    var con = JsonConvert.DeserializeObject<ResponseData<List<TopResult>>>(aresult);
                    aList = con.data;
                }
                return aList;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<TopResult>> GetYesterdaysResult(string token, int totalpage)
        {

            List<TopResult> aList = new List<TopResult>();
            try
            {
                var client = _httpClientFactory.CreateClient("result");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Quiz/GetDailyWinners?numberOfResults="+ totalpage);
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                    var con = JsonConvert.DeserializeObject<ResponseData<List<TopResult>>>(aresult);
                    aList = con.data;
                }
                return aList;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<List<TopResult>> GetResultWeekly(string token)
        {

            List<TopResult> aList = new List<TopResult>();
            try
            {
                var client = _httpClientFactory.CreateClient("result");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Quiz/GetWeeklyLeaderboard?numberOfResults=10");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;

                    var con = JsonConvert.DeserializeObject<ResponseData<List<TopResult>>>(aresult);
                    aList = con.data;
                }
                return aList;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<UserProfile> GetUserProfile(string token)
        {

            UserProfile aList = new UserProfile();
            try
            {
                var client = _httpClientFactory.CreateClient("result");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/User/GetUserDetails");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;

                    var content = JsonConvert.DeserializeObject<ResponseData<UserProfile>>(aresult);
                    aList = content.data;
                }
                return aList;
            }
            catch (Exception ex)
            {

                throw;
            }

        }




        //public bool ValidateCaptch(string capresn)
        //{
        //    var options = new RestClientOptions(string.Format("https://www.google.com/recaptcha/api/siteverify?secret={0}&response={1}", "6LdTSyEjAAAAAPGE7pjsAr-p7MilxiQmtVlAr14H", capresn));
        //    var client = new RestClient(options);
        //    var request = new RestRequest();
        //    request.Method = Method.Post;
        //    RestResponse response = client.Execute(request);
        //    var content = response.Content;
        //    var result = JsonConvert.DeserializeObject<CaptchaResponse>(content);

        //    return false;
        //}
    }
}
