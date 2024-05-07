using CloudContacts.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace CloudContacts.Data
{
    public class ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : IdentityDbContext<ApplicationUser>(options)
    {
        public virtual DbSet<ImageUpload> Images { get; set; }

        public DbSet<Contact> Contacts { get;set; }

        public DbSet<Category> Categories { get; set; }
        public IEnumerable<object> Category { get; internal set; }
    }
}
