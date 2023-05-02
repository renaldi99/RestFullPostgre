using System.ComponentModel.DataAnnotations;

namespace RestFullPostgre.Dto
{
    public class SearchTrancodeInformationDto
    {
        public string? name_trancode { get; set; }
        public string? type_trancode { get; set; }
        public string? description { get; set; }
        public string? environment { get; set; }
    }
}
