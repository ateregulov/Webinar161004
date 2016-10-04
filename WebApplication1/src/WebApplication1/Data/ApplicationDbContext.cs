using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using WebApplication1.Models;

namespace WebApplication1.Data
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            // Customize the ASP.NET Identity model and override the defaults if needed.
            // For example, you can rename the ASP.NET Identity table names and more.
            // Add your customizations after calling base.OnModelCreating(builder);

            builder.Entity<Message>()
                .HasOne(p => p.Recipient)
                .WithMany(b => b.MessagesReceived)
                .HasForeignKey(p => p.RecipientId);

            builder.Entity<Message>()
                .HasOne(p => p.Sender)
                .WithMany(b => b.MessagesSended)
                .HasForeignKey(p => p.SenderId);
        }

        public DbSet<Message> Messages { get; set; }

        public DbSet<ApplicationUser> ApplicationUser { get; set; }

    }
}
