using NagagQuizWebv2.Models;
using NagagQuizWebv2.Viewmodels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NagagQuizWebv2.Repository
{
    public class Prediction
    {

        private readonly IHttpClientFactory _httpClientFactory;
        public Prediction(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<PredictionModel> GetPredictionEvent(string token)
        {

            ResponseData<PredictionModel> aList = new ResponseData<PredictionModel>();
            try
            {

                var client = _httpClientFactory.CreateClient("prediction");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Events/GetAllEvents");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                    aList = JsonConvert.DeserializeObject<ResponseData<PredictionModel>>(aresult);
                }
                return aList.data;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public async Task<List<PredictionModel>> GetPredictionEventList(string token)
        {

            ResponseModel_dataList<PredictionModel> aList = new ResponseModel_dataList<PredictionModel>();
            try
            {

                var client = _httpClientFactory.CreateClient("prediction");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Events/GetAllEvents");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;

                    aList = JsonConvert.DeserializeObject<ResponseModel_dataList<PredictionModel>>(aresult);

                }
                return aList.data;
            }
            catch (Exception ex)
            {

                throw;
            }

        }


        public async Task<PredictionModel> PostPrediction(int EventId, int PredictedWinningTeamId, string token)
        {


            ResponseData<PredictionModel> Contents = new ResponseData<PredictionModel>();
            try
            {
                var body = new
                {
                    EventId = EventId,
                    PredictedWinningTeamId = PredictedWinningTeamId
                };
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, Application.Json);
                var client = _httpClientFactory.CreateClient("createprediction");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Events/SubmitPrediction");
                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                    Contents = JsonConvert.DeserializeObject<ResponseData<PredictionModel>>(aresult);
                }
                return Contents.data;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
