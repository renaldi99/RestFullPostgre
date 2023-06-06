namespace RestFullPostgre.Message
{
    public class PayloadMessage
    {
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
        public dynamic payload { get; set; }
    }

    public class PayloadPage<T> where T : class
    {
        public T? data { get; set; }
        public int pageNumber { get; set; }
        public int pageSize { get; set; }
    }

    public class ParameterPage
    {
        public int page { get; set; } = 1;
        public int size { get; set; } = 10;
    }
}
