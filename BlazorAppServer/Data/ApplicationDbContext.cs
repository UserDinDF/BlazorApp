using BlazorAppServer.Areas.Identity.Data;
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

        //protected override void OnModelCreating(ModelBuilder builder)
        //{
        //    builder.Entity<FileModel>().Property(x => x.Id).UseIdentityColumn();

        //    base.OnModelCreating(builder);
        //}

        public DbSet<FileModel> Loads { get; set; }
      
    }
}
