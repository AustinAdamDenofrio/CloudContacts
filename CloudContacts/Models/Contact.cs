using CloudContacts.Client.Models.Enums;
using CloudContacts.Data;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace CloudContacts.Models
{
    public class Contact
    {
        private DateTimeOffset? _birthDate;
        private DateTimeOffset _created;

        public int Id { get; set; }
        [Required]
        [Display(Name = "First Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string? FirstName { get; set; }

        [Required]
        [Display(Name = "Last Name")]
        [StringLength(50, ErrorMessage = "The {0} must be at least {2} and at most {1} characters long", MinimumLength = 2)]
        public string? LastName { get; set;}

        [NotMapped]
        public string FullName => $"{FirstName} {LastName}";

        [Display(Name = "Birthday")]
        [DataType(DataType.Date)]
        public DateTimeOffset? BirthDate {
            get { return _birthDate;  }
            set { _birthDate = value?.ToUniversalTime();  }
        }

        [Required]
        [Display(Name = "Address")]
        public string? Address { get; set; }

        [Display(Name = "Address 2")]
        public string? Address2 { get; set; }

        [Required]
        public string? City { get; set; }

        [Required]
        public State State { get; set; }

        [Required]
        [Display(Name = "Zip Code")]
        [DataType(DataType.PostalCode)]
        public int ZipCode { get; set; }

        [Required]
        [EmailAddress]
        public string? Email { get; set; }

        [Required]
        [Phone]
        [Display(Name = "Phone Numbe")]
        public string? PhoneNumber { get; set;}

        [Required]
        public DateTimeOffset Created 
        {
            get => _created;
            set => _created = value.ToUniversalTime();
        }

        [Required]
        public string? AppUserId {  get; set; }
        public virtual ApplicationUser? AppUser { get; set; }

        public virtual ICollection<Category> Categories { get; set; } = new HashSet<Category>();

        public Guid? ImageId { get; set; }
        public virtual ImageUpload? Image {  get; set; }
    }
    //TODO: What about the DTOs???
}
