using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auditions.UI.MVC.Models
{
    public class ContactViewModel
    {
        [Required(ErrorMessage = "*Name is required")]
        [StringLength(50, ErrorMessage ="*50 character limit")]
        public string Name { get; set; }

        [DataType(DataType.EmailAddress)]
        public string Email { get; set; }

        [Required(ErrorMessage = "*Subject required")]
        [StringLength(100, ErrorMessage ="*100 character limit")]
        public string Subject { get; set; }

        [Required(ErrorMessage ="*Message required")]
        [StringLength(500, ErrorMessage ="*500 character limit")]
        [UIHint("MultilineText")]
        public string Message { get; set; }
    }
}