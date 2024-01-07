using BlazorAppServer.Areas.Identity.Data;
using BlazorAppServer.Data.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace BlazorAppServer.Data
{
    public class ApplicationDbContext : IdentityDbContext<BlazorAppServerUser, IdentityRole, string>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<FileModel>()
                .HasMany(f => f.Comments)
                .WithOne(c => c.FileModel) 
                .HasForeignKey(c => c.FileModelId);

            modelBuilder.Entity<FileModel>()
             .HasMany(f => f.Image)
             .WithOne(c => c.FileModel)
             .HasForeignKey(c => c.FileModelId);
        }

        public DbSet<FileModel> Loads { get; set; }
        public DbSet<CommentModel> Comments { get; set; }
        public DbSet<ImageModel> Images { get; set; }

    }
}
