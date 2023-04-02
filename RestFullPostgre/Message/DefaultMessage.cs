namespace RestFullPostgre.Message
{
    public class DefaultMessage
    {
        public bool isSuccess { get; set; }
        public int statusCode { get; set; }
        public string message { get; set; }
    }
}
