using DataAccessLayer.Entity;
using Microsoft.EntityFrameworkCore;
using PizzaShopDAL.Data;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace DataAccessLayer
{
    public class SocialNetworkDBContext : DbContext
    {
        public DbSet<User> Users { get; set; }
        public DbSet<Profile> Profiles { get; set; }
        public DbSet<Galery> Galeries { get; set; }
        public DbSet<Photo> Photos { get; set; }
        
        private string connectionString { get; set; }

        public SocialNetworkDBContext(string connectionString)
        {
            this.connectionString = connectionString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(connectionString);
            base.OnConfiguring(optionsBuilder);
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<User>()
                .HasIndex(u => u.Username)
                .IsUnique();

            modelBuilder.Entity<Profile>().OwnsOne(a => a.Address);

            /* setup One-To-Many relationship */
            modelBuilder.Entity<Galery>()
                 .HasOne(g => g.Profile)
                 .WithMany(p => p.Galeries)
                 .HasForeignKey(a => a.ProfileId);

            modelBuilder.Entity<Photo>()
                 .HasOne(p => p.Galery)
                 .WithMany(g => g.Photos)
                 .HasForeignKey(a => a.GaleryId);

           modelBuilder.Entity<User>()
                .HasOne<Profile>(u => u.Profile)
                .WithOne(o => o.Owner)
                .HasForeignKey<Profile>(p => p.OwnerId);

            modelBuilder.Seed();

            base.OnModelCreating(modelBuilder);
        }
    }
}
