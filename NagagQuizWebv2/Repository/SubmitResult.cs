using Microsoft.AspNetCore.Mvc;
using NagagQuizWebv2.Models;
using NagagQuizWebv2.Viewmodels;
using Newtonsoft.Json;
using RestSharp;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Threading.Tasks;
using static System.Net.Mime.MediaTypeNames;

namespace NagagQuizWebv2.Repository
{
    public class SubmitResult
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SubmitResult(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<ResponseModel> PostSubmitResult(MatchResultModel result, string token)
        {

            ResponseModel aList = new ResponseModel();
            try
            {
                var body = new
                {
                    Msisdn = result.Msisdn,
                    OnDate = result.OnDate,
                    Score = result.Score,
                    TimeInSeconds = result.TimeInSeconds
                };

                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, Application.Json);
                var client = _httpClientFactory.CreateClient("postresult");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Quiz/MatchResult");
                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<ResponseModel>(aresult);
                    aList = data;
                }
                return aList;
            }
            catch (Exception ex)
            {

                throw;
            }

        }



        public async Task<MatchSummery> PostSubmitResul_NEWwithList( MatchSummery result, string token)
         {

            MatchSummery aList = new MatchSummery();
            try
            {
                var dsBody = new
                {
                    OnDate = DateTime.Now,
                    Score = result.score,
                    TimeInSeconds = result.timeInMilliseconds
                };

                var body = dsBody;
                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8, Application.Json);
                var client = _httpClientFactory.CreateClient("postresultnm");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Add("Authorization", "Bearer " + token);
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/Quiz/MatchResult");
                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                    //ResponseData<MatchSummery> con = JsonConvert.DeserializeObject<ResponseData<MatchSummery>>(aresult);
                    //aList = con.data;
                }
                return aList;
            }
            catch (Exception ex)
            {

                throw;
            }

        }
    }
    }

