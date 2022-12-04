using NagagQuizWebv2.Models;
using NagagQuizWebv2.Viewmodels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace NagagQuizWebv2.Repository
{
    public class NagadPoll
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public NagadPoll(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<List<NagadPollModel>> GetPoll(string token)
        {

            ResponseModel_dataList<NagadPollModel> Contents = new ResponseModel_dataList<NagadPollModel>();
            try
            {
                var client = _httpClientFactory.CreateClient("getpoll");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Pole/GetPoles");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                    Contents = JsonConvert.DeserializeObject<ResponseModel_dataList<NagadPollModel>>(aresult);

                }
                return Contents.data;
            }
            catch (Exception ex)
            {

                throw;
            }

        }

        public async Task<ResponseModel_NagadPoll> PostPoll(string token, int questionId, int choiceId)
        {
            ResponseModel_NagadPoll txtContents = new ResponseModel_NagadPoll();
            try
            {
                var client = _httpClientFactory.CreateClient("PostPoll");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Pole/IsCorrectPoleResponse?questionId=" + questionId + "&choiceId=" + choiceId + "");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                    //txtContents = JsonConvert.DeserializeObject<ResponseModel_NagadPoll>(aresult);
                }
                return txtContents;
            }
            catch (Exception ex)
            {
                throw;
            }

        }
    }
}
