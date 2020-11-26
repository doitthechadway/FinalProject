using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace Auditions.UI.MVC.Models
{
    public class ActorViewModel
    {
        [Required(ErrorMessage = "*First Name is Required")]
        [Display(Name = "Actor First Name")]
        [StringLength(50, ErrorMessage = "*50 character limit")]

        public string ActorFirstName { get; set; }

        [Required(ErrorMessage = "*Last Name is Required")]
        [Display(Name = "Actor Last Name")]
        [StringLength(50, ErrorMessage = "*50 character limit")]
        public string ActorLastName { get; set; }

        [Display(Name = "Address")]
        [DisplayFormat(NullDisplayText = "*Not Available")]
        [StringLength(100, ErrorMessage = "*100 character limit")]
        public string Address { get; set; }

        [DisplayFormat(NullDisplayText = "*Not Available")]
        [StringLength(100, ErrorMessage = "*100 character limit")]
        public string City { get; set; }

        [DisplayFormat(NullDisplayText = "*Not Available")]
        [StringLength(2, ErrorMessage = "*2 character limit")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [DisplayFormat(NullDisplayText = "*Not Available")]
        [StringLength(5, ErrorMessage = "*5 character limit")]
        public string ZipCode { get; set; }

        [Display(Name = "Phone Number")]
        [StringLength(13, ErrorMessage = "*13 character limit")]
        [DisplayFormat(NullDisplayText = "*Not Available")]
        public string PhoneNumber { get; set; }

        [Required(ErrorMessage = "*Agency Name is Required")]
        [Display(Name = "Agency Name")]
        [StringLength(128, ErrorMessage = "*128 character limit")]
        public string AgencyID { get; set; }

        [DisplayFormat(NullDisplayText = "*Not Available")]
        [Display(Name = "Actor Photo/Headshot")]
        [StringLength(50, ErrorMessage = "*50 character limit")]
        public string ActorPhoto { get; set; }

        [DisplayFormat(NullDisplayText = "*Not Available")]
        [Display(Name = "Notes about Actor")]
        [StringLength(300, ErrorMessage = "*300 character limit")]
        [UIHint("MultilineText")]
        public string SpecialNotes { get; set; }

        [DisplayFormat(NullDisplayText = "*Not Available", DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date Hired by Agency")]
        public Nullable<System.DateTime> DateAdded { get; set; }

        [Required(ErrorMessage = "*Actor Status (Active/Inactive)")]
        public bool IsActive { get; set; }
    }
}