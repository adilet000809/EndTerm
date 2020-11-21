using System;
using System.Collections.Generic;
using System.Text;
using EndTerm.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace EndTerm.Data
{
    public class ApplicationDbContext : IdentityDbContext<IdentityUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }
        
        public DbSet<Category> Categories { get; set; }
        public DbSet<Advertisement> Advertisements { get; set; }
        public DbSet<Oblast> Oblasts { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Favourites> Favourites { get; set; }
        public DbSet<FavouritesItem> FavouritesItems { get; set; }
        
    }
}