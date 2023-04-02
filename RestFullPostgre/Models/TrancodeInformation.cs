using System.ComponentModel.DataAnnotations;

namespace RestFullPostgre.Models
{
    public class TrancodeInformation
    {
        [Required]
        public string? name_trancode { get; set; }
        [Required]
        public string? type_trancode { get; set; }
        [Required]
        public string? description { get; set; }
        [Required]
        public string? environment { get; set; }
    }
}
