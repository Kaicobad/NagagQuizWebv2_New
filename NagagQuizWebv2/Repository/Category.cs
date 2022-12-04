using NagagQuizWebv2.Models;
using Newtonsoft.Json;
using RestSharp;

namespace NagagQuizWebv2.Repository
{
    public class Category
    {
        ApiUrl apiUrl = new ApiUrl();
        public ResponseModel GetCategories(string token)
        {
            var client = new RestClient(apiUrl.GameCategoryUrl);
            var request = new RestRequest();
            request.Method = Method.GET;
            request.AddHeader("bearer", token);
            request.AddHeader("Content-Type", "application/json");

            var response = client.Execute(request);
            var content = response.Content;

            var result = JsonConvert.DeserializeObject<ResponseModel>(content);
            return result;
        }
    }
}
