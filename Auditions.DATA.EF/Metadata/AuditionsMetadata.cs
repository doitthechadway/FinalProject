using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Auditions.DATA.EF.Metadata
{
    class AuditionsMetadata
    {
        [MetadataType(typeof(Actor))]
        public partial class Actor { }
        public class ActorMetaData
        {

            [Required(ErrorMessage = "*First Name is Required")]
            [Display(Name = "First Name")]
            public string ActorFirstName { get; set; }

            [Required(ErrorMessage = "*Last Name is Required")]
            [Display(Name = "Last Name")]
            public string ActorLastName { get; set; }

            [Required(ErrorMessage = "*Actor Contact ID is Required")]
            [Display(Name = "Actor Contact Info ID")]
            public int ActorContactID { get; set; }

            [Required(ErrorMessage = "*Agency Name is Required")]
            [Display(Name = "Agency Name")]
            public string AgencyID { get; set; }

            [DisplayFormat(NullDisplayText = "*Not Available")]
            [Display(Name = "Actor Photo/Headshot")]
            public string ActorPhoto { get; set; }

            [DisplayFormat(NullDisplayText = "*Not Available")]
            [Display(Name = "Notes about Actor")]
            public string SpecialNotes { get; set; }

            [DisplayFormat(NullDisplayText = "*Not Available", DataFormatString = "{0:d}", ApplyFormatInEditMode = true))]
            [Display(Name = "Date Hired by Agency")]
            public Nullable<System.DateTime> DateAdded { get; set; }
            public bool IsActive { get; set; }
        }


        [MetadataType(typeof(ActorContactInfo))]
        public partial class ActorContactInfo { }

        public class ActorContactInfoMetaData
        {
            [Display(Name = "Actor Contact ID")]
            public int ActorContactID { get; set; }
            [Display(Name = "Address")]
            public string Address { get; set; }

            public string City { get; set; }

            public string State { get; set; }
            [Display(Name = "Zip Code")]
            public string ZipCode { get; set; }
            [Display(Name = "Phone Number")]
            [StringLength(]
            public string PhoneNumber { get; set; }
        }

        [MetadataType(typeof(AuditionLocation))]
        public partial class AuditionLocation { }

        public class AuditionLocationMetaData
        {


            public int LocationID { get; set; }

            public string LocationName { get; set; }

            public string Address { get; set; }

            public string City { get; set; }

            public string State { get; set; }

            public string ZipCode { get; set; }

            public byte AuditionLimit { get; set; }

            public string AuditionPhoto { get; set; }

            public string AuditionDetails { get; set; }

            [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> AuditionDate { get; set; }
            public bool IsActive { get; set; }
        }

        [MetadataType(typeof(Reservation))]
        public partial class Reservation
        { }

        public class ReservationMetaData
        {
            [Required(ErrorMessage = "*Auditon ID Required")]
            [Display(Name = "Audition ID")]
            public int AuditionID { get; set; }

            [Required(ErrorMessage = "*Actor ID Required")]
            [Display(Name = "Actor ID")]
            public int ActorId { get; set; }


            [Required(ErrorMessage = "*Location ID Required")]
            [Display(Name = "Location ID")]
            public int LocationId { get; set; }

            [Required(ErrorMessage = "*Audition Date Required")]
            [Display(Name = "Audition Date")]
            [DisplayFormat(DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            public System.DateTime AuditionDate { get; set; }
        }

        [MetadataType(typeof(UserDetail))]
        public partial class UserDetail { }

        public class UserDetailMetaData
        {
            [Required(ErrorMessage ="*Agency ID Required")]
            [Display(Name = "Agency ID")]
            [StringLength(128, ErrorMessage = "*128 character limit")]
            public string UserID { get; set; }

            [Required(ErrorMessage = "*Agent's First Name Required")]
            [Display(Name = "Agent's First Name")]
            [StringLength(50, ErrorMessage = "*50 character limit")]
            public string FirstName { get; set; }

            [Required(ErrorMessage = "*Agen'ts Last Name Required")]
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
            public string UserDetails { get; set; }

            [Display(Name = "Agency Date Founded")]
            [DisplayFormat(NullDisplayText = "*Not Available", DataFormatString = "{0:d}", ApplyFormatInEditMode = true)]
            public Nullable<System.DateTime> DateFounded { get; set; }

            [Display(Name = "Agency Name")]
            [StringLength(50, ErrorMessage = "*50 character limit")]
            [DisplayFormat(NullDisplayText = "*Not Available")]
            public string AgencyName { get; set; }
        }
    }
}
