
using NagagQuizWebv2.Models;
using Newtonsoft.Json;
using System;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System.Collections.Generic;
using NagagQuizWebv2.Viewmodels;

namespace NagagQuizWebv2.Repository
{
    public class QuizQuestion
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public QuizQuestion(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }
        public async Task<(List<QuizQuestionModel>, string)> GetQuestion(string token)
        {

            List <QuizQuestionModel> aList = new List<QuizQuestionModel>();
            string matctoken = "";
            try
            {
                var client = _httpClientFactory.CreateClient("question");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Quiz/GetQuestionsForAMatchPro");
                var httpResponse = await client.GetAsync(url);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;

                    ResponseDataCustom<List<QuizQuestionModel>> con = JsonConvert.DeserializeObject<ResponseDataCustom<List<QuizQuestionModel>>>(aresult);
                    matctoken = con.matchToken;
                    aList = con.data;
                }
                return (aList, matctoken);
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
}
