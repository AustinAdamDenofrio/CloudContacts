using CloudContacts.Data;
using System.ComponentModel.DataAnnotations;

namespace CloudContacts.Models
{
    public class Category
    {
        public int Id { get; set; }

        [Required]
        [Display(Name = "Category Name")]
        public string? Name { get; set; }

        public virtual ICollection<Contact> Contacts { get; set; } = new HashSet<Contact>();

        [Required]
        public string? AppUserId { get; set; }
        public virtual ApplicationUser? AppUser { get; set; }
    }
}
