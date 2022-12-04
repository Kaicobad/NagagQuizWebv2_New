using NagagQuizWebv2.Models;
using Newtonsoft.Json;
using RestSharp;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Threading.Tasks;
using System;
using System.Text;
using static System.Net.Mime.MediaTypeNames;
using NagagQuizWebv2.Viewmodels;

namespace NagagQuizWebv2.Repository
{
    public class SignIn 
    {
        private readonly IHttpClientFactory _httpClientFactory;
        public SignIn(IHttpClientFactory clientFactory)
        {
            _httpClientFactory = clientFactory;
        }

        public async Task<SIgnInViewmodel> GetUserStatus(string UserName)
        {

            SIgnInViewmodel aList = new SIgnInViewmodel();
            try
            {
                var body = new
                {
                    UserName = UserName
                };

                var sr = JsonConvert.SerializeObject(body);

                var content = new StringContent(JsonConvert.SerializeObject(body), Encoding.UTF8,Application.Json);
                var client = _httpClientFactory.CreateClient("signin");
                client.DefaultRequestHeaders.Clear();
                client.DefaultRequestHeaders.Accept.Add(new MediaTypeWithQualityHeaderValue("application/json"));
                var url = string.Format("https://nagadapi.shadhinquiz.com/api/User/NagadSignIn");
                var httpResponse = await client.PostAsync(url, content);
                if (httpResponse.IsSuccessStatusCode)
                {
                    var aresult = httpResponse.Content.ReadAsStringAsync().Result;
                    var data = JsonConvert.DeserializeObject<ResponseData<SIgnInViewmodel>>(aresult);
                    aList = data.data;
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
