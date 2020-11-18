using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditions.DATA.EF
{
    [MetadataType(typeof(ActorMetaData))]
    public partial class Actor { }

    public class ActorMetaData
    {

        [Required(ErrorMessage = "*First Name is Required")]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "*50 character limit")]

        public string ActorFirstName { get; set; }

        [Required(ErrorMessage = "*Last Name is Required")]
        [Display(Name = "Last Name")]
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
        [RegularExpression("phone", ErrorMessage = "*Please input a valid phone number: 123-456-6789")]
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

    [MetadataType(typeof(AuditionLocationMetaData))]
    public partial class AuditionLocation { }

    public class AuditionLocationMetaData
    {
        [Display(Name = "Location ID")]
        [Required(ErrorMessage = "*Location ID Required")]
        public int LocationID { get; set; }

        [Display(Name = "Location Name")]
        [Required(ErrorMessage = "*Location Name Required")]
        [StringLength(50, ErrorMessage = "*50 character limit")]
        public string LocationName { get; set; }


        [Required(ErrorMessage = "*Address Required")]
        [StringLength(100, ErrorMessage = "*100 character limit")]
        public string Address { get; set; }

        [Required(ErrorMessage = "*City Required")]
        [StringLength(100, ErrorMessage = "*100 character limit")]
        public string City { get; set; }

        [Required(ErrorMessage = "*State Required")]
        [StringLength(2, ErrorMessage = "*2 character limit")]
        public string State { get; set; }

        [Display(Name = "Zip Code")]
        [Required(ErrorMessage = "*Zip Code Required")]
        [StringLength(5, ErrorMessage = "*5 character limit")]
        public string ZipCode { get; set; }

        [Display(Name = "Audition Limit")]
        [Required(ErrorMessage = "*Audition Limit Required")]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid Value")]

        public byte AuditionLimit { get; set; }

        [Display(Name = "Audition Photo")]
        [DisplayFormat(NullDisplayText = "*Not Available")]
        [StringLength(50, ErrorMessage = "*50 character limit")]
        public string AuditionPhoto { get; set; }

        [Display(Name = "Audition Details")]
        [DisplayFormat(NullDisplayText = "*Not Available")]
        [StringLength(300, ErrorMessage = "*300 character limit")]
        [UIHint("MultilineText")]
        public string AuditionDetails { get; set; }

        [Display(Name = "Audition Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true, NullDisplayText = "*Not Available")]
        public Nullable<System.DateTime> AuditionDate { get; set; }

        [Display(Name = "Audition Status (Active/Inactive)")]
        [Required(ErrorMessage = "*Show Active/Inactive Audition Required")]
        public bool IsActive { get; set; }
    }

    [MetadataType(typeof(ReservationMetaData))]
    public partial class Reservation
    { }

    public class ReservationMetaData
    {
        [Required(ErrorMessage = "*Auditon ID Required")]
        [Display(Name = "Audition ID")]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid Value")]
        public int AuditionID { get; set; }

        [Required(ErrorMessage = "*Actor ID Required")]
        [Display(Name = "Actor ID")]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid Value")]
        public int ActorId { get; set; }


        [Required(ErrorMessage = "*Location ID Required")]
        [Display(Name = "Location ID")]
        [Range(0, double.MaxValue, ErrorMessage = "Invalid Value")]
        public int LocationId { get; set; }

        [Required(ErrorMessage = "*Reservation Date Required")]
        [Display(Name = "Reservation Date")]
        [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public System.DateTime AuditionDate { get; set; }
    }

    [MetadataType(typeof(UserDetailMetaData))]
    public partial class UserDetail { }

    public class UserDetailMetaData
    {
        [Required(ErrorMessage = "*Agency ID Required")]
        [Display(Name = "Agency ID")]
        [StringLength(128, ErrorMessage = "*128 character limit")]
        public string UserID { get; set; }

        [Required(ErrorMessage = "*Agent's First Name Required")]
        [Display(Name = "Agent's First Name")]
        [StringLength(50, ErrorMessage = "*50 character limit")]
        public string FirstName { get; set; }

        [Required(ErrorMessage = "*Agent's Last Name Required")]
        [Display(Name = "Agent's Last Name")]
        [StringLength(50, ErrorMessage = "*50 character limit")]
        public string LastName { get; set; }

        [Display(Name = "Agency Logo")]
        [StringLength(50, ErrorMessage = "*50 character limit")]
        [DisplayFormat(NullDisplayText = "*Not Available")]
        public string UserPhoto { get; set; }

        [Display(Name = "Agency Details")]
        [StringLength(300, ErrorMessage = "*300 character limit")]
        [DisplayFormat(NullDisplayText = "*Not Available")]
        [UIHint("MultilineText")]
        public string UserNotes { get; set; }

        [Display(Name = "Agency Date Founded")]
        [DisplayFormat(NullDisplayText = "*Not Available", DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
        public Nullable<System.DateTime> DateFounded { get; set; }

        [Display(Name = "Agency Name")]
        [StringLength(50, ErrorMessage = "*50 character limit")]
        [DisplayFormat(NullDisplayText = "*Not Available")]
        public string AgencyName { get; set; }
    }
}
