﻿using System.ComponentModel.DataAnnotations;

namespace RestFullPostgre.Models
{
    public class TrancodeManual
    {
        [Required]
        public string name_trancode { get; set; }
        [Required]
        public string caller_trancode { get; set; }
        [Required]
        public string caller_language { get; set; }
        [Required]
        public string squad_related { get; set; }
        public string use { get; set; }
        public string group_trancode { get; set; }
    }
}
