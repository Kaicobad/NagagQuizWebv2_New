namespace NagagQuizWebv2.Viewmodels
{
    public class ResponseDataCustom<T> where T : class
    {
        public int status { get; set; }
        public string message { get; set; }
        public string error { get; set; }
        public string matchToken { get; set; }
        public T data { get; set; }

    }
}
