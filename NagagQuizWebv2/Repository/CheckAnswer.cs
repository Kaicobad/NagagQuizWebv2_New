using NagagQuizWebv2.Models;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Reflection.Metadata;
using System.Threading.Tasks;

namespace NagagQuizWebv2.Repository
{
    public class CheckAnswer
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public CheckAnswer(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<ResponseModel> AnswerStatus(int questionId, int choiceId, string token)
        {
            ResponseModel txtContents = new ResponseModel();
            try
            {
                var client = _httpClientFactory.CreateClient("anwser");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Quiz/IsCorrectChoice?questionId=" + questionId +"&choiceId="+ choiceId + "");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                     txtContents = JsonConvert.DeserializeObject<ResponseModel>(aresult);
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
