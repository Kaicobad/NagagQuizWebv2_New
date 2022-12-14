using NagagQuizWebv2.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Security.Policy;
using NagagQuizWebv2.Viewmodels;

namespace NagagQuizWebv2.Repository
{
    public class LiveScore
    {
        private readonly IHttpClientFactory _httpClientFactory;

        public LiveScore(IHttpClientFactory clientFactory)
        {
            this._httpClientFactory = clientFactory;
        }
        public async Task<List<LiveMatchModel>> GetLiveScore()
        {
            string apiurl= "apiv2.allsportsapi.com/football?met=Livescore&APIkey=";
            string apikey = "7653e6c951f05d491c8f6de181739dac550d0c64ad1bfc9fb49feb453e421d03";

            ResponseLiveMatchcs<LiveMatchModel> Contents = new ResponseLiveMatchcs<LiveMatchModel>();
            try
            {
                var client = _httpClientFactory.CreateClient("getlivescore");
                client.DefaultRequestHeaders.Clear();
                //client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                //var url = string.Format("https://apiv2.allsportsapi.com/football/?met=Livescore&APIkey=7653e6c951f05d491c8f6de181739dac550d0c64ad1bfc9fb49feb453e421d03");
                var url = string.Format("https://apiv2.allsportsapi.com/football/?met=Livescore&APIkey=4e9359ac625f9c9014724ce3a2106fa6bb5caecb1cb3e60ed33c508e20a87788");

                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                    Contents = JsonConvert.DeserializeObject<ResponseLiveMatchcs<LiveMatchModel>>(aresult);

                }
                return Contents.result;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
