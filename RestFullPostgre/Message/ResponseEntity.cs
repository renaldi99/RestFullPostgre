using System.Text.Json.Serialization;

namespace RestFullPostgre.Message
{
    public class ResponseEntity
    {
        public bool isSuccess { get; set; }
        public string message { get; set; }
        public dynamic data { get; set; }
    }
}
