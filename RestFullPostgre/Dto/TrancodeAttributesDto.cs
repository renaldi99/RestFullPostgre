using System.ComponentModel.DataAnnotations;

namespace RestFullPostgre.Dto
{
    public class TrancodeAttributesDto
    {
        public string? name_trancode { get; set; }
        public string? type_trancode { get; set; }
        public string? description { get; set; }
        public string? environment { get; set; }
        public string caller_trancode { get; set; }
        public string caller_language { get; set; }
        public string squad_related { get; set; }
        public string use { get; set; }
        public string group_trancode { get; set; }
    }

    public class SearchTrancodeAttributesDto
    {
        public string? name_trancode { get; set; }
        public string? type_trancode { get; set; }
        public string? description { get; set; }
        public string? environment { get; set; }
        public string caller_trancode { get; set; }
        public string caller_language { get; set; }
        public string squad_related { get; set; }
    }
}
