using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace AssessmentAPI.Models
{
    public class User
    {
        [Key]
        public int userid { get; set; }
        [Required]
        public string username { get; set; }
        [Required]
        public string email { get; set; }
        [Required]
        public string phoneNo { get; set; }
        [Required]
        public string skillset { get; set; }
        [Required]
        public string hobby { get; set; }
    }
}
