using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace GlossaryApp.Models
{
    public class TermModel
    {
        public int TermID { get; set; }
        [Required(ErrorMessage = "Term field is required.")]
        public string Term { get; set; }
        [Required(ErrorMessage = "Definition field is required."), MaxLength(250)]
        public string Definition { get; set; }
    }
}