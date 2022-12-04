using System.Collections.Generic;

namespace NagagQuizWebv2.Viewmodels
{
    public class ResponseLiveMatchcs<T> where T : class
    {
        public int success { get; set; }
        public List<T> result { get; set; }

    }
    //public class ResponseModel_dataList<T> where T : class
    //{
    //    public int status { get; set; }
    //    public string message { get; set; }
    //    public string error { get; set; }
    //    public List<T> data { get; set; }
    //}
}
