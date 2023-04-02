using System.ComponentModel.DataAnnotations;

namespace RestFullPostgre.Dto
{
    public class SearchTrancodeManualDto
    {
        public string name_trancode { get; set; }
        public string caller_trancode { get; set; }
        public string caller_language { get; set; }
        public string squad_related { get; set; }
    }
}
